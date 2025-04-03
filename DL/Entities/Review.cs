using System.ComponentModel.DataAnnotations.Schema;
namespace Data_Layer.Entities
{
    public class Review
    {
        public int Id { get; set; }
        public int Rating { get; set; }
        public string? Comment { get; set; }
        public DateTime ReviewDate { get; set; }

        #region Foreign Key
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Product))]
        public int ProductId { get; set; }
        #endregion

        #region Navigation Property
        public User User { get; set; }

        public Product Product { get; set; }
        #endregion
    }
}
