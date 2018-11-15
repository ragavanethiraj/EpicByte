using EpicByteApi.Classes;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EpicByteApi.Controllers
{

    [RoutePrefix("api/FaceDetect")]
    public class FaceDetectController : FaceApiController
    {

        private FaceAttributeType[] faceAttributeTypes;

        [HttpPost]
        [Route("user/detect")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Detect()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            try
            {
                var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

                var files = provider.Files;
                var file1 = files[0];
                var fileStream = await file1.ReadAsStreamAsync();


                var faces = await faceServiceClient.DetectAsync(fileStream, false, true, new FaceAttributeType[] { FaceAttributeType.Gender, FaceAttributeType.Age, FaceAttributeType.Smile, FaceAttributeType.Glasses, FaceAttributeType.HeadPose, FaceAttributeType.FacialHair, FaceAttributeType.Emotion, FaceAttributeType.Hair, FaceAttributeType.Makeup, FaceAttributeType.Occlusion, FaceAttributeType.Accessories, FaceAttributeType.Noise, FaceAttributeType.Exposure, FaceAttributeType.Blur });
                return Ok(faces);


            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


       

        [HttpPost]
        [Route("user/attribute")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Attribute()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest();
            }
            try
            {
                var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

                var files = provider.Files;
                var file1 = files[0];
                var fileStream = await file1.ReadAsStreamAsync();


                var faces = await faceServiceClient.DetectAsync(fileStream);
                return Ok(faces);


            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [HttpGet]
        [Route("user/verify")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Verify(string guid1, string guid2)
        {
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest();
            }
            try
            {
                var face1 = new Guid(guid1);
                var face2 = new Guid(guid2);

                var res = await faceServiceClient.VerifyAsync(face1, face2); 
                return Ok(res);


            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

    }
}
