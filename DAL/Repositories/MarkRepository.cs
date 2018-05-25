using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
	internal class MarkRepository : Repository<Mark>, IMarkRepository
	{
		public MarkRepository(DataContext context) : base(context)
		{
		}
	}
}