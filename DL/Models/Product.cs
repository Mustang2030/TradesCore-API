using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public int Stock { get; set; }

        public string ImageUrl { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        #endregion

        #region Navigation Properties
        public List<CategoryProduct> Categories { get; set; }

        public User User { get; set; }

        public List<OrderItems>? Orders { get; set; }

        public List<Review> Reviews { get; set; }
        #endregion
    }
}
