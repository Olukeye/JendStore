using AutoMapper;
using JendStore.Products.Service.API.DTO;
using JendStore.Products.Service.API.Models;
using JendStore.Products.Service.API.Repository.Interface;
using JendStore.PRoducts.Service.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JendStore.Products.Service.API.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;
        protected ResponseDTOStatus _response;

        public ProductController(IUnitOfWork unitOfWork, IMapper mapper, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            var product = await _unitOfWork.Products.GetAll();
            _response.Result = _mapper.Map<IList<ProductDto>>(product);

            return Ok(_response);
        }

        [HttpGet("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int productId)
        {
            var product = await _unitOfWork.Products.Get(c => c.ProductId == productId);
            _response.Result = _mapper.Map<ProductDto>(product);

            return Ok(_response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] CreateProductDto createDto)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError($"Invalid Post Action in {nameof(Post)}");
                _response.Result = ModelState;
            }
            var product = _mapper.Map<Product>(createDto);

            await _unitOfWork.Products.Insert(product);

            await _unitOfWork.Save();

            _response.Result = product;

            return Ok(_response);
        }

        [HttpPut("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int productId, [FromBody] UpdateProduct update)
        {
            if (!ModelState.IsValid && productId < 1)
            {
                _logger.LogError($"Invalid Update Action in {nameof(Put)}");
                return BadRequest(ModelState);
            }

            var product = await _unitOfWork.Products.Get(c => c.ProductId == productId);

            if (product == null)
            {
                _logger.LogError($"Invalid action in {nameof(Put)}");
                return BadRequest("Wrong Data Submitted");
            }

            var result = _mapper.Map(update, product);
            _unitOfWork.Products.Update(product);
            await _unitOfWork.Save();

            _response.Result = result;

            return Ok(_response);

        }

        [HttpDelete("{productId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int productId)
        {
            if (!ModelState.IsValid && productId < 1)
            {
                _logger.LogError($"Invalid Action in {nameof(Delete)}");
                return BadRequest(ModelState);
            }

            var product = await _unitOfWork.Products.Get(c => c.ProductId == productId);

            if (product == null)
            {
                _logger.LogError($"Invalid action in {nameof(Delete)}");
                return BadRequest("Wrong Data Submitted");
            }

            await _unitOfWork.Products.Delete(productId);
            await _unitOfWork.Save();

            return Ok(_response);

        }
    }
}