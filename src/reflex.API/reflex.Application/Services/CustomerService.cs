using Newtonsoft.Json;
using reflex.Application.Services.ExternalService;
using reflex.Application.Utilities;
using reflex.Domain;
using reflex.Domain.DTO;
using reflex.Domain.Entities;
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
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICardRepository _cardRepository;
        private readonly IOtpRepository _otpRepository;
        public CustomerService(ICustomerRepository customerRepository, ICardRepository cardRepository,
            IOtpRepository otpRepository)
        {
            _customerRepository = customerRepository;
            _cardRepository = cardRepository;
            _otpRepository = otpRepository;
        }


        public async Task<BaseResponse> AddCard(int userId, CardDTO cardInfo)
        {
            BaseResponse res = new();
            var user = await _customerRepository.GetByIdAsync(userId);
            if (user == null)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "Invalid UserID";
                return res;
            }

            var splitted = cardInfo.ExpiryDate.Split("/");
            var newDate = splitted[0] + "/20" + splitted[1];

            if (Extension.IsValidCVV(cardInfo.CVV) == false || Extension.IsValidExpiryDate(newDate) == false)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.BadRequest.ToString();
                res.Message = "Invalid Card Data";
                return res;
            }
           
            var cardString = JsonConvert.SerializeObject(cardInfo);
            var encryptedCardInfo = EncryptionService.Encrypt(cardString);
            CardInfo newCard = new()
            {
                UserId = user.id,
                EncrypytedCardinfo = encryptedCardInfo
            };
            await _cardRepository.AddAsync(newCard);
            res.Message = "Success";
            res.data = newCard;
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;
        }

        public async Task<BaseResponse> CreateLoginPin(int userId, LoginPinDTO model)
        {
            BaseResponse res = new();
            var user = await _customerRepository.GetByIdAsync(userId);
            if (user == null)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "Invalid UserID";
                return res;
            }

            if (model.LoginPin != model.ConfirmLoginPin)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "Login Pin Mismatch. Try Again";
                return res;
            }
            user.loginPin = PasswordHasher.Hash(model.LoginPin.ToString());
            user.loginPinCreatedAt = DateTime.Now;
            user.loginPinUpdateAt = DateTime.Now;
            await _customerRepository.Update(user);

            res.Message = "Success";
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;
        }

        public async Task<BaseResponse> CreateTransactionPin(int userId, TransactionPinDTO model)
        {
            BaseResponse res = new();
            var user = await _customerRepository.GetByIdAsync(userId);
            if (user == null)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "Invalid UserID";
                return res;
            }

            if (model.Pin != model.ConfirmPin)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "Transaction Pin Mismatch. Try Again";
                return res;
            }
            user.transactionPin = PasswordHasher.Hash(model.Pin.ToString());
            user.transactionPinCreatedAt = DateTime.Now;
            user.transactionPinUpdateAt = DateTime.Now;
            await _customerRepository.Update(user);

            res.Message = "Success";
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;

        }

        public async Task<BaseResponse> Login(LoginDTO model)
        {
            BaseResponse res = new();
            var user = await _customerRepository.FindAsync(x => x.customerPhoneNumber == model.PhoneNumber);
            if (user == null)
            {
                res.Status = true;
                res.ResponseCode = HttpStatusCode.NotFound.ToString();
                res.Message = "Login Failed, User Does Not Exist, Sign Up";
                return res;
            }
            Otp otp = new()
            {
                otpType = OtpType.multifactorAuthentication,
                createdAt = DateTime.Now,
                ExpiresAt = DateTime.Now.AddMinutes(5),
                userId = user.First().id,
                otp = Extension.GenerateOTP()
            };
            await _otpRepository.AddAsync(otp);
            res.Message = "Success";
            res.data = $"OTP SENT, {otp}";
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;

        }

        public async Task<BaseResponse> Signup(SignUpDTO model)
        {
            BaseResponse res = new();
            if (model == null)
            {
                res.Status = true;
                res.Message = "Fill User Details";
                res.ResponseCode = HttpStatusCode.BadRequest.ToString();
                return res;
            }
            var ifEmailExist = await _customerRepository.FindAsync(x => x.emailAddress == model.EmailAddress || x.userName == model.UserName);
            if (ifEmailExist.Count() > 0)
            {
                res.Status = true;
                res.Message = "Email or User Exist";
                res.ResponseCode = HttpStatusCode.BadRequest.ToString();
                return res;
            }

            Customer user = new Customer()
            {
                userName = model.UserName,
                emailAddress = model.EmailAddress,
                firstName = model.FirstName,
                lastName = model.LastName,
                customerPhoneNumber = model.PhoneNumber.ToString(),
                isAccountLocked = false,
                isPasswordSet = false
            };
            await _customerRepository.AddAsync(user);
            res.Message = "Success";
            res.data = user;
            res.ResponseCode = HttpStatusCode.OK.ToString();
            res.Status = true;
            return res;
        }

    }
}
