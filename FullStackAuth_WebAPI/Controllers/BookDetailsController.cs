using FullStackAuth_WebAPI.Data;
using FullStackAuth_WebAPI.DataTransferObjects;
using FullStackAuth_WebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FullStackAuth_WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookDetailsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public BookDetailsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet("{bookId}")]
        public IActionResult Get(string bookId)
        {
            List<ReviewWithUserDto> reviews = _context.Reviews
                .Where(b => b.BookId == bookId).Include(b => b.User).Select(r => new ReviewWithUserDto
                {
                    Id = r.Id,
                    BookId = r.BookId,
                    Text = r.Text,
                    Rating = r.Rating,
                    User = new UserForDisplayDto
                    {
                        Id = r.User.Id,
                        FirstName = r.User.FirstName,
                        LastName = r.User.LastName,
                        UserName = r.User.UserName
                    }
                }).ToList();

            bool isFav = false;
            var userId = User.FindFirstValue("id");

            if(userId != null)
            {
                isFav = _context.Favorites.Where(f => f.UserId == userId).Select(f => f.BookId).ToList().Contains(bookId);
            }
     

            BookDetailsDTO customResponse = new BookDetailsDTO
            {
                Reviews = reviews,
                AverageRating = reviews.Select(r => r.Rating).Sum() / reviews.Count(),
                Favorited = isFav
            };

            return StatusCode(200, customResponse);
        }
    }
}
