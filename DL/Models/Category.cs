namespace Data_Layer.Models
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string? Name { get; set; }
        public List<Product> Products { get; set; }
    }   
}
