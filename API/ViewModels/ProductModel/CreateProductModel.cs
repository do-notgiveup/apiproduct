using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.ProductModel
{
    public class CreateProductModel
    {
        [MaxLength(255, ErrorMessage = "Tên sản phẩm không thể vượt quá 255 ký tự")]
        public string ProductName { get; set; }

        public int CateId { get; set; }
        [Range(0, double.MaxValue, ErrorMessage = "Giá sản phẩm không được là số âm")]
        public double Price { get; set; }
    }
}
