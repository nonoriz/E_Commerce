

using E_Commerce.Application.Common;
using E_Commerce.Application.DTOs.Products;

namespace E_Commerce.Application.Contracts
{
    public interface IProductService
    {
        Task<Result<PaginatedResult<ProductDto>>> GetAllProductsAsync(ProductQueryParams queryParams,CancellationToken ct=default!);

        Task<Result<IReadOnlyList<BrandDto>>> GetAllBrandsAsync(CancellationToken ct=default!);

        Task<Result<IReadOnlyList< TypeDto>>> GetAllTypesAsync(CancellationToken ct=default!);

        Task<Result<ProductDto>> GetProductByIdAsync(int id , CancellationToken ct=default!);
    }
}
