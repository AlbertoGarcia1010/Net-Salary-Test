using System.Xml.Linq;

namespace Salary.App.Utils.Base.Response
{
    public class Response
    {
        public bool Success { get; set; }

        public Metadata Metadata { get; set; }
        public dynamic? Data { get; set; }

        public Response(bool success, Metadata metada)
        {
            Success = success;
            Metadata = metada;
        }
        public Response(bool success, Metadata metada, dynamic data)
        {
            Success = success;
            Metadata = metada;
            Data = data;
        }
    }
}
