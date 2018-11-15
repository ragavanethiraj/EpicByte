using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EpicByteApi.Controllers
{
    public class FaceApiController : ApiController
    {
        protected FaceServiceClient faceServiceClient;
        public FaceApiController()
        {
            faceServiceClient = new FaceServiceClient(
                ConfigurationManager.AppSettings["FaceAPIKey"],
                ConfigurationManager.AppSettings["FaceAPIRoot"]);
        }
    }
}
