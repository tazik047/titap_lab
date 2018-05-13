using System.Data.Entity;
using DAL.Models;

namespace DAL
{
	public class DataContext : DbContext
	{
		public DbSet<Alternative> Alternatives { get; set; }

		public DbSet<Criterion> Сriterions { get; set; }

		public DbSet<Mark> Marks { get; set; }

		public DbSet<LPR> LPRs { get; set; }

		public DbSet<Result> Results { get; set; }

		public DbSet<Vector> Vectors { get; set; }
	}
}