namespace appMoteleros.Backend.Models
{
    using System.Web;
    using appMoteleros.Common.Models;

    public class ClientView : Client
    {
        public HttpPostedFileBase ImageFile { get; set; }
    }
}