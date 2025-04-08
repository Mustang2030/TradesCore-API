using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
{
   public class Cart
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public List<CartItems> CartItems { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        #endregion

        #region Navigation Property
        public User User { get; set; }
        #endregion
    }
}
