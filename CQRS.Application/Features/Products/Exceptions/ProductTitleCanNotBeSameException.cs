using CQRS.Application.Bases;

namespace CQRS.Application.Features.Products.Exceptions
{
    public class ProductTitleCanNotBeSameException : BaseException
    {
        public ProductTitleCanNotBeSameException() : base("Ürün başlığı zaten var!") { }
    }
}
