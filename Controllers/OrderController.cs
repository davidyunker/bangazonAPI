using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;
//bangazon.data now it knows where to go to get the informatoin 

namespace BangazonAPI.Controllers
{
    //entity framework - interacting with your database without hitting your database at all - the middle man between the models (classes) - entity framework handles the translation between all of the different databases with different standards. All of this is under a library in .Net. ORM object relational mapping.
    //DB context
    [ProducesAttribute("application/json")]
    //all will product json 
    [Route("[controller]")]
    //localhost5000/customers is the route
    public class OrderController : Controller
    {
        private BangazonContext context;

        public OrderController(BangazonContext ctx)
        {
            //a controller will have the context which is the database you're hitting
            context = ctx;
        }
        // GET api/values
        [HttpGet]
       
        public IActionResult Get()
        {
            IQueryable<object> orders = from order in context.Order select order;
            //out interface to the database is in context right now. context.Customer is the customer in the DB
            //from the customer table select everything, then hold it inside the customers variable. At the end it will hold a collection of customers that are in the database. If there are none, it will be null. Then the code below runs. 

            if (orders == null)
            {
                return NotFound();
                //NotFound() is a helper function. It is a valid 404 response back to the client. 
            }

            return Ok(orders);
            //put all the customers to the body of the response and send it back to the client

        }

        // GET api/values/5
        
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // returns a single customer 
                Order order = context.Order.Single(m => m.OrderId == id);

                if (order == null)
                {
                    return NotFound();
                }
                
                return Ok(order);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }
        // POST api/values
        
        [HttpPost]

    //   !ModelState is comparing against all your annotations, etc. 

        public IActionResult Post([FromBody] Order order)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
// 
            context.Order.Add(order);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (OrderExists(order.OrderId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetOrder", new { id = order.OrderId }, order);
        }
// the above defers to the GET method/handler further up. 
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Order order)
        {
            if (ModelState.IsValid && id == order.OrderId)
            {
                try 
                {
                    context.Update(order);
                    context.SaveChanges();
                }
                    catch (System.InvalidOperationException ex)
                {
                    NotFound();
                }
                Ok(order);
            }
        }

        // DELETE api/values/5
     
        [HttpDelete("{id}")]

        public void Delete(int id)
    {
        Order order = context.Order.Single(m => m.OrderId == id);

        context.Order.Remove(order);
        try
        {
        context.SaveChanges();
        }
        catch 
        {
            if (order != null) 
            {
                Ok(order);
            }
            else 
            {
                throw; 
            }
        }
    }
          private bool OrderExists(int id)
        {
            return context.Order.Count(e => e.OrderId == id) > 0;
        }
    }
} 