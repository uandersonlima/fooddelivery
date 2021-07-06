using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace fooddelivery.Models
{
    public class Image
    {
        [Key]
        public ulong Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public byte[] Data { get; set; }
        public string Size { get; set; }

        [ForeignKey("Food")]
        public ulong FoodId { get; set; }
        public Food Food { get; set; }
    }
}