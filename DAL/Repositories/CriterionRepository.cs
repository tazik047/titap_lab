using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
	internal class CriterionRepository : Repository<Criterion>, ICriterionRepository
	{
		public CriterionRepository(DataContext context) : base(context)
		{
		}
	}
}