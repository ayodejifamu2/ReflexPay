using reflex.Application.Utilities;
using reflex.Domain;
using reflex.Domain.DTO;
using reflex.Domain.Interface;
using reflex.Domain.Interface.ServiceInterface;
using reflex.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata;
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

        
        public async Task<BaseResponse> VerifyEmail(string email)
        {
            BaseResponse res = new();
            var users = await _custRepo.FindAsync(x => x.emailAddress == email);
            if (users == null)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "No Customer Found";
                return res;
            }
            var user = users.FirstOrDefault();

            Otp otp = new Otp()
            {
                createdAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(5),
                otp = Extension.GenerateOTP(),
                otpType = OtpType.emailVerification,
                userId = user.id
            };
            //An Email Serices that Sends the Otp
            await _repository.AddAsync(otp);
            res.Message = "Success";
            res.data = otp;
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;

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

            //update the Otp Information
            await UpdateOtpType(users, otptype);
            res.Message = "Success";
            res.data = true;
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;
        }

        public async Task<BaseResponse> VerifyPhoneNumber(string phoneNumber)
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

            Otp otp = new Otp()
            {
                createdAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(5),
                otp = Extension.GenerateOTP(),
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

        private async Task UpdateOtpType(Customer cust, OtpType otp)
        {
            if (otp == OtpType.emailVerification)
            {
                cust.emailVerifiedDate = DateTime.Now;
                cust.isEmailVerified = true;
                await _custRepo.Update(cust);
            }
            else if (otp == OtpType.phoneNumberVerification)
            {
                cust.phoneNumVerifiedDate = DateTime.Now;
                cust.isPhoneNumVerified = true;
                await _custRepo.Update(cust);
            }
            else if (otp == OtpType.passwordReset)
            {
                cust.passwordUpdatedAt = DateTime.Now;
                await _custRepo.Update(cust);
            }
        }
    }
}
