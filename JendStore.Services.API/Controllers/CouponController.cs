using AutoMapper;
using JendStore.Services.API.Data;
using JendStore.Services.API.DTO;
using JendStore.Services.API.IRepository;
using JendStore.Services.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace JendStore.Services.API.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    public class CouponController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CouponController> _logger;
        private ResponseDTOStatus _response;

        public CouponController(IUnitOfWork unitOfWork, ILogger<CouponController> logger,  IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
            _response = new ResponseDTOStatus();

        }

        [HttpGet]
        public async Task<ResponseDTOStatus> Get()
        {
            var coupon = await _unitOfWork.Coupons.GetAll();

            _response.StatusResult = _mapper.Map<IList<CouponDTO>>(coupon);

            return _response;
        }

        [HttpGet("{id:int}")]
        public async Task<ResponseDTOStatus> Get(int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                   _response.StatusResult = ModelState;
                }

                var coupon = await _unitOfWork.Coupons.Get(c => c.CouponId == id);

                _response.StatusResult = _mapper.Map<CouponDTO>(coupon);

            }
            catch(Exception ex)
            {
                _response.Status = false;
                _response.Message = ex.Message;
            }

            return _response;
        }


        [HttpGet("code/{code}")]
        public async Task<ResponseDTOStatus> GetCode(string code)
        {
            try
            {

                if (!ModelState.IsValid)
                {
                    _response.StatusResult = ModelState;
                }

                var coupon = await _unitOfWork.Coupons.Get(c => c.Code.ToLower() == code.ToLower());

                if(coupon == null)
                {
                    _response.Message = ("Code coupon Invalid ");
                }

                _response.StatusResult = _mapper.Map<CouponDTO>(coupon);

            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public async Task<ResponseDTOStatus> Post([FromBody] CreateCouponDTO createDTO)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    _logger.LogError($"Invalid Post Action in {nameof(Post)}");
                    return _response;
                }
                var coupon = _mapper.Map<Coupon>(createDTO);

                await _unitOfWork.Coupons.Insert(coupon);

                await _unitOfWork.Save();

                _response.StatusResult = coupon;
                
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = ex.Message;
            }
            return _response;
        }


        [HttpPut("{id:int}")]
        public async Task<ResponseDTOStatus> Put(int id, [FromBody] UpdateCouponDTO updateDTO)
        {
            try
            {
                if (!ModelState.IsValid && id < 1)
                {
                    _logger.LogError($"Invalid Update Action in {nameof(Put)}");
                    _response.StatusResult = ModelState;
                }

                var coupon = await _unitOfWork.Coupons.Get(c => c.CouponId == id);

                if (coupon == null)
                {
                    _logger.LogError($"Invalid action in {nameof(Put)}");
                }

                var result = _mapper.Map(updateDTO, coupon);
                _unitOfWork.Coupons.Update(coupon);
                await _unitOfWork.Save();

                _response.StatusResult = result;
            }
            catch (Exception ex)
            {
                _response.Status = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
