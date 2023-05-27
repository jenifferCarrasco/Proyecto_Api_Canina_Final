using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interface
{
    public interface IDateTimeService
    {
        DateTime NowUTC { get; }
    }
}
