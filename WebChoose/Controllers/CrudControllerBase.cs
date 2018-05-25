using System.Linq;
using System.Web.Mvc;
using DAL;
using DAL.Repositories.Interfaces;

namespace WebChoose.Controllers
{
	public abstract class CrudControllerBase<T> : Controller where T: class, new()
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IRepository<T> _repository;

		protected CrudControllerBase(IUnitOfWork unitOfWork, IRepository<T> repository)
		{
			_unitOfWork = unitOfWork;
			_repository = repository;
		}

		public ActionResult Index()
		{
			return View();
		}

		public ActionResult Items()
		{
			return Json(_repository.Get().ToList(), JsonRequestBehavior.AllowGet);
		}

		[HttpGet]
		public ActionResult Details(int id)
		{
			var item = _repository.Find(id);
			return View(item);
		}

		[HttpGet]
		public ActionResult Manage(int? id)
		{
			T item = null;
			if (id != null)
			{
				item = _repository.Find(id);
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
				_repository.Create(item);
			}
			else
			{
				_repository.Update(item);
			}

			_unitOfWork.Save();

			return RedirectToAction("Details", new { id = GetEntityId(item) });
		}

		public ActionResult Delete(int id)
		{
			_repository.Drop(_repository.Find(id));
			_unitOfWork.Save();

			return RedirectToAction("Index");
		}

		protected abstract object GetEntityId(T entity);
	}
}