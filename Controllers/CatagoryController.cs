
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

        [HttpGet]
        public IActionResult GetAllCatagory()
        {
            using(var sweph = new SwissEphNet.SwissEph())
            {
                
            }
            Func<Catagory, ViewsModel.CatagoryView> maping = catagory =>
            {
                return new CatagoryView()
                {
                    Id = catagory.Id,
                    //MasterProductId = product.MasterProduct.Id,
                    Name = catagory.Name

                };

            };
            return Ok(_Service.GetAllCatagory().Select(maping));
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
            ValidationContext vc = new ValidationContext(request);
            ICollection<ValidationResult> results = new List<ValidationResult>();
            Validator.TryValidateObject(request, vc, results);
            return Ok(_Service.CreateCatagory(request));
        }

        [HttpPost]
        [Route("findProduct")]
        public IActionResult FindCatagory(FindCatagoryRequest request)
        {
            Func<Catagory, ViewsModel.CatagoryView> maping = catagory =>
            {
                return new CatagoryView()
                {
                    Id = catagory.Id,
                    //MasterProductId = product.MasterProduct.Id,
                    Name = catagory.Name
                    
                };

            };
            return Ok(_Service.FindCatagory(request).Select(maping));
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, CatagoryUpdateRequest request)
        {
            Catagory updateCatagory = _Service.UpdateCatagory(id, request);

            return Ok(updateCatagory);
        }

    }

    
}
