using Ardalis.Specification;
using DOMAIN.Canina.Entities;
using System;

namespace APLICATION.Specification
{
	public class PagedAdminSpecification : Specification<Administrador>
    {
        public PagedAdminSpecification(int pageSize, int pageNumber, string email, string username)
        {
            Query.Include(x => x.Usuario);

            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);


            if (!string.IsNullOrEmpty(email))
                Query.Search(x => x.Usuario.Email, "%" + email + "%");

            if (!string.IsNullOrEmpty(username))
                Query.Search(x => x.Usuario.UserName, "%" + username + "%");

        }

        public PagedAdminSpecification(Guid propietarioId)
        {
			Query.Where(x=>x.Id == propietarioId)
                .Include(x => x.Usuario);

		}
    }
}
