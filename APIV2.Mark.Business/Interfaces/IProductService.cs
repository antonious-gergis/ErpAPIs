
using APIV2.Mark.Database.Models;
using APIV2.Mark.Entities.Dtos;

namespace APIV2.Mark.Business.Interfaces
{
    public interface IProductService
    {
        ApiResponse<Product> GetItem(int id);
        ApiResponse<List<Product>> GetItems();
        ApiResponse<bool> Create(Product product);

        ApiResponse<bool> Edit(Product product);

        ApiResponse<bool> Delete(int id);

        public bool IsItemExists(string nameEn, string nameAr, string barcode, string sku);
        public bool IsItemExists(string nameEn, string nameAr, string barcode, string sku, long id);
        ApiResponse<TotalDetailsResponse<List<Product>>> GetListProducts(Param param);
        ApiResponse<List<Unit>> GetListUnits();
        ApiResponse<List<Category>> GetListCategories();
        ApiResponse<bool> UploadProducts(List<ProductDto> products);
    }
}
