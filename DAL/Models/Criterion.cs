using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DAL.Models
{
    public class Criterion
    {
        public int Id { get; set; }

		[Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

		[Required]
        [Display(Name = "Range")]
        public int Range { get; set; }

		[Required]
        [Display(Name = "Weight")]
        public int Weight { get; set; }

		[Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

		[Required]
        [Display(Name = "OptimType")]
        public string OptimType { get; set; }

		[Required]
        [Display(Name = "Edlzmer")]
        public string Edlzmer { get; set; }

		[Required]
        [Display(Name = "ScaleType")]
        public string ScaleType { get; set; }

        public virtual ICollection<Mark> Marks { get; set; }
    }
}