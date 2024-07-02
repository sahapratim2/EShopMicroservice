
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketRequest(string UserName);
    public record GetBasketResponse(ShoppingCart Cart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var result = await sender.Send(new GetBasketQuery(userName));

                var resposnse = result.Adapt<GetBasketResponse>();

                return Results.Ok(resposnse);
            });
        }
    }
}
