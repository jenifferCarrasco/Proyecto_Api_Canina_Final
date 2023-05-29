using Ardalis.Specification;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
