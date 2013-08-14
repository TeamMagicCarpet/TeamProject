using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using NewsSystem.Repositories;
using NewsSystem.Models;
using NewsSystem.Api.Models;

namespace NewsSystem.Api.Controllers
{
    public class ImagesController : ApiController
    {
        private readonly IRepository<Image> imageRepository;

        public ImagesController(IRepository<Image> imageRepository)
        {
            this.imageRepository = imageRepository;
        }


    }
}
