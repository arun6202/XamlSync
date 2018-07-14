using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using XamlSync.Models;

namespace XamlSync.Controllers
{

    public class XamlSync6202Controller : ApiController
    {
        public static XamlPayload XamlPayload = new XamlPayload();

        // GET api/XamlSync6202
        [HttpGet]
        public XamlPayload Get()
        {
            return XamlPayload;
        }

        [HttpPost]
        public void Post([FromBody]XamlPayload xamlPayload)
        {
            XamlPayload = xamlPayload;
        }

    }

}
