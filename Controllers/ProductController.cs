using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;

namespace BangazonAPI.Controllers
{

    [ProducesAttribute("application/json")]
    //all will product json 
    [Route("[controller]")]

      public class ProductController : Controller
    {
                private BangazonContext context;
                  public ProductController(BangazonContext ctx)
        {
            //a controller will have the context which is the database you're hitting
            context = ctx;
        }


    [HttpGet]
       
        public IActionResult Get()
        {
            IQueryable<object> products = from product in context.Product select product;
            //out interface to the database is in context right now. context.Product is the customer in the DB
            //from the product table select everything, then hold it inside the products variable. At the end it will hold a collection of customers that are in the database. If there are none, it will be null. Then the code below runs. 

            if (products == null)
            {
                return NotFound();
                //NotFound() is a helper function. It is a valid 404 response back to the client. 
            }

            return Ok(products);
            //put all the products to the body of the response and send it back to the client

        }

        // GET api/values/5
        
        [HttpGet("{id}", Name = "GetProduct")]
             public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // returns a single product  
                Product product = context.Product.Single(m => m.ProductId == id);

                if (product == null)
                {
                    return NotFound();
                }
                
                return Ok(product);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }
        [HttpPost]

    //   !ModelState is comparing against all your annotations, etc. 

        public IActionResult Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
// 
            context.Product.Add(product);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProductExists(product.ProductId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetProduct", new { id = product.ProductId }, product);
        }

              [HttpDelete("{id}")]
        public void Delete(int id)
    {
        Product product = context.Product.Single(m => m.ProductId == id);

        context.Product.Remove(product);
        try
        {
        context.SaveChanges();
        }
        catch 
        {
            if (product != null) 
            {
                Ok(product);
            }
            else 
            {
                throw; 
            }
        }
    }
         private bool ProductExists(int id)
        {
            return context.Product.Count(e => e.ProductId == id) > 0;
        }
    }
}

