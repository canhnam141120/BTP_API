namespace BookTradingPlatform.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManageCategoryController : ControllerBase
    {
        private readonly IManageCategoryRepository _manageCategoryRepository;

        public ManageCategoryController(IManageCategoryRepository manageCategoryRepository)
        {
            _manageCategoryRepository = manageCategoryRepository;
        }

        [HttpGet("all")]
        public async Task<IActionResult> getAllCategory([FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageCategoryRepository.getAllCategoryAsync(page);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> getCategoryById([FromRoute] int id)
        {
            try
            {
                var apiResponse = await _manageCategoryRepository.getCategoryByIdAsync(id);
                if (apiResponse.NumberOfRecords != 0)
                {
                    return Ok(apiResponse);
                }
                return NotFound(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> createCategory([FromForm] CategoryVM categoryVM)
        {
            try
            {
                var apiResponse = await _manageCategoryRepository.createCategoryAsync(categoryVM);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
            }
        }

        [HttpPut("edit/{id}")]
        public async Task<IActionResult> updateCategory([FromRoute] int id, [FromForm] CategoryVM categoryVM)
        {
            try
            {
                var apiMessage = await _manageCategoryRepository.updateCategoryAsync(id, categoryVM);
                if (apiMessage.Message == Message.UPDATE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> deleteCategory([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageCategoryRepository.deleteCategoryAsync(id);
                if (apiMessage.Message == Message.DELETE_SUCCESS.ToString())
                {
                    return Ok(apiMessage);
                }
                return NotFound(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }
    }
}

