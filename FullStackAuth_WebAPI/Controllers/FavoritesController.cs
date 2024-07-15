using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoritesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FavoritesController(ApplicationDbContext context)
        {
            _context = context;
        }
        // GET: api/favorites
        [HttpGet, Authorize]
        public IActionResult Get()
        {
            if (User.FindFirstValue("id") == null)
            {
                return Unauthorized();
            }
            List<Favorite> favorites = _context.Favorites.Where(f => f.UserId == User.FindFirstValue("id")).ToList();

            return StatusCode(200,favorites);
        }

        // POST api/favorites
        [HttpPost, Authorize]
        public IActionResult Post([FromBody] Favorite data)
        {
            try
            {
                string userId = User.FindFirstValue("id");

                if (string.IsNullOrEmpty(userId))
                {
                    return Unauthorized();
                }

                var checkIfAlreadyFavorited = _context.Favorites.Where(f => f.BookId == data.BookId).Where(f => f.UserId == userId).ToList();

                if (!checkIfAlreadyFavorited.Any())
                {
                    data.UserId = userId;
                    _context.Favorites.Add(data);
                    _context.SaveChanges();
                }
                else
                {
                    return Conflict(new { error = "Resource already exists", message = "That book is already favorited by this user." });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                return StatusCode(201, data);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        //Bonus
        // DELETE api/favorites/5
        //[HttpDelete("{bookId}"), Authorize]
        //public IActionResult Delete(string bookId)
        //{
        //    try
        //    {
        //        var userId = User.FindFirstValue("id");
        //        if (string.IsNullOrEmpty(userId))
        //        {
        //            return Unauthorized();
        //        }

        //        Favorite favorite = _context.Favorites.Where(c => c.BookId == bookId).Where(u => u.UserId == userId).FirstOrDefault();

        //        if (favorite == null)
        //        {
        //            return NotFound();
        //        }
                
        //        _context.Favorites.Remove(favorite);
        //        _context.SaveChanges();

        //        return StatusCode(204);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ex.Message);
        //    }
        //}
    }
}
