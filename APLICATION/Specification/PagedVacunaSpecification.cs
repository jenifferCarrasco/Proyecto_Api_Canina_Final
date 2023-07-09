using Ardalis.Specification;
using DOMAIN.Canina;
using DOMAIN.Canina.Entities;

namespace APLICATION.Specification
{
	public class PagedVacunaSpecification : Specification<Vacuna>
	{
		public PagedVacunaSpecification(int pageSize, int pageNumber, string nombre)
		{
			Query.Include(X => X.VacunaInventario)
				.Where(x=>x.Estatus == Estados.Activo);

			Query.Skip((pageNumber - 1) * pageSize)
				.Take(pageSize);

			if (!string.IsNullOrEmpty(nombre))
				Query.Search(x => x.Nombre, "%" + nombre + "%");

			//if (!string.IsNullOrEmpty(lab))
			//    Query.Search(x => x.Laboratorio, "%" + lab + "%");

		}

		public PagedVacunaSpecification(string nombre)
		{
			Query.Include(X => X.VacunaInventario);

			if (!string.IsNullOrEmpty(nombre))
				Query.Search(x => x.Nombre, "%" + nombre + "%");

		}
	}
}
