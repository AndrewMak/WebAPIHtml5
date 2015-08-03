using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.API.Controllers
{
    public class SenderController : ApiController
    {
        [HttpPost()]
        public string Sended()
        {
            string path = string.Empty;
            path = System.Web.Hosting.HostingEnvironment.MapPath("~/Files/");

            if (!new DirectoryInfo(path).Exists) Directory.CreateDirectory(path);

            System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files;
            try
            {
                for (int i = 0; i < hfc.Count; i++)
                {
                    System.Web.HttpPostedFile hpf = hfc[i];

                    if (hpf.ContentLength > 0)
                    {
                        if (!File.Exists(path + Path.GetFileName(hpf.FileName)))
                        {
                            hpf.SaveAs(path + Path.GetFileName(hpf.FileName));
                        }

                    }
                }
                return hfc.Count + "Arquivos enviados com sucesso";

                new HttpRequestMessage().CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception ex)
            {
                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.InternalServerError));
            }
            return string.Empty;
        }

        [HttpGet]
        public string Hello()
        {
            return "ola";
        }
    }
}
