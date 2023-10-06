
namespace NorthWind.Sales.UseCases.CreateOrder
{
    public class CreateOrderInteractor : ICreateOrderInputPort
    {
        readonly ICreateOrderOutputPort OutputPort;
        readonly INorthWindSalesCommandsRepository Repository;

        public CreateOrderInteractor(ICreateOrderOutputPort outputPort,
            INorthWindSalesCommandsRepository repository)
        {
            OutputPort = outputPort;
            Repository = repository;
        }

        public async ValueTask Handle(CreateOrderDTO orderDto)
        {
            OrderAggregate OrderAggregate = new OrderAggregate
            {
                CustomerId = orderDto.CustomerId,
                ShipAddress = orderDto.ShipAddress,
                ShipCity = orderDto.ShipCity,
                ShipCountry = orderDto.ShipCountry,
                ShipPostalCode = orderDto.ShipPostalCode
            };

            foreach (var Item in orderDto.OrderDetails)
            {
                OrderAggregate.AddDetail(Item.ProductId,
                    Item.UnitPrice, Item.Quantity);
            }

            await Repository.CreateOrder(OrderAggregate);
            await Repository.SaveChanges();
            await OutputPort.Handle(OrderAggregate.Id);
        }
    }

}
