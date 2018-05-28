using DAL;
using DAL.Models;
using DAL.Repositories.Interfaces;

namespace WebChoose.Controllers
{
	public class AlternativeController : CrudControllerBase<Alternative>
	{
		public AlternativeController(IUnitOfWork unitOfWork) : base(unitOfWork)
		{
		}

		protected override IRepository<Alternative> Repository => UnitOfWork.AlternativeRepository;

		protected override object GetEntityId(Alternative entity)
		{
			return entity.Id == 0 ? (object)null : entity.Id;
		}
	}
}