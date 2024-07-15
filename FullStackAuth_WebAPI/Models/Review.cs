using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace FullStackAuth_WebAPI.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string BookId { get; set; }

        [Required]
        public string Text { get; set; }

        [Required]
        public int Rating { get; set; }

        [ForeignKey("User")]
        [BindNever]
        public string UserId { get; set; }
        [BindNever]
        public virtual User User { get; set; }
    }
}
