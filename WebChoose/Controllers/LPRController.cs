using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace WebChoose.Controllers
{
	public class LPRController : CrudControllerBase<LPR>
	{
		public LPRController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		protected override IRepository<LPR> Repository => UnitOfWork.LprRepository;

		protected override object GetEntityId(LPR entity)
		{
			return entity.Id == 0 ? (object)null : entity.Id;
		}
	}
}