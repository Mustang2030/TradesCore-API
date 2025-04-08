using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
{
    public class OrderItems
    {
        public int Quantity { get; set; }
        public double Price { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(Order))]
        public string OrderId { get; set; }

        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        #endregion

        #region Navigation Properties
        public Order Order { get; set; }
        public Product Product { get; set; }
        public Payment Payment { get; set; }
        #endregion
    }
}
