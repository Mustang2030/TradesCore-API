using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
{
    public class CartItems
    {
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
