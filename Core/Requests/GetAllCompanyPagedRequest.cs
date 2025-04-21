using Localiza.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Core.Requests
{
    public class GetAllCompanyPagedRequest
    {
        [Required]
        public int PageNumber { get; set; } = Configuration.DefaultPageNumber;
        [Required]
        public int PageSize { get; set; } = Configuration.DefaultPageSize;
    }
}
