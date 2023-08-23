using ShopWpf.View.Base;
using System.Collections.ObjectModel;
using ShopDAL.Models;
using ShopDAL.EFCore;

namespace ShopWpf.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        public ObservableCollection<Customer> ShopRepository { get; set; }


        public MainWindowViewModel()
        {
            ShopRepository = new ObservableCollection<Customer>();

            using (var context = new ShopContext())
            {
                ShopRepository = context.


            }
        }


    }
}
