using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Layer.Entities
{
   public class Cart
    {
        public int Id { get; set; }
        public List<CartItem> CartItems { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        #endregion

        #region Navigation Property
        public User User { get; set; }
        #endregion
    }
}
