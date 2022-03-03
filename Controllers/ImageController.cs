
﻿
using AstroBackEnd.Services.Core;
using AstroBackEnd.RequestModels;


using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Security.Claims;
using AstroBackEnd.Repositories;
using AstroBackEnd.Models;
using Microsoft.AspNetCore.Authorization;
using AstroBackEnd.RequestModels.ImageRequest;
using AstroBackEnd.ViewsModel;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/image")]
    [ApiController]
    public class ImageController : ControllerBase
    {

        private IImageService _Service;
        private IUnitOfWork _work;
        public ImageController(IImageService service, IUnitOfWork work)
        {
            this._Service = service;
            this._work = work;
        }
        [HttpGet("{id}")]
        public IActionResult GetImage(int id)
        {
            var image = _Service.GetImage(id);
            if (image != null)
            {
                return Ok(image);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateImage([FromBody] ImageCreateRequest request)
        {
            return Ok(_Service.CreateImage(request));
        }

        [HttpGet]
        public IActionResult FindImage(int id, string link,int productId, string sortBy, int page = 1, int pageSize = 20)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindImageRequest findImageRequest = new FindImageRequest()
                {
                    Id = id,
                    Link = link,
                    ProductId=productId
                };
                return Ok(_Service.FindImage(findImageRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateImage(int id, ImageUpdateRequest request)
        {
            ImgLink updateImage = _Service.UpdateImage(id, request);

            return Ok(updateImage);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            ImgLink imgLink = _work.Image.Get(id);
            if (imgLink != null)
            {
                _Service.DeleteImage(id);
                return Ok(imgLink);
            }
            else
            {
                return NotFound();
            }
        }

        private readonly IImageService _imageService;

    }
}
