using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Microsoft.EntityFrameworkCore;
using Bangazon.Models;
using Microsoft.AspNetCore.Http;
using System.Net; 
using System.Net.Http; 
//bangazon.data now it knows where to go to get the informatoin 

namespace BangazonAPI.Controllers
{
    //entity framework - interacting with your database without hitting your database at all - the middle man between the models (classes) - entity framework handles the translation between all of the different databases with different standards. All of this is under a library in .Net. ORM object relational mapping.
    //DB context
    [ProducesAttribute("application/json")]
    //all will product json 
    [Route("[controller]")]
    //localhost5000/customers is the route
    public class CustomersController : Controller
    {
        private BangazonContext context;

        public CustomersController(BangazonContext ctx)
        {
            //a controller will have the context which is the database you're hitting
            context = ctx;
        }
        // GET api/values
        [HttpGet]
       
        public IActionResult Get()
        {
            IQueryable<object> customers = from customer in context.Customer select customer;
            //out interface to the database is in context right now. context.Customer is the customer in the DB
            //from the customer table select everything, then hold it inside the customers variable. At the end it will hold a collection of customers that are in the database. If there are none, it will be null. Then the code below runs. 

            if (customers == null)
            {
                return NotFound();
                //NotFound() is a helper function. It is a valid 404 response back to the client. 
            }

            return Ok(customers);
            //put all the customers to the body of the response and send it back to the client

        }

        // GET api/values/5
        
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // returns a single customer 
                Customer customer = context.Customer.Single(m => m.CustomerId == id);

                if (customer == null)
                {
                    return NotFound();
                }
                
                return Ok(customer);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }
        // POST api/values
        
        [HttpPost]

    //   !ModelState is comparing against all your annotations, etc. 

        public IActionResult Post([FromBody] Customer customer)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
// 
            context.Customer.Add(customer); 
            //    db.EmployeeInfoes.AddObject(employeeinfo);

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CustomerExists(customer.CustomerId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetCustomer", new { id = customer.CustomerId }, customer);
        }
// the above defers to the GET method/handler further up. 
        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]Customer customer)
        {
            if (ModelState.IsValid && id == customer.CustomerId)
            {
            context.Customer.Attach(customer);
                try 
                {
                    context.SaveChanges();
                }
                catch 
                {
                    if (CustomerExists(customer.CustomerId))
                    {
                        Ok(customer);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }
        




        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
    {
        Customer customer = context.Customer.Single(m => m.CustomerId == id);

        context.Customer.Remove(customer);
        try
        {
        context.SaveChanges();
        }
        catch 
        {
            if (customer != null) 
            {
                Ok(customer);
            }
            else 
            {
                throw; 
            }
        }
    }
          private bool CustomerExists(int id)
        {
            return context.Customer.Count(e => e.CustomerId == id) > 0;
        }
    }
} 