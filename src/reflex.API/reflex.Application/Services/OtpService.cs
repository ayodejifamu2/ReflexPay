using reflex.Domain;
using reflex.Domain.DTO;
using reflex.Domain.Interface;
using reflex.Domain.Interface.ServiceInterface;
using reflex.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace reflex.Application.Services
{
    public class OtpService : IOtpService
    {
        private readonly IOtpRepository _repository;
        private readonly ICustomerRepository _custRepo;
        public OtpService(IOtpRepository repository, ICustomerRepository custRepo)
        {
            _repository = repository;
            _custRepo = custRepo;
        }

        
        public Task<BaseResponse> VerifyEmail(string email, string verificationCode)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse> VerifyKYC(string userId, KycDTO kycInfo)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse> VerifyOtp(int userId, OtpType otptype, string otp)
        {
            BaseResponse res = new();
            var users = await _custRepo.GetByIdAsync(userId);
            if (users == null)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "No Customer Found";
                return res;
            }
            var userOtp = await _repository.FindAsync(x => x.userId == userId & x.otpType == otptype);
            if (userOtp == null)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.BadRequest.ToString();
                res.Message = "Request Otp Again";
                return res;
            }
            var otpData = userOtp.OrderByDescending(x => x.ExpiresAt).FirstOrDefault();
            if(DateTime.Now > otpData.ExpiresAt)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.Forbidden.ToString();
                res.Message = "Otp has Expired";
                return res;
            }
            if (otpData.otp != otp)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.BadRequest.ToString();
                res.Message = "Invalid Otp";
                return res;
            }
            res.Message = "Success";
            res.data = true;
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;
        }

        public async Task<BaseResponse> VerifyPhoneNumber(string phoneNumber, string verificationCode)
        {
            BaseResponse res = new();
            var users = await _custRepo.FindAsync(x => x.customerPhoneNumber == phoneNumber);
            if (users == null)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "No Customer Found";
                return res;
            }
            var user = users.FirstOrDefault();
            //Mock Verification Service
            //
            if (verificationCode != "123456")
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.BadRequest.ToString();
                res.Message = "Invalid Verification Code, Verification Failed";
                return res;
            }
            Otp otp = new Otp()
            {
                createdAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(5),
                otp = "123456",
                otpType = OtpType.phoneNumberVerification,
                userId = user.id
            };

            await _repository.AddAsync(otp);
            res.Message = "Success";
            res.data = otp;
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;
        }
    }
}
