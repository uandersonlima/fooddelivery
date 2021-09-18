using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using fooddelivery.Models.Contracts;

namespace fooddelivery.Models
{
    public class Food
    {
        [Key]
        public ulong Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Price { get; set; }
        public bool IsAppetizer { get; set; }
        public bool Available { get; set; }

        public ulong CategoryId { get; set; }
        public Category Category { get; set; }


        public List<Suborder> Suborders { get; set; }
        public List<Image> Images { get; set; }
        public List<FoodIngredients> FoodIngredients { get; set; }

        public void AddIngredient(List<ulong> ingredientIds)
        {
            ingredientIds.ForEach(ingredientId => FoodIngredients.Add(new FoodIngredients { FoodId = Id, IngredientId = ingredientId }));
        }

        internal void RemoveIngredient(List<ulong> ingredientIds)
        {
            ingredientIds.ForEach(ingredientId =>
            {
                var relational = FoodIngredients.Find(x => x.IngredientId == ingredientId);

                if (relational == null)
                    throw new ArgumentException("O arquimento não conta na lista de ingredientes");

                FoodIngredients.Remove(relational);

            });
        }
    }
}