using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reflex.Domain.DTO;
using reflex.Domain.Interface.ServiceInterface;
using System.ComponentModel.DataAnnotations;

namespace reflex.API.Controllers
{

    [EnableCors("ALLOW")]
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public AuthController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody]SignUpDTO model)
        {
            var res = await _customerService.Signup(model);
            return Ok(res);
        }
        [HttpPost("Addcard/{userId}")]
        public async Task<IActionResult> AddNewcard([Required] int userId, [FromBody] CardDTO model)
        {
            var res = await _customerService.AddCard(userId, model);
            return Ok(res);
        }

        [HttpPost("createTransactionPin/{userId}")]
        public async Task<IActionResult> TransactionPin([Required] int userId, [FromBody] TransactionPinDTO model)
        {
            var res = await _customerService.CreateTransactionPin(userId, model);
            return Ok(res);
        }

        [HttpPost("createLoginPin/{userId}")]
        public async Task<IActionResult> LoginPin([Required] int userId, [FromBody] LoginPinDTO model)
        {
            var res = await _customerService.CreateLoginPin(userId, model);
            return Ok(res);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var res = await _customerService.Login(model);
            return Ok(res);
        }

        [HttpPost("Unlock/{userId}")]
        public async Task<IActionResult> Unlock(string userId, [Required] string pin)
        {
            var res = await _customerService.Unlock(userId, pin);
            return Ok(res);
        }


    }
}
