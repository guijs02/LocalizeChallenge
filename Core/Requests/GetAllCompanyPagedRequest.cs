using Localiza.Core;
using System.ComponentModel.DataAnnotations;

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
