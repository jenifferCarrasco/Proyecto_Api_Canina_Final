using Ardalis.Specification;
using DOMAIN.Canina.Entities;

namespace APLICATION.Specification
{
	public class PagedCentroSpecification : Specification<Centro>
    {
        public PagedCentroSpecification(int pageSize, int pageNumber, string nombre)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, "%" + nombre + "%");

        }

		public PagedCentroSpecification(string nombre)
		{
			if (!string.IsNullOrEmpty(nombre))
				Query.Search(x => x.Nombre, "%" + nombre + "%");
		}
	}
}
