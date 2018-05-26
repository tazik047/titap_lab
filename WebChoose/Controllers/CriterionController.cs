using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace WebChoose.Controllers
{
	public class CriterionController : CrudControllerBase<Criterion>
	{
		public CriterionController(IUnitOfWork unitOfWork) : base(unitOfWork, unitOfWork.CriterionRepository)
		{
		}

		protected override object GetEntityId(Criterion entity)
		{
			return entity.Id == 0 ? (object) null : entity.Id;
		}
	}
}