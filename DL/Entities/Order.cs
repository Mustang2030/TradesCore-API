using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
   public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public DateTime OrderDate { get; set; }
        public double TotalAmount { get; set; }
        public string OrderStatus { get; set; } // Shipped, Pending, Delivered, Cancelled

        #region Foreign Key
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        #endregion

        #region Navigation Properties
        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
        public Payment Payment { get; set; }
        #endregion
    }
}
