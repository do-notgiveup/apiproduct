using System.ComponentModel.DataAnnotations;

namespace API.ViewModels.CategoryModel
{
    public class CreateCategoryModel
    {
        [MaxLength(255, ErrorMessage = "Tên loại không thể vượt quá 255 ký tự")]
        public string CateName { get; set; }
    }
}
