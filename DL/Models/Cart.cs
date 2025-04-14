namespace Data_Layer.Models
{
   public class Cart
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        #region Foreign Key
        public string UserId { get; set; }
        #endregion

        #region Navigation Property
        public User User { get; set; }
        public List<Product> Items { get;set; }
        #endregion
    }
}
