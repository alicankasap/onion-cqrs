using CQRS.Application.DTO;
using CQRS.Application.Interfaces.AutoMapper;
using CQRS.Application.Interfaces.UnitOfWorks;
using CQRS.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CQRS.Application.Features.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQueryRequest, IList<GetAllProductsQueryResponse>>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public GetAllProductsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IList<GetAllProductsQueryResponse>> Handle(GetAllProductsQueryRequest request, CancellationToken cancellationToken)
        {
            var products = await unitOfWork.GetReadRepository<Product>().GetAllAsync(include: x => x.Include(b => b.Brand));

            var mappedBrand = mapper.Map<BrandDto, Brand>(new Brand());
            var mappedProducts = mapper.Map<GetAllProductsQueryResponse, Product>(products);

            foreach (var item in mappedProducts)
                item.Price -= (item.Price * item.Discount / 100);

            return mappedProducts;
        }
    }
}
