using Application.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shared.Services
{
    public class DateTimeServices : IDateTimeService
    {
        public DateTime NowUTC => DateTime.UtcNow;
    }
}
 