using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using WebChoose.Infrastructure;
using WebChoose.Models;

namespace WebChoose.Controllers
{
	public class ResultController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public ResultController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Items()
		{
			var results = _unitOfWork
				.ResultRepository
				.Get()
				.GroupBy(p => p.LPRId)
				.Select(p => new
				{
					p.First().LPR.Name,
					Choose = GetResultHtmlList(p)
				})
				.ToArray();

			return this.ToJson(results);
		}

		[HttpGet]
		public ActionResult Step1()
		{
			var criterions = _unitOfWork
				.CriterionRepository
				.Get()
				.Select(p => new SelectListItem
				{
					Text = p.Name,
					Value = p.Id.ToString(),
					Selected = true
				})
				.OrderBy(p=>p.Text)
				.ToList();

			ViewBag.Lprs = _unitOfWork
				.LprRepository
				.Get()
				.Select(p => new SelectListItem
				{
					Text = p.Name,
					Value = p.Id.ToString()
				})
				.OrderBy(p=>p.Text)
				.ToList();

			return View(criterions);
		}

		[HttpPost]
		public ActionResult Step1(int lprId, int[] criterions)
		{
			return RedirectToAction("Step2", new {lprId, criterions = string.Join(",", criterions)});
		}

		[HttpGet]
		public ActionResult Step2(int lprId, string criterions)
		{
			ViewBag.LprId = lprId;
			ViewBag.Criterions = criterions;

			var ids = ParseCriterionIds(criterions);
			var marks = GetMarksByCriterion(ids);

			return View(marks);
		}

		public ActionResult Step2(int lprId, string criterions, Dictionary<int, int> mark)
		{
			var marks = _unitOfWork
				.MarkRepository
				.Get(p => mark.Keys.Contains(p.Id))
				.ToList();

			foreach (var i in marks)
			{
				i.NumMark = mark[i.Id];
				_unitOfWork.MarkRepository.Update(i);
			}

			_unitOfWork.Save();


			return RedirectToAction("Step3", new {lprId, criterions});
		}

		[HttpGet]
		public ActionResult Step3(int lprId, string criterions)
		{
			var ids = ParseCriterionIds(criterions);

			var marks = GetMarksByCriterion(ids)
				.ToDictionary(p => p.Key, p =>
				{
					var type = p.Value.First().Criterion.OptimType;
					return new MarkNormalizationViewModel
					{
						Type = type,
						Marks = p.Value,
						Etalon = type == OptimType.Maximum
							? p.Value.Max(t => t.NumMark)
							: p.Value.Min(t => t.NumMark)
					};
				});

			foreach (var criterion in marks)
			{
				foreach (var mark in criterion.Value.Marks)
				{
					if (criterion.Value.Type == OptimType.Maximum)
					{
						mark.NormMark = mark.NumMark * 1.0 / criterion.Value.Etalon;
					}
					else
					{
						mark.NormMark = criterion.Value.Etalon * 1.0 / mark.NumMark;
					}

					_unitOfWork.MarkRepository.Update(mark);
				}
			}

			_unitOfWork.Save();

			return View(marks);
		}

		public ActionResult Step4(int lprId, string criterions)
		{
			var resultModel = new ChooseResultViewModel();
			var ids = ParseCriterionIds(criterions);
			var lpr = _unitOfWork.LprRepository.Find(lprId);
			var alternatives = _unitOfWork
				.AlternativeRepository
				.Get()
				.ToList();

			var alternativeMarks = new Dictionary<int, AlternativeChooseResult>();

			foreach (var alternative in alternatives)
			{
				alternativeMarks[alternative.Id] = new AlternativeChooseResult
				{
					AlternativeName = alternative.Name,
					Marks = alternative
						.Vectors
						.Where(p => ids.Contains(p.Mark.CriterionId))
						.OrderBy(p => p.Mark.CriterionId)
						.Select(p => new Tuple<int, double>(p.Mark.CriterionId, p.Mark.NormMark))
						.ToArray()
				};
			}

			var tmp = alternativeMarks.Values.First();
			var normKoef = Enumerable
				.Range(0, tmp.Marks.Length)
				.Select(p =>
				{
					var sum = alternativeMarks.Keys.Select(k => alternativeMarks[k].Marks[p].Item2).Sum();
					return new
					{
						Koef = Math.Abs(sum) < 1e-8 ? 0 : (1 / sum),
						CriterionId = tmp.Marks[p].Item1
					};
				})
				.ToArray();

			foreach (var alternative in alternativeMarks)
			{
				alternative.Value.Result = 0.0;
				for (int i = 0; i < normKoef.Length; i++)
				{
					alternative.Value.Result += normKoef[i].Koef * alternative.Value.Marks[i].Item2;
				}
			}

			resultModel.LprName = lpr.Name;
			resultModel.Alternatives = alternatives.ToArray();
			resultModel.NormKoeficients = normKoef.ToDictionary(p => p.CriterionId, p => p.Koef);
			resultModel.Results = alternativeMarks
				.Values
				.OrderByDescending(p => p.Result)
				.Select(p => new Tuple<string, double>(p.AlternativeName, p.Result))
				.ToArray();

			_unitOfWork
				.ResultRepository
				.Get(p => p.LPRId == lprId)
				.ToList()
				.ForEach(_unitOfWork.ResultRepository.Drop);

			alternativeMarks
				.OrderByDescending(p => p.Value.Result)
				.Select((p, i) => new Result
				{
					LPRId = lprId,
					AlternativeId = p.Key,
					Range = i
				}).ToList()
				.ForEach(_unitOfWork.ResultRepository.Create);

			_unitOfWork.Save();

			return View(resultModel);
		}

		private int[] ParseCriterionIds(string criterions)
		{
			return criterions
				.Split(',')
				.Select(p =>
				{
					int a;
					return int.TryParse(p.Trim(), out a) ? a : -1;
				})
				.Where(p => p != -1)
				.ToArray();
		}

		private Dictionary<string, Mark[]> GetMarksByCriterion(int[] criterionIds)
		{
			return _unitOfWork
				.MarkRepository
				.Get(p => criterionIds.Contains(p.CriterionId))
				.GroupBy(p => p.Criterion.Name)
				.ToDictionary(p => p.Key, p => p.OrderBy(t => t.Name).ToArray());
		}

		private string GetResultHtmlList(IEnumerable<Result> results)
		{
			var builder = new StringBuilder("<ol>");
			foreach (var result in results)
			{
				builder.AppendFormat("<li>{0}</li>", result.Alternative.Name);
			}

			builder.Append("</ol>");
			return builder.ToString();
		}
	}
}