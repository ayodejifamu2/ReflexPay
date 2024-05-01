using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reflex.Application.Services;
using reflex.Domain;
using reflex.Domain.DTO;
using reflex.Domain.Interface.ServiceInterface;

namespace reflex.API.Controllers
{
    [EnableCors("ALLOW")]
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly IOtpService _otpService;
        public VerifyController(IOtpService otpService)
        {
            _otpService = otpService;
        }

        [HttpPost("VerifyPhoneNumber/{phoneNumber}")]
        public async Task<IActionResult> VerifyPhoneNumber(string phoneNumber, string verificationCode)
        {
            var res = await _otpService.VerifyPhoneNumber(phoneNumber, verificationCode);
            return Ok(res);
        }

        [HttpPost("confirmOtp/{userId}")]
        public async Task<IActionResult> ConfirmOtp(int userId, string otp, OtpType otptype)
        {
            var res = await _otpService.VerifyOtp(userId, otptype, otp);
            return Ok(res);
        }
    }
}
