using CQRS.Application.Bases;
using CQRS.Application.Features.Products.Exceptions;
using CQRS.Domain.Entities;

namespace CQRS.Application.Features.Products.Rules
{
    public class ProductRules : BaseRules
    {
        public Task ProductTitleCanNotBeSame(IList<Product> products, string requestTitle)
        {
            if (products.Any(x => x.Title == requestTitle)) throw new ProductTitleCanNotBeSameException();

            return Task.CompletedTask;
        }
    }
}
