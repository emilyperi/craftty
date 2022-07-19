using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Craftty.WebSite.Models;
using Craftty.WebSite.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Craftty.WebSite.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class ProductsController : ControllerBase
    {
        public ProductsController(JsonFileProductService productService)
        {
            this.ProductService = productService;
	    }

        public JsonFileProductService ProductService { get; }

        [HttpGet]
        public IEnumerable<Product> Get()
	    {
            return ProductService.GetProducts();
	    }

        [Route("Rate")]
        [HttpGet]
        public ActionResult Get([FromQuery] string ProductId, int rating) 
	    {
            ProductService.AddRating(ProductId, rating);
            return Ok();
	    }
    }
}

