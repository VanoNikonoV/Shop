using ShopWpf.View.Base;
using System.Collections.ObjectModel;
using ShopDAL.Models;
using ShopDAL.EFCore;
using System.Linq;
using System.Collections.Generic;

namespace ShopWpf.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public List<Customer> ShopRepository { get; set; }


        public MainWindowViewModel()
        {
            ShopRepository = new List<Customer>();

            using (var context = new ShopContext())
            {
               ShopRepository = context.Customers.ToList();

            }
        }


    }
}
