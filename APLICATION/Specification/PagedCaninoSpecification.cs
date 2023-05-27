using Ardalis.Specification;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Specification
{
    public class PagedCaninoSpecification : Specification<Caninos>
    {
        public PagedCaninoSpecification(int pageSize, int pageNumber, string nombre, string raza)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, "%" + nombre + "%");
            if (!string.IsNullOrEmpty(raza))
                Query.Search(x => x.Raza, "%" + raza + "%");

        }
    }
}
