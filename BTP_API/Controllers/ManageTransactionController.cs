using BTP_API.Helpers;
using BTP_API.Models;
using Org.BouncyCastle.Asn1.Cmp;
using static BTP_API.Helpers.EnumVariable;

namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageTransactionController : ControllerBase
    {
        private readonly IManageTransactionRepository _manageTransactionRepository;

        public ManageTransactionController(IManageTransactionRepository manageTransactionRepository)
        {
            _manageTransactionRepository = manageTransactionRepository;
        }

        [HttpGet("exchange/all")]
        public async Task<IActionResult> getAllExchange()
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeAsync();
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("exchange/{id}/detail")]
        public async Task<IActionResult> getAllExchangeDetail(int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeDetailAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("exchange/{id}/bill")]
        public async Task<IActionResult> getAllExchangeBill(int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllExchangeBillAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("exchange/{id}/update-status")]
        public async Task<IActionResult> updateStatusExchange(int id, string status)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateStatusExchangeAsync(id, status);
                if (apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("exchange-detail/{id}/update-status")]
        public async Task<IActionResult> updateExchangeDetail(int id, ExchangeDetailVM exchangeDetailVM)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateExchangeDetailAsync(id, exchangeDetailVM);
                if (apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpGet("rent/all")]
        public async Task<IActionResult> getAllRent()
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentAsync();
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("rent/{id}")]
        public async Task<IActionResult> getAllRentDetail(int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentDetailAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("rent/{id}/bill")]
        public async Task<IActionResult> getAllRentBill(int id)
        {
            try
            {
                var apiResponse = await _manageTransactionRepository.getAllRentBillAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPut("rent/{id}/update-status")]
        public async Task<IActionResult> updateStatusRent(int id, string status)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateStatusRentAsync(id, status);
                if (apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("rent-detail/{id}/update-status")]
        public async Task<IActionResult> updateRentDetail  (int id, RentDetailVM rentDetailVM)
        {
            try
            {
                var apiMessage = await _manageTransactionRepository.updateRentDetailAsync(id, rentDetailVM);
                if (apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage{Message = Message.UPDATE_FAILED.ToString() });
            }
        }

    }
}
