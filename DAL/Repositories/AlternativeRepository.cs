using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
	internal class AlternativeRepository : Repository<Alternative>, IAlternativeRepository
	{
		public AlternativeRepository(DataContext context) : base(context)
		{
		}
	}
}