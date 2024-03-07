namespace JendStore.Cart.Service.API.DTO
{
    public class CartHeaderDto
    {
        public int CartHeaderId { get; set; }
        public string? UserId { get; set; }
        public string? Code { get; set; }
        public double Discount { get; set; }
        public double Total { get; set; }
    }
}
