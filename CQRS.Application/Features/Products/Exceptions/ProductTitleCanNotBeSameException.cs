using CQRS.Application.Bases;

namespace CQRS.Application.Features.Products.Exceptions
{
    public class ProductTitleCanNotBeSameException : BaseExceptions
    {
        public ProductTitleCanNotBeSameException() : base("Ürün başlığı zaten var!") { }
    }
}
