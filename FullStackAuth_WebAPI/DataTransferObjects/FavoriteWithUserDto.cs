using System.ComponentModel.DataAnnotations;

namespace FullStackAuth_WebAPI.DataTransferObjects
{
    public class FavoriteWithUserDto
    {
        public int Id { get; set; }
        public string BookId { get; set; }
        public string Title { get; set; }
        public string ThumbnailUrl { get; set; }
        public UserForDisplayDto User { get; set;}
    }
}
