using CQRS.Application.Bases;

namespace CQRS.Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistException : BaseException
    {
        public UserAlreadyExistException() : base("Kullanıcı zaten oluşturulmuş!")
        {
        }
    }
}
