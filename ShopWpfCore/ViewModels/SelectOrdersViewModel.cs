using ShopDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
