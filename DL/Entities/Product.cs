using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ImageUrl { get; set; }
        public int MyProperty { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(Category))]
        public string CategoryId { get; set; }

        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        #endregion

        #region Navigation Properties
        public Category Category { get; set; }
        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        public List<Review> Reviews { get; set; }
        #endregion
    }
}
