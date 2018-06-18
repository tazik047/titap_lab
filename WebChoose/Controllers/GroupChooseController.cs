using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using WebChoose.Models;

namespace WebChoose.Controllers
{
	public class GroupChooseController : Controller
	{
		private readonly IUnitOfWork _unitOfWork;

		public GroupChooseController(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		// GET: GroupChoose
		public ActionResult Index()
		{
			var alternatives = _unitOfWork.AlternativeRepository.Get().ToList();
			var result = CalculateCompareTable(alternatives);

			ViewBag.Alternatives = alternatives.ToDictionary(p => p.Id, p => p.Name);
			ViewBag.CompairResult = GetCompairResult(result);

			return View(result);
		}

		private List<PairCompair> GetCompairResult(Dictionary<int, Dictionary<int, int>> voteTable)
		{
			return voteTable
				.Select(p =>
				{
					var winCount = p.Value.Count(r => r.Value > voteTable[r.Key][p.Key]);
					return new PairCompair
					{
						AlternativeId = p.Key,
						WinCount = winCount,
						LoseCount = p.Value.Count - winCount -1
					};
				})
				.ToList();
		}

		private Dictionary<int, Dictionary<int, int>> CalculateCompareTable(IEnumerable<Alternative> alternatives)
		{
			var ids = alternatives.Select(p => p.Id).ToList();
			var result = ids.ToDictionary(p => p, p => ids.ToDictionary(t => t, t => 0));

			var results = _unitOfWork.ResultRepository.Get()
				.GroupBy(p=>p.LPRId).ToList();

			foreach (var lpr in results)
			{
				var choice = lpr
					.OrderByDescending(p => p.Range)
					.ToList();
				var betterThan = new List<int>();
				foreach (var i in choice)
				{
					foreach (var prev in betterThan)
					{
						result[i.AlternativeId][prev] += 1;
					}

					betterThan.Add(i.AlternativeId);
				}
			}

			return result;
		}
	}
}