using EpicByteApi.Classes;
using Microsoft.ProjectOxford.Face;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;


namespace EpicByteApi.Controllers
{
    [RoutePrefix("api/Upload")]
    public class UploadController : ApiController
    {
        
            [Route("user/PostUserImage")]
            [AllowAnonymous]
            public async Task<HttpResponseMessage> PostUserImage()
            {
                Dictionary<string, object> dict = new Dictionary<string, object>();
                try
                {

                    var httpRequest = HttpContext.Current.Request;

                    foreach (string file in httpRequest.Files)
                    {
                        HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                        var postedFile = httpRequest.Files[file];
                        if (postedFile != null && postedFile.ContentLength > 0)
                        {

                            int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                            IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                            var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                            var extension = ext.ToLower();
                            if (!AllowedFileExtensions.Contains(extension))
                            {

                                var message = string.Format("Please Upload image of type .jpg,.gif,.png.");

                                dict.Add("error", message);
                                return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                            }
                            else if (postedFile.ContentLength > MaxContentLength)
                            {

                                var message = string.Format("Please Upload a file upto 1 mb.");

                                dict.Add("error", message);
                                return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                            }
                            else
                            {



                                var filePath = HttpContext.Current.Server.MapPath("~/Userimage/" + postedFile.FileName + extension);

                                postedFile.SaveAs(filePath);

                            }
                        }

                        var message1 = string.Format("Image Updated Successfully.");
                        return Request.CreateErrorResponse(HttpStatusCode.Created, message1); ;
                    }
                    var res = string.Format("Please Upload a image.");
                    dict.Add("error", res);
                    return Request.CreateResponse(HttpStatusCode.NotFound, dict);
                }
                catch (Exception ex)
                {
                    var res = string.Format("some Message");
                    dict.Add("error", res);
                    return Request.CreateResponse(HttpStatusCode.NotFound, dict);
                }
            }

        //public async Task<IHttpActionResult> UploadImage(string fileName = "")
        //{
        //    //Use a GUID in case the fileName is not specified
        //    if (fileName == "")
        //    {
        //        fileName = Guid.NewGuid().ToString();
        //    }
        //    //Check if submitted content is of MIME Multi Part Content with Form-data in it?
        //    if (!Request.Content.IsMimeMultipartContent("form-data"))
        //    {
        //        return BadRequest("Could not find file to upload");
        //    }

        //    //Read the content in a InMemory Muli-Part Form Data format
        //    var provider = await Request.Content.ReadAsMultipartAsync(new InMemoryMultipartFormDataStreamProvider());

        //    //Get the first file
        //    var files = provider.Files;
        //    var uploadedFile = files[0];

        //    //Extract the file extention
        //    var extension = ExtractExtension(uploadedFile);
        //    //Get the file's content type
        //    var contentType = uploadedFile.Headers.ContentType.ToString();

        //    //create the full name of the image with the fileName and extension
        //    var imageName = string.Concat(fileName, extension);

        //    //Initialise Blob and FaceApi connections
            
        //    var storageAccount = CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]); //Azure storage account connection
        //    var _faceServiceClient = new FaceServiceClient(ConfigurationManager.AppSettings["FaceAPIKey"]);  //FaceApi connection
        //    var blobClient = storageAccount.CreateCloudBlobClient();
        //    var anonContainer = blobClient.GetContainerReference("powerappimages"); //camera control images
        //    var d365Container = blobClient.GetContainerReference("entityimages"); //dynamics 365 contact images
        //    var contactId = Guid.Empty;
        //    double confidence = 0;
        //    Entity crmContact = null;

        //    var blockBlob = anonContainer.GetBlockBlobReference(imageName);
        //    blockBlob.Properties.ContentType = contentType;

        //    //Upload anonymous image from camera control to powerappimages blob
        //    using (var fileStream = await uploadedFile.ReadAsStreamAsync()) //as Stream is IDisposable
        //    {
        //        blockBlob.UploadFromStream(fileStream);
        //    }

        //    //Detect faces in the uploaded anonymous image
        //    Face[] anonymousfaces = await _faceServiceClient.DetectAsync(blockBlob.Uri.ToString(), returnFaceId: true, returnFaceLandmarks: true);

        //    //Iterate stored contact entity images and verify the identity
        //    foreach (IListBlobItem item in d365Container.ListBlobs(null, true))
        //    {
        //        CloudBlockBlob blob = (CloudBlockBlob)item;
        //        Face[] contactfaces = await _faceServiceClient.DetectAsync(blob.Uri.ToString(), returnFaceId: true, returnFaceLandmarks: true);
        //        VerifyResult result = await _faceServiceClient.VerifyAsync(anonymousfaces[0].FaceId, contactfaces[0].FaceId);
        //        if (result.IsIdentical)
        //        {
        //            //Face identified. Retrieve associated contact
        //            MatchCollection mc = Regex.Matches(blob.Uri.ToString(),
        //                @"([a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12})"); //strip contact id from image filename
        //            contactId = Guid.Parse(mc[0].ToString());
        //            confidence = Math.Round((result.Confidence * 100), 2);
        //            crmContact = GetContact(contactId);
        //            break;
        //        }
        //    }
        //    var fileInfo = new UploadedFileInfo
        //    {
        //        FileName = fileName,
        //        FileExtension = extension,
        //        ContentType = contentType,
        //        FileURL = blockBlob.Uri.ToString(),
        //        ContactId = contactId.ToString(),
        //        Confidence = confidence.ToString(),
        //        FirstName = crmContact?.GetAttributeValue<string>("firstname"),
        //        LastName = crmContact?.GetAttributeValue<string>("lastname"),
        //        StudentID = crmContact?.GetAttributeValue<string>("sms_studentid"),
        //    };
        //    return Ok(fileInfo);

        //}



    }
}
