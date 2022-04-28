using Business.Abstract;
using Business.Concrete;
using DataAccess.Concrete.EntitiyFramework;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //bir class ile ilgili bilgi verme imzalama vs 
    public class ProductsController : ControllerBase
    {
        //Loosely Coupled (Gevşek Bağımlılık)
        private IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var data = _productService.GetAll();

            if (data.succes)
            {
                return Ok(data.Data);
            }
            else
            {
                //return BadRequest(data.message);
                return BadRequest(data);
            }
        }
        [HttpPost("addproduct")]
        public IActionResult Add(Product datas)
        {
            var result = _productService.Add(datas);

            if (result.succes)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        [HttpGet("getbyid")]
        public IActionResult GetById(int id)
        {
            var result = _productService.GetById(id);

            if (result.succes)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
    }
}
