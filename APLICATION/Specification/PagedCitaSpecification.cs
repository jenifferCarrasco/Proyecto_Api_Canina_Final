using Ardalis.Specification;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Specification
{
    public class PagedCitaSpecification : Specification<Citas>
    {
        public PagedCitaSpecification(int pageSize, int pageNumber, DateTime fecha, Guid centro)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (fecha != null)
                Query.Search(x => Convert.ToString(x.FechaCita), "%" + fecha + "%");
            if (centro != null)
                Query.Search(x => Convert.ToString(x.CentroId), "%" + centro + "%");

        }
    }
}
