using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using WebChoose.Models;

namespace WebChoose.Controllers
{
	public class ResultController : Controller
	{
		private readonly UnitOfWork _unitOfWork;

		public ResultController(UnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
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
			var ids = ParseCriterionIds(criterions);
			var lpr = _unitOfWork.LprRepository.Find(lprId);
			var alternatives = _unitOfWork
				.AlternativeRepository
				.Get()
				.ToList();

			/*_unitOfWork
				.CriterionRepository
				.Get(p=>ids.Contains(p.Id))
				.ToList()
				.Select(p=>1 / p.Marks.)*/

			var result = new Dictionary<int, double[]>();

			foreach (var alternative in alternatives)
			{
				result[alternative.Id] = alternative
					.Vectors
					.Where(p => ids.Contains(p.Mark.CriterionId))
					.OrderBy(p => p.Id)
					.Select(p => p.Mark.NormMark)
					.ToArray();
			}

			var normKoef = Enumerable
				.Range(0, result.Values.First().Length)
				.Select(p => 1 / result.Keys.Select(k => result[k][p]).Sum())
				.ToArray();

			foreach (var a in result)
			{
				var r = 0.0;
				for (int i = 0; i < normKoef.Length; i++)
				{
					r += normKoef[i] * a.Value[i];
				}
			}

			return View();
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
	}
}