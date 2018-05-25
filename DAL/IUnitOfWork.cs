using DAL.Repositories.Interfaces;

namespace DAL
{
	public interface IUnitOfWork
	{
		IAlternativeRepository AlternativeRepository { get; }
		ICriterionRepository CriterionRepository { get; }
		ILprRepository LprRepository { get; }
		IMarkRepository MarkRepository { get; }
		IResultRepository ResultRepository { get; }
		IVectorRepository VectorRepository { get; }
		int Save();
	}
}