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
                    return Ok(apiResponse);
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
                    return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.GET_FAILED.ToString() });
            }
        }

        [HttpPost("search")]
        public async Task<IActionResult> searchCategory([FromForm] string search, [FromQuery] int page = 1)
        {
            try
            {
                var apiResponse = await _manageCategoryRepository.searchCategoryAsync(search, page);
                return Ok(apiResponse);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.SEARCH_FAILED.ToString() });
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> createCategory([FromForm] CategoryVM categoryVM)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiMessage { Message = Message.CREATE_FAILED.ToString() });
                }
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
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.UPDATE_FAILED.ToString() });
            }
        }

        [HttpPut("delete/{id}")]
        public async Task<IActionResult> deleteCategory([FromRoute] int id)
        {
            try
            {
                var apiMessage = await _manageCategoryRepository.deleteCategoryAsync(id);
                    return Ok(apiMessage);
            }
            catch
            {
                return BadRequest(new ApiMessage { Message = Message.DELETE_FAILED.ToString() });
            }
        }
    }
}

