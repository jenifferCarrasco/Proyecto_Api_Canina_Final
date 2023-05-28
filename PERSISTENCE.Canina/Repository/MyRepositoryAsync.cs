using Application.Interface;
using Ardalis.Specification.EntityFrameworkCore;
using PERSISTENCE.Canina.Context;

namespace PERSISTENCE.Canina.Repository
{
	public class MyRepositoryAsync<T> : RepositoryBase<T>, IRepositoryAsync<T> where T : class
	{
		private readonly ApplicationDbContext dbContext;
		public MyRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
		{
			this.dbContext = dbContext;
		}
	}
}
