using AutoMapper;
using ProductManagementSystem.Domain._SharedKernel;
using ProductManagementSystem.Domain.Base.Dto;
using ProductManagementSystem.Domain.Product.Dto;
using ProductManagementSystem.Domain.Product.Entity;
using ProductManagementSystem.Domain.Product.Repository;
using ProductManagementSystem.Infrastructure.Base;
using ProductManagementSystem.Infrastructure.Context.ProductContext;
using System.Linq.Expressions;


namespace pro.Infrastructure.Repositories
{
    public class ProductRepository :EntityRepository<Product>, IProductRepository
    {
        private readonly IMapper _mapper;
        public ProductRepository(AppDatabaseContext appDbContext,IMapper mapper) : base(appDbContext,mapper)
        {
            _mapper = mapper;
        }

        public IUnitOfWork UnitOfWork => AppDbContext;

        public void AddProduct(Product product)
        {
            Create(product);
        }

        public void UpdateProduct(Product product)
        {
            Update(product);
        }
        public void DeleteProduct(Product product)
        {
            Delete(product);
        }

        public async Task<Product> GetProductById(long id)
        =>await FirstOrDefaultAsyncWithTracking(p => p.Id == id);


        public async Task<ProductDto> GetProductDtoById(long id)
         => _mapper.Map<ProductDto>(await FirstOrDefaultAsyncWithNoTracking(p => p.Id == id));

        public async Task<PageList<ProductDto>> GetProductListAsync(SearchProductDto requestDto)
        {
            PageList<ProductDto> result = new();

            #region prepare filter
            Expression<Func<Product, bool>> filter = p => (string.IsNullOrEmpty(requestDto.Filter.Name)
            || p.Name.ToLower().Contains(requestDto.Filter.Name.Trim().ToLower()));
            #endregion

            #region prepare sorting
            List<Product> productList = requestDto.Sorting.SortingColumn switch
            {
                "name" => await SearchPage(requestDto.Paginator.PageNumber, requestDto.Paginator.PageSize, requestDto.Sorting.SortingDirection, x => x.Name, filter),
                "description"=> await SearchPage(requestDto.Paginator.PageNumber, requestDto.Paginator.PageSize, requestDto.Sorting.SortingDirection, x => x.Description, filter),
                "price" => await SearchPage(requestDto.Paginator.PageNumber, requestDto.Paginator.PageSize, requestDto.Sorting.SortingDirection, x => x.Price, filter),
                "creationDate" => await SearchPage(requestDto.Paginator.PageNumber, requestDto.Paginator.PageSize, requestDto.Sorting.SortingDirection, x => x.CreationDate, filter),
                _ => await SearchPage(requestDto.Paginator.PageNumber, requestDto.Paginator.PageSize, requestDto.Sorting.SortingDirection, x => x.Id, filter),
            };

            #endregion


            #region mapping
            if (productList?.Count > default(int))
            {
                long totalCount = await GetTotalCount(filter);
                result.SetResult(Mapper.Map<List<Product>, List<ProductDto>>(productList), totalCount);
            }
            #endregion

            return result;

        }

        public async Task<bool> CheckUniqueName(string name, long id)
            =>await GetAnyAsync(x=>x.Id!=id&&x.Name.ToLower().Contains(name.Trim().ToLower()));

        public async Task<bool> CheckExistence(long id)
            => await GetAnyAsync(x => x.Id == id);
    }
}
