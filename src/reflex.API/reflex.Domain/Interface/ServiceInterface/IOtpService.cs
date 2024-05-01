using reflex.Domain.DTO;
using reflex.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Domain.Interface.ServiceInterface
{
    public interface IOtpService 
    {
        Task<BaseResponse> VerifyKYC(string userId, KycDTO kycInfo);
        Task<BaseResponse> VerifyPhoneNumber(string phoneNumber, string verificationCode);
        Task<BaseResponse> VerifyEmail(string email, string verificationCode);
        Task<BaseResponse> VerifyOtp(int userId, OtpType otptype, string otp);
    }
}
