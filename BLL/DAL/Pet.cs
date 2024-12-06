#nullable disable
using BLL;
using System.ComponentModel.DataAnnotations;

namespace BLL.DAL
{
    public class Pet
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        public bool? IsFemale { get; set; }

        public decimal? Age { get; set; }
        public decimal Weight { get; set; }
        public int? SpeciesId { get; set; }
        public Species Species { get; set; }

        public int? UserId { get; set; }
        public User User { get; set; }
    }
}
