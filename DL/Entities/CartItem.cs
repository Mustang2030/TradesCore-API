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
        public int Id { get; set; }
        public int Quantity { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(Cart))]
        public int CartId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        #endregion

        #region Navigation Property
        public Cart Cart { get; set; }

        public Product Product { get; set; }
        #endregion
    }
}
