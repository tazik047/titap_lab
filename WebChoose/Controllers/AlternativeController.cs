using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;
using WebChoose.Infrastructure;

namespace WebChoose.Controllers
{
	public class AlternativeController : CrudControllerBase<Alternative>
	{
		public AlternativeController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		public override ActionResult Index()
		{
			var criterions = UnitOfWork
				.CriterionRepository
				.Get()
				.OrderBy(p => p.Name)
				.Select(p => new ColumnItem(p.Id.ToString(), p.Name))
				.ToList();

			ViewBag.Columns = new[] {new ColumnItem("Name", "Name".Translate())}
				.Union(criterions)
				.ToList();

			return base.Index();
		}

		public override ActionResult Items()
		{
			var items = Repository
				.Get()
				.ToList()
				.Select(t =>
				{
					var criterions = t
						.Vectors
						.ToDictionary(p => p.Mark.CriterionId.ToString(), p => p.Mark.Name);
					criterions["Name"] = t.Name;
					criterions["Id"] = t.Id.ToString();

					return criterions;
				});

			return this.ToJson(items);
		}

		public override ActionResult Manage(Alternative model)
		{
			if (model.Id != 0)
			{
				var origin = Repository.Find(model.Id);
				foreach (var originVector in origin.Vectors.ToArray())
				{
					UnitOfWork.VectorRepository.Drop(originVector);
				}
				origin.Name = model.Name;
				foreach (var modelVector in model.Vectors)
				{
					origin.Vectors.Add(modelVector);
				}

				UnitOfWork.Save();
				

				return RedirectToAction("Details", new {id = model.Id});
			}

			return base.Manage(model);
		}

		protected override IRepository<Alternative> Repository => UnitOfWork.AlternativeRepository;

		protected override object GetEntityId(Alternative entity)
		{
			return entity.Id == 0 ? (object)null : entity.Id;
		}
	}
}