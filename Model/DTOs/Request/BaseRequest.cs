using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype.Model.DTOs.Request
{
    public class BaseRequest<T>
    {
        public T Request { get; set; }
        public int PageSize { get; set; } = 0;
        public int Skip { get; set; } = 10;
    }
}
