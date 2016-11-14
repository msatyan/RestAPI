using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using MyRestAPI.Models;
using MyRestAPI.Services;


namespace MyRestAPI.Controllers
{
    
    [Route("api/[controller]")]
    public class MyProductsController : Controller
    {
        private readonly IMyProductsRepository m_productRepository;

        public MyProductsController(IMyProductsRepository productRepository)
        {
            m_productRepository = productRepository;
        }

        // SELECT ALL
        // GET api/myproducts
        [HttpGet]
        public IEnumerable<MyProducts> Get()
        {
            // TODO: Add fault tolerance
            var products = m_productRepository.GetAllMyProducts();
            return products;
        }


        // If you want to apply constrains to attribute route
        // Google it for ASP.NET MVC Attribute Routing Constrains.
        [Authorize]
        // SELECT ONE
        // GET api/myproducts/3
        [HttpGet("{id:int}")] // added validation (id should be int)
        [Produces("application/json", Type = typeof(MyProducts))] // Content Negotiation: restrict to JSON-formatted responses
        public MyProducts Get(int id)
        {
            // TODO: Add fault tolerance
            var product = m_productRepository.GetMyProducts(id);
            return product;
        }


        // CREATE NEW
        // POST api/myproducts
        [HttpPost]
        [Consumes("application/json")]
        public void Post([FromBody]MyProducts product)
        {
            // TODO: Add fault tolerance and return value
            if (ModelState.IsValid)
            {
                m_productRepository.AddMyProducts(product);
            }
        }

        // UPDATE
        // PUT api/myproducts/5
        [HttpPut("{id}")]
        [Consumes("application/json")]
        public void Put(int id, [FromBody]MyProducts product)
        {
            // TODO: Add fault tolerance and return value
            if (ModelState.IsValid)
            {
                m_productRepository.UpdateMyProducts(product);
            }

        }


        // DELETE api/myproducts/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            // TODO: Add fault tolerance and return value
            m_productRepository.DeleteMyProducts(id);
        }

    }
}

