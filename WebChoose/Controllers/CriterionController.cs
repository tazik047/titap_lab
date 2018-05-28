using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace WebChoose.Controllers
{
	public class CriterionController : CrudControllerBase<Criterion>
	{
		public CriterionController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		[ChildActionOnly]
		public ActionResult GetAlternativeCriterions(IEnumerable<Vector> vectors)
		{
			//throw new Exception(string.Join(",", vectors.Select(p=>p.Mark.Name).ToList()));

			var criterions = Repository.Get().ToList();

			var selectedItems = vectors.ToDictionary(p => p.Mark.CriterionId, p => p.MarkId);

			ViewBag.SelectedItems = criterions.ToDictionary(p => p.Id,
				p => selectedItems.ContainsKey(p.Id) ? selectedItems[p.Id] : 0);

			return PartialView(criterions);
		}

		protected override IRepository<Criterion> Repository => UnitOfWork.CriterionRepository;

		protected override object GetEntityId(Criterion entity)
		{
			return entity.Id == 0 ? (object) null : entity.Id;
		}
	}
}