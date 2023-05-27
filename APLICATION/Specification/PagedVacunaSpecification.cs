using Ardalis.Specification;
using DOMAIN.Canina.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Specification
{
    public class PagedVacunaSpecification : Specification<Vacunas>
    {
        public PagedVacunaSpecification(int pageSize, int pageNumber, string nombre, string lab)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (!string.IsNullOrEmpty(nombre))
                Query.Search(x => x.Nombre, "%" + nombre + "%");
            if (!string.IsNullOrEmpty(lab))
                Query.Search(x => x.Laboratorio, "%" + lab + "%");

        }
    }
}
