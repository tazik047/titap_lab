using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Repositories.Interfaces;
using WebChoose.Infrastructure;

namespace WebChoose.Controllers
{
	public abstract class CrudControllerBase<T> : Controller where T: class, new()
	{
		protected readonly IUnitOfWork UnitOfWork;
		protected abstract IRepository<T> Repository { get; }

		protected CrudControllerBase(IUnitOfWork unitOfWork)
		{
			UnitOfWork = unitOfWork;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Items()
		{
			return this.ToJson(Repository.Get().ToList());
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			var item = Repository.Find(id);
			return View(item);
		}

		[HttpGet]
		public ActionResult Manage(int? id)
		{
			T item = null;
			if (id != null)
			{
				item = Repository.Find(id);
			}
			else
			{
				item = new T();
			}

			return View(item);
		}

		[HttpPost]
		public ActionResult Manage(T item)
		{
			if (GetEntityId(item) == null)
			{
				Repository.Create(item);
			}
			else
			{
				Repository.Update(item);
			}

			UnitOfWork.Save();

			return RedirectToAction("Details", new { id = GetEntityId(item) });
		}

		public ActionResult Delete(int id)
		{
			Repository.Drop(Repository.Find(id));
			UnitOfWork.Save();

			return RedirectToAction("Index");
		}

		protected abstract object GetEntityId(T entity);
	}
}