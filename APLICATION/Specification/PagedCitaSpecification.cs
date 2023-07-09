using Ardalis.Specification;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;
using System;

namespace APLICATION.Specification
{
	public class PagedCitaSpecification : Specification<Cita>
	{
		public PagedCitaSpecification(int pageNumber, int pageSize, string centro)
		{
			Query
				.Include(x => x.Canino)
				.Include(x => x.Centro)
				.Include(x => x.Vacunador)
				.Include(x => x.Propietario);
				//Where(x => x.Estatus == Estados.Activo)

			Query.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize);


			if (centro != null)
				Query.Search(x => x.CentroId.ToString(), "%" + centro + "%");

		}

		public PagedCitaSpecification(Guid id)
		{
			Query.Where(x => x.Id == id)
				.Include(x => x.Canino)
				.Include(x => x.Centro)
				.Include(x => x.Vacunador)
				.Include(x => x.Propietario);
		}

		public PagedCitaSpecification(Guid propietarioId, int? id)
		{
			Query.Where(x => x.Propietario.Id == propietarioId && x.Estatus == Estados.Activo)
				.Include(x => x.Canino)
				.Include(x => x.Centro)
				.Include(x => x.Vacunador)
				.Include(x => x.Propietario);
		}
	}
}
