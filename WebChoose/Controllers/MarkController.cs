using DAL;
using DAL.Models;

namespace WebChoose.Controllers
{
	public class MarkController : CrudControllerBase<Mark>
	{
		public MarkController(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.MarkRepository)
		{
		}

		protected override object GetEntityId(Mark entity)
		{
			return entity.Id == 0 ? (object)null : entity.Id;
		}
	}
}