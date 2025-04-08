using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
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

        public List<OrderItems> OrderItems { get; set; }
        public Payment Payment { get; set; }
        #endregion
    }
}
