using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
    public class CartItem
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Quantity { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(Cart))]
        public string CartId { get; set; }

        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        #endregion

        #region Navigation Property
        public Cart Cart { get; set; }

        public Product Product { get; set; }
        #endregion
    }
}
