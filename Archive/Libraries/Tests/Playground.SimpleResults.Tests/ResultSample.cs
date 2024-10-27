namespace Playground.SimpleResults.Tests
{
    public class ResultSample
    {
        
        private List<Order> _orders = new List<Order>();
        
        public Result CreateOrder(int userId, List<int> productIds)
        {
            if (userId < 1)
            {
                return Result.Fail(Messages.User_DoesNot_Exist);
            }

            if (productIds == null || productIds.Count == 0)
            {
                return Result.Fail(Messages.Products_AreEmpty);
            }

            var order = new Order(Guid.NewGuid(), userId, productIds);
            _orders.Add(order);
            return Result.Ok(order.OrderId);
        }


        public Result<Order> GetOrder(Guid orderId)
        {
            if (orderId == Guid.Empty)
            {
                return HttpResult.BadRequest<Order>(Messages.OrderId_NotValid);
            }

            var order = _orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order == null)
            {
                return HttpResult.NotFound<Order>(Messages.Order_DoesNotExist);
            }

            return HttpResult.Okay(order);
        }

        public record Order(Guid OrderId, int UserId, List<int> ProductIds);
    }
}