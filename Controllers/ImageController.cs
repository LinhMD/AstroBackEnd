
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
            try
            {
                return Ok(_Service.GetImage(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }           
        }

        [HttpPost]
        public IActionResult CreateImage([FromBody] ImageCreateRequest request)
        {
            try
            {
                return Ok(_Service.CreateImage(request));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
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
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateImage(int id, ImageUpdateRequest request)
        {
            try
            {
                ImgLink updateImage = _Service.UpdateImage(id, request);

                return Ok(updateImage);
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteImage(int id)
        {
            try
            {
                return Ok(_Service.DeleteImage(id));
            }
            catch (ArgumentException ex) { return BadRequest(ex.Message); }
        }
        private readonly IImageService _imageService;

    }
}
