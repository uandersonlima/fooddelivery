using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddelivery.Models
{
    public class Image
    {
        [Key]
        public int Code { get; set; }
        public string Name { get; set; }
        public byte[] Data { get; set; }

        [ForeignKey("Food")]
        public int FoodCode { get; set; }
        public Food Food { get; set; }
    }
}