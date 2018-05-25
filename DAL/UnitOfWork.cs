using System;
using System.Data.Entity.Validation;
using System.Linq;
using DAL.Repositories;
using DAL.Repositories.Interfaces;

namespace DAL
{
	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly DataContext _context;
		private IAlternativeRepository _alternativeRepository;
		private ICriterionRepository _criterionRepository;
		private ILprRepository _lprRepository;
		private IMarkRepository _markRepository;
		private IResultRepository _resultRepository;
		private IVectorRepository _vectorRepository;

		public UnitOfWork(DataContext context)
		{
			_context = context;
		}

		public IAlternativeRepository AlternativeRepository => _alternativeRepository ?? (_alternativeRepository = new AlternativeRepository(_context));

		public ICriterionRepository CriterionRepository => _criterionRepository ?? (_criterionRepository = new CriterionRepository(_context));

		public ILprRepository LprRepository => _lprRepository ?? (_lprRepository = new LprRepository(_context));

		public IMarkRepository MarkRepository => _markRepository ?? (_markRepository = new MarkRepository(_context));

		public IResultRepository ResultRepository => _resultRepository ?? (_resultRepository = new ResultRepository(_context));

		public IVectorRepository VectorRepository => _vectorRepository ?? (_vectorRepository = new VectorRepository(_context));

		public int Save()
		{
			try
			{
				return _context.SaveChanges();
			}
			catch (DbEntityValidationException ex)
			{
				// Retrieve the error messages as a list of strings.
				var errorMessages = ex.EntityValidationErrors
					.SelectMany(x => x.ValidationErrors)
					.Select(x => x.ErrorMessage);

				// Join the list to a single string.
				var fullErrorMessage = string.Join("; ", errorMessages);

				// Combine the original exception message with the new one.
				var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);

				// Throw a new DbEntityValidationException with the improved exception message.
				throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
			}
		}

		public void Dispose()
		{
			_context?.Dispose();
		}
	}
}