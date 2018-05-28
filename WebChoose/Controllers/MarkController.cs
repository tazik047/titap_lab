using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace WebChoose.Controllers
{
	public class MarkController : CrudControllerBase<Mark>
	{
		public MarkController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		protected override IRepository<Mark> Repository => UnitOfWork.MarkRepository;

		protected override object GetEntityId(Mark entity)
		{
			return entity.Id == 0 ? (object)null : entity.Id;
		}
	}
}