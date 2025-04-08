using System.ComponentModel.DataAnnotations.Schema;

namespace Data_Layer.Models
{
    public class Review
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }

        [ForeignKey(nameof(Product))]
        public string ProductId { get; set; }
        #endregion

        #region Navigation Property
        public User User { get; set; }

        public Product? Product { get; set; }
        #endregion
    }
}
