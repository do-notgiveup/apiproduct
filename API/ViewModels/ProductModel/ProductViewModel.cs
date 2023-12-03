namespace API.ViewModels.ProductModel
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string ProductName { get; set; } = null!;

        public int CateId { get; set; }

        public double? Price { get; set; }
        public string CateName { get; set; }
    }
}
