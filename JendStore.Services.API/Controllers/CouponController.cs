using AutoMapper;
using JendStore.Services.API.DTO;
using JendStore.Services.API.IRepository;
using JendStore.Services.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JendStore.Services.API.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CouponController> _logger;
        protected ResponseDTOStatus _response;

        public CouponController(IUnitOfWork unitOfWork, ILogger<CouponController> logger,  IMapper mapper)
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
            var coupon = await _unitOfWork.Coupons.GetAll();

            _response.Result = _mapper.Map<IList<CouponDTO>>(coupon);

            return Ok(_response);
        }

        [HttpGet("{couponId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ResponseDTOStatus> Get(int couponId)
        {
            var coupon = await _unitOfWork.Coupons.Get(c => c.CouponId == couponId);
            if (coupon == null)
            {
                _logger.LogError($"Invalid action in {nameof(Get)}");
                _response.IsSuccess = false;
                _response.Message = "coupon Id is Invalid";
            }

            _response.Result = _mapper.Map<CouponDTO>(coupon);
            return _response;
        }

        [HttpGet("code/{code}")]
        public async Task<ResponseDTOStatus> GetCode(string code)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    _response.Result = ModelState;
                }

                var coupon = await _unitOfWork.Coupons.Get(c => c.Code.ToLower() == code.ToLower());

                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = ("Code coupon Invalid ");
                }

                _response.Result = _mapper.Map<CouponDTO>(coupon);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Post([FromBody] CreateCouponDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Invalid Post Action in {nameof(Post)}");
                    _response.Result = ModelState;
                }
                var coupon = _mapper.Map<Coupon>(createDTO);

                await _unitOfWork.Coupons.Insert(coupon);

                await _unitOfWork.Save();

                _response.Result = coupon;

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return Ok(_response);
        }

        [HttpPut("{couponId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseDTOStatus> Put(int couponId, [FromBody] UpdateCouponDTO updateDTO)
        {
            try
            {
                if (!ModelState.IsValid && couponId < 1)
                {
                    _logger.LogError($"Invalid Update Action in {nameof(Put)}");
                    _response.Result = ModelState;
                }

                var coupon = await _unitOfWork.Coupons.Get(c => c.CouponId == couponId);

                if (coupon == null)
                {
                    _logger.LogError($"Invalid action in {nameof(Put)}");
                }

                var result = _mapper.Map(updateDTO, coupon);
                _unitOfWork.Coupons.Update(coupon);
                await _unitOfWork.Save();

                _response.Result = result;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpDelete("{couponId:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ResponseDTOStatus> Delete(int couponId)
        {
            try
            {
                if (!ModelState.IsValid && couponId < 1)
                {
                    _logger.LogError($"Invalid Update Action in {nameof(Delete)}");
                    _response.Result = ModelState;
                }

                var coupon = await _unitOfWork.Coupons.Get(c => c.CouponId == couponId);
                if (coupon == null)
                {
                    _logger.LogError($"Invalid action in {nameof(Delete)}");
                }

                await _unitOfWork.Coupons.Delete(couponId);
                await _unitOfWork.Save();

                return _response;
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
