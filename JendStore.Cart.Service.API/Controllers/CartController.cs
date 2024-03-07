using AutoMapper;
using JendStore.Cart.Service.API.DTO;
using JendStore.Cart.Service.API.Models;
using JendStore.Cart.Service.API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;

namespace JendStore.Cart.Service.API.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<CartController> _logger;
        protected ResponseDTOStatus _response;

        public CartController(IUnitOfWork unitOfWork, IMapper mapper1, ILogger<CartController> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper1;
            _logger = logger;
            _response = new();
        }

        [HttpPost("CartUpsert")]
        public async Task<IActionResult> CartUspsert(CartDto cartDto)
        {
            var cartHeder = await _unitOfWork.CartHeaders.Get(u => u.UserId == cartDto.CartHeader.UserId);
            if (cartHeder == null)
            {
                //Create header and detail
                CartHeader cartHeader = _mapper.Map<CartHeader>(cartDto.CartHeader);
                await _unitOfWork.CartHeaders.Insert(cartHeader);
                await _unitOfWork.Save();
                cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                await _unitOfWork.CartDetails.Insert(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
                await _unitOfWork.Save();
            }
            else
            {
                //if header is not null
                //check if detail has same product
                var cartDetail = await _unitOfWork.CartDetails.Get(u => u.ProductId == cartDto.CartDetails.First().ProductId && u.CartHeaderId == cartHeder.CartHeaderId);
                if (cartDetail == null)
                {
                    //create cartDeatil
                    cartDto.CartDetails.First().CartHeaderId = cartHeder.CartHeaderId;
                    await _unitOfWork.CartDetails.Insert(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
                    await _unitOfWork.Save();
                }
                else
                {
                    //update quantity in cart detail
                    cartDto.CartDetails.First().Quantity += cartDetail.Quantity;
                    cartDto.CartDetails.First().CartHeaderId = cartDetail.CartHeaderId;
                    cartDto.CartDetails.First().CartDetailId = cartDetail.CartDetailId;
                    _unitOfWork.CartDetails.Update(_mapper.Map<CartDetail>(cartDto.CartDetails.First()));
                    await _unitOfWork.Save(); 
                }
            }
            _response.Result = cartDto;
            return Ok(_response);
        }
    }
}