using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
	internal class LprRepository : Repository<LPR>, ILprRepository
	{
		public LprRepository(DataContext context) : base(context)
		{
		}
	}
}