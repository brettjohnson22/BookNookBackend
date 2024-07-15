using FullStackAuth_WebAPI.Models;
using System.Collections.Generic;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class BookDetailsDTO
    {
        public List<ReviewWithUserDto> Reviews { get; set; }
        public double AverageRating { get; set; }
        public bool Favorited { get; set; }
    }
}
