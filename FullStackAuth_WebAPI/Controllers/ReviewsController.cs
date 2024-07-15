using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST api/reviews
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Review data)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                data.UserId = userId;

                _context.Reviews.Add(data);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _context.SaveChanges();

                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Bonus
        // PUT api/reviews/5
        //[HttpPut("{id}"), Authorize]
        //public IActionResult Put(int id, [FromBody] Review data)
        //{
        //    try
        //    {
        //        Review review = _context.Reviews
        //            .Include(c => c.User).FirstOrDefault(c => c.Id == id);

        //        if (review == null)
        //        {
        //            return NotFound();
        //        }

        //        var userId = User.FindFirstValue("id");
        //        if (string.IsNullOrEmpty(userId) || review.UserId != userId)
        //        {

        //            return Unauthorized();
        //        }

        //        review.UserId = userId;
        //        review.User = _context.Users.Find(userId);
        //        review.Text = data.Text;
        //        review.Rating = data.Rating;
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }
        //        _context.SaveChanges();
        //        return StatusCode(201, review);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}

        // DELETE api/reviews/5
        //[HttpDelete("{id}"), Authorize]
        //public IActionResult Delete(int id)
        //{
        //    Review review = _context.Reviews.FirstOrDefault(c => c.Id == id);
        //    if (review == null)
        //    {
        //        return NotFound();
        //    }
        //    _context.Reviews.Remove(review);
        //    return StatusCode(204);


        //}
    }
}
