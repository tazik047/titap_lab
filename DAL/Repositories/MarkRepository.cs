using DAL.Models;
using DAL.Repositories.Interfaces;

namespace DAL.Repositories
{
	internal class MarkRepository : Repository<Mark>, IMarkRepository
	{
		public MarkRepository(DataContext context) : base(context)
		{
		}

		public override void Create(Mark entity)
		{
			int result;
			if (int.TryParse(entity.Name, out result))
			{
				entity.NumMark = result;
			}

			base.Create(entity);
		}
	}
}