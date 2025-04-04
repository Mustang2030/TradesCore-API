using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
    public class OrderItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
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
