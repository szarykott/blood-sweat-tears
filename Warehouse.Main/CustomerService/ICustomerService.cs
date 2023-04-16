namespace Warehouse.Main.CustomerService;

public interface ICustomerService
{
    OrderReply HandleOrder(Order order);
}