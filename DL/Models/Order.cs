using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
{
    public class Order
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        public DateTime OrderDate { get; set; } = DateTime.Now;

        public OrderStatus Status { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        #endregion

        #region Navigation Properties
        public User User { get; set; }

        public List<Product> Items { get; set; }

        public Payment Payment { get; set; }
        #endregion
    }
}
