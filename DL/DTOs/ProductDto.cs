using Data_Layer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; }
        #endregion
    }
}
