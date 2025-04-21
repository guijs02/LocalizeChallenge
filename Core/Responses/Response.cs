using System.Text.Json.Serialization;

namespace Localiza.Core.Responses
{
    public class Response<T>
    {
        private int _code = Configuration.DefaultStatusCode;

        [JsonConstructor]
        public Response() => _code = Configuration.DefaultStatusCode;

        public Response(
                        T? data,
                        int code = Configuration.DefaultStatusCode,
                        string? message = null)
        {
            Data = data;
            _code = code;
            Message = message;
        }

        public T? Data { get; set; }
        public string? Message { get; set; }

        public int StatusCode => _code;

        [JsonIgnore]
        public bool IsSuccess => _code is >= 200 and <= 299;
    }
}
