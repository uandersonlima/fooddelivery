using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace fooddelivery.Models
{
    public class Category
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }

        public List<Food> Foods { get; set; }
    }
}