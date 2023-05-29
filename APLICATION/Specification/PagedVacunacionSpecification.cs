using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Text;

namespace APLICATION.Specification
{
    public class PagedVacunacionSpecification : Specification<DOMAIN.Canina.Entities.Vacunacion>
    {
        public PagedVacunacionSpecification(int pageSize, int pageNumber, Guid canino, Guid vacunador)
        {
            Query.Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            if (canino != null)
                Query.Search(x => Convert.ToString(x.CaninoId), "%" + canino + "%");
            if (vacunador != null)
                Query.Search(x => Convert.ToString(x.VacunadorId), "%" + vacunador + "%");

        }
    }
}
