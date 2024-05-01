using reflex.Domain.DTO;
using reflex.Domain.Response;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.Interface.ServiceInterface
{
    public interface ICustomerService
    {
        Task<BaseResponse> CreateTransactionPin(int userId, TransactionPinDTO model);
        Task<BaseResponse> CreateLoginPin(int userId, LoginPinDTO model);
        Task<BaseResponse> AddCard(int userId, CardDTO cardInfo);
        Task<BaseResponse> Signup(SignUpDTO model);
        Task<BaseResponse> Login(LoginDTO model);
        
    }
}
