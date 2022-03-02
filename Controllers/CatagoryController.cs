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
using AstroBackEnd.RequestModels.CatagoryRequest;
using AstroBackEnd.ViewsModel;
using System.ComponentModel.DataAnnotations;
using AstroBackEnd.Utilities;

namespace AstroBackEnd.Controllers
{
    [Route("api/v1/catagory")]
    [ApiController]
    public class CatagoryController : ControllerBase
    {
        private ICatagorysService _Service;
        private IUnitOfWork _work;
        public CatagoryController(ICatagorysService service, IUnitOfWork work)
        {
            this._Service = service;
            this._work = work;
        }

        
        [HttpGet("{id}")]
        public IActionResult GetCatagory(int id)
        {
            var product = _Service.GetCatagory(id);
            if (product != null)
            {
                return Ok(product);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] CatagoryCreateRequest request)
        {
            try
            {
                Validation.Validate(request);
                return Ok(_Service.CreateCatagory(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
            
        }

        [HttpGet]
        //[Route("catagory")]
        public IActionResult FindCatagory(int id, string name, string sortBy, int page, int pageSize)
        {
            try
            {
                PagingRequest pagingRequest = new PagingRequest()
                {
                    SortBy = sortBy,
                    Page = page,
                    PageSize = pageSize,
                };
                FindCatagoryRequest findCatagoryRequest = new FindCatagoryRequest()
                {
                    Id=id,
                    Name = name
                };
                return Ok(_Service.FindCatagory(findCatagoryRequest));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, CatagoryUpdateRequest request)
        {
            Catagory updateCatagory = _Service.UpdateCatagory(id, request);

            return Ok(updateCatagory);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCatagory(int id)
        {
            //_Service.DeleteCatagory(id);
            //return Ok();
            Catagory catagory = _work.Catagorys.Get(id);
            if (catagory!=null)
            {
                _Service.DeleteCatagory(id);
                return Ok(catagory);
            }
            else
            {
                return NotFound();
            }
        }

    }

    
}