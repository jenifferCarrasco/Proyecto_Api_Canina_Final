using Ardalis.Specification;
using System;

namespace APLICATION.Specification
{
	public class PagedVacunacionSpecification : Specification<DOMAIN.Canina.Entities.Vacunacion>
	{
		public PagedVacunacionSpecification(int pageSize, int pageNumber, string nombreCanino)
		{
			Query.Include(x => x.Canino)
				.Include(x => x.Vacunador)
				.Include(x => x.Centro)
				.Include(x => x.Vacuna);

			Query.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize);

			if (string.IsNullOrEmpty(nombreCanino))
				Query.Search(x => x.Canino.Nombre, "%" + nombreCanino + "%");
		}

		public PagedVacunacionSpecification(Guid caninoId)
		{
			Query.Include(x => x.Canino)
				 .Include(x => x.Vacunador)
				 .Include(x => x.Centro)
				 .Include(x => x.Vacuna)
				 .Where(x => x.CaninoId == caninoId);
		}
	}
}
