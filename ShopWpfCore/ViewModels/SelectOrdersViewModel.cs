using ShopDAL.Models;
using System.Collections.Generic;

namespace ShopWpfCore.ViewModels
{
    internal class SelectOrdersViewModel
    {
        public List<Order> OrdersForCustomer { get; }

        public SelectOrdersViewModel(List<Order> ordersForCustomer)
        {
            OrdersForCustomer = new List<Order>();

            OrdersForCustomer = ordersForCustomer;
        }


    }
}
