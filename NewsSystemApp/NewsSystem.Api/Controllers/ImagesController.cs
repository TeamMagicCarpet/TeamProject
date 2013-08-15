using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsSystem.Repositories;
using NewsSystem.Models;
using NewsSystem.Api.Models;
using System.Web;
using System.IO;
using DropNet;

namespace NewsSystem.Api.Controllers
{
    public class ImagesController : ApiController
    {
        private readonly IRepository<Image> imageRepository;
        private const string DropboxAppKey = "eo27ptjqc375r27";
        private const string DropboxAppSecret = "if6d3lj2bduyjtr";
        private const string DropboxUserKey = "dpxhb52urfjepf6d";
        private const string DropboxUserSecret = "9z32ciem95c0xfl";

        public ImagesController(IRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        [ActionName("getall")]
        public IEnumerable<ImageModel> GetAll()
        {
            var imageEntities = this.imageRepository.All();
            var imageModels =
                from userEntity in imageEntities
                select new ImageModel()
                {
                    ImageId = userEntity.ImageId,
                    Name = userEntity.Name,
                    Location = userEntity.Location
                };
            return imageModels.ToList();
        }

        [HttpPost]
        [ActionName("upload")]
        public HttpResponseMessage Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;

            if (httpRequest.Files.Count > 0)
            {
                List<string> imgUrls = new List<string>();

                foreach (string file in httpRequest.Files)
                {

                    var postedFile = httpRequest.Files[file];
                    if (!string.IsNullOrEmpty(postedFile.FileName))
                    {
                        string shareUrl = UploadInDropbox(postedFile.FileName, postedFile.InputStream, postedFile.ContentLength);
                        imgUrls.Add(shareUrl);
                    }
                }

                result = Request.CreateResponse(HttpStatusCode.Created, imgUrls);
            }
            else
            {
                result = Request.CreateResponse(HttpStatusCode.BadRequest);
            }

            return result;
        }

        private static string UploadInDropbox(string fileName, Stream stream, int fileSize)
        {
            DropNetClient client = new DropNetClient(DropboxAppKey, DropboxAppSecret, DropboxUserKey, DropboxUserSecret);
            client.UseSandbox = true;

            var bytes = new byte[fileSize];
            stream.Read(bytes, 0, fileSize);

            client.UploadFile("/", fileName, bytes);
            return client.GetMedia("/" + fileName).Url;
        }
    }
}
