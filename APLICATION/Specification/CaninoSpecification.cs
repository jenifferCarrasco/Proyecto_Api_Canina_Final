using Ardalis.Specification;
using DOMAIN.Canina.Entities;
using System;

namespace APLICATION.Specification
{
	public class CaninoSpecification : Specification<Canino>
    {
        public CaninoSpecification(int pageSize, int pageNumber, string nombre, string raza)
        {
			Query.Skip((pageNumber - 1) * pageSize)
				  .Take(pageSize);

			if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, "%" + nombre + "%");

            if (!string.IsNullOrEmpty(raza))
                Query.Search(x => x.Raza, "%" + raza + "%");

        }

		public CaninoSpecification(Guid propietarioId)
		{
			Query.Where(x => x.PropietarioId == propietarioId);
		}
	}
}
