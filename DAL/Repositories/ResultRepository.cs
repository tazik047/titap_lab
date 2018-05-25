using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
	internal class ResultRepository : Repository<Result>, IResultRepository
	{
		public ResultRepository(DataContext context) : base(context)
		{
		}
	}
}