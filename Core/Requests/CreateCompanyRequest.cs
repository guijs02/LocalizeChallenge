using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Localiza.Core.Requests
{
    public class CreateCompanyRequest
    {
        public string Cnpj { get; set; } = string.Empty;
    }
}
