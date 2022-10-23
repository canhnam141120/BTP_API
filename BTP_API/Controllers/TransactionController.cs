using BTP_API.Context;
using BTP_API.Helpers;
using BTP_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Asn1.Cmp;
using System.IdentityModel.Tokens.Jwt;
using static BTP_API.Helpers.EnumVariable;

namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        [HttpPut("exchange/{id}/cancel")]
        public async Task<IActionResult> cancelExchange(int id)
        {
            try
            {
                var apiMessage = await _transactionRepository.cancelExchangeAsync(id);
                if(apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("exchange-detail/{id}/cancel")]
        public async Task<IActionResult> cancelExchangeDetail(int id)
        {
            try
            {
                var apiMessage = await _transactionRepository.cancelExchangeDetailAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if (apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("rent/{id}/cancel")]
        public async Task<IActionResult> cancelRent(int id)
        {
            try
            {
                var apiMessage = await _transactionRepository.cancelRentAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }

        [HttpPut("rent-detail/{id}/cancel")]
        public async Task<IActionResult> cancelRentDetail(int id)
        {
            try
            {
                var apiMessage = await _transactionRepository.cancelRentDetailAsync(id);
                if (apiMessage.Message == Message.SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                if (apiMessage.Message == Message.NOT_YET_LOGIN.ToString())
                {
                    return BadRequest(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.FAILED.ToString() });
            }
        }
    }
}
