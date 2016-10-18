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

    [ProducesAttribute("application/json")]
    //all will product json 
    [Route("[controller]")]
    //localhost5000/LineItem is the route
    public class LineItemController : Controller
    {
        private BangazonContext context;

        public LineItemController(BangazonContext ctx)
        {
            //a controller will have the context which is the database you're hitting
            context = ctx;
        }

        [HttpGet]
       
        public IActionResult Get()
        {
            IQueryable<object> lineitems = from lineitem in context.LineItem select lineitem;
            //our interface to the database is in context right now. context.LineItem is the lineitem in the DB
            //from the lineitem table select everything, then hold it inside the lineitems variable. At the end it will hold a collection of customers that are in the database. If there are none, it will be null. Then the code below runs. 

            if (lineitems == null)
            {
                return NotFound();
                //NotFound() is a helper function. It is a valid 404 response back to the client. 
            }

            return Ok(lineitems);
            //put all the lineitems to the body of the response and send it back to the client

        }
         [HttpGet("{id}", Name = "GetLineItem")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                // returns a single lineitem  
                LineItem lineitem = context.LineItem.Single(m => m.LineItemId == id);

                if (lineitem == null)
                {
                    return NotFound();
                }
                
                return Ok(lineitem);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }


        }
        // POST api/values
        
        [HttpPost]

    //   !ModelState is comparing against all your annotations, etc. 

        public IActionResult Post([FromBody] LineItem lineitem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
// 
            context.LineItem.Add(lineitem);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LineItemExists(lineitem.LineItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetLineItem", new { id = lineitem.LineItemId }, lineitem);
        }
// the above defers to the GET method/handler further up. 
        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody]LineItem lineitem)
        {
           
         if (lineitem.LineItemId != id)
            {
                return BadRequest(ModelState);

            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {

                context.Update(lineitem);
                context.SaveChanges();

            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
            return Ok(lineitem);
        }

        // DELETE api/values/5
     
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)

    {
         if (!ModelState.IsValid)
            {
           return BadRequest(ModelState);
            }
            try
            {
            LineItem lineitem = context.LineItem.Single(m => m.LineItemId == id);

                if (lineitem == null)
                {
                    return NotFound();
                }
                context.LineItem.Remove(lineitem);
                context.SaveChanges();

                return Ok(lineitem);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }
          private bool LineItemExists(int id)
        {
            return context.LineItem.Count(e => e.LineItemId == id) > 0;
        }

    }
}