using CQRS.Application.Interfaces.AutoMapper;
using CQRS.Application.Interfaces.UnitOfWorks;
using CQRS.Domain.Entities;
using MediatR;

namespace CQRS.Application.Features.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommandRequest>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.GetReadRepository<Product>().GetAsync(x => x.Id == request.Id);

            var mappedProducts = mapper.Map<Product, UpdateProductCommandRequest>(request);

            var productCategories = await unitOfWork.GetReadRepository<ProductCategory>().GetAllAsync(x => x.ProductId == product.Id);

            await unitOfWork.GetWriteRepository<ProductCategory>().HardDeleteRangeAsync(productCategories);

            foreach (var categoryId in request.CategoryIds)
                await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new()
                {
                    CategoryId = categoryId,
                    ProductId = product.Id
                });

            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(mappedProducts);

            await unitOfWork.SaveAsync();
        }
    }
}
