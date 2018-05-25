using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
	internal class VectorRepository : Repository<Vector>, IVectorRepository
	{
		public VectorRepository(DataContext context) : base(context)
		{
		}
	}
}