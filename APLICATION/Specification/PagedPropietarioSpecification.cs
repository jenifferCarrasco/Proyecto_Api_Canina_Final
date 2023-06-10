using Ardalis.Specification;
using DOMAIN.Canina.Entities;
using System;

namespace APLICATION.Specification
{
    public class PagedPropietarioSpecification : Specification<Propietario>
    {
        public PagedPropietarioSpecification(int pageSize, int pageNumber, string nombre, string cedula)
        {
            Query.Include(x => x.Usuario);

            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, "%" + nombre + "%");
            if (!string.IsNullOrEmpty(cedula))
                Query.Search(x => x.Cedula, "%" + cedula + "%");

        }

		public PagedPropietarioSpecification(Guid propietarioId)
		{
            Query.Where(x => x.Id == propietarioId)
            .Include(x => x.Caninos);

		}
	}
}
