using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Localiza.Core.Responses
{
    public class Response<T>(
                    T? data,
                    int code = Configuration.DefaultStatusCode,
                    string? message = null)
    {
        public T? Data { get; set; } = data;
        public string? Message { get; set; } = message;
        public int StatusCode => code;

        [JsonIgnore]
        public bool IsSuccess => code is >= 200 and <= 299;
    }
}
