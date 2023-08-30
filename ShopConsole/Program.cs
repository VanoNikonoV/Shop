
using ShopConsole;
using ShopDAL.EFCore;
using ShopDAL.Models;

Console.WriteLine("------ Shop ------");

using(var context = new ShopContext()) 
{
    context.Database.EnsureDeleted();
    context.Database.EnsureCreated();

    List<Customer> customers = GetDataForTabels.GetCustomers(50);

    context.Customers.AddRange(customers);

    context.SaveChanges();

    List<Order> orders = GetDataForTabels.GetOrders(50);

    context.Orders.AddRange(orders);

    context.SaveChanges();

    foreach (Customer customer in context.Customers) 
    {
        Console.WriteLine(customer);
    }

    Console.WriteLine();

    foreach (Order order in context.Orders)
    {
        Console.WriteLine(order);
    }
    Console.ReadLine();
}