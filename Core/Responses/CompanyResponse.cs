using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localize.Core.Responses
{
    public class CompanyResponse
    {
        public string LegalName { get; set; } = string.Empty;
        public string Cnpj { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
