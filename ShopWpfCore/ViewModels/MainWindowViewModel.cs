using ShopWpf.View.Base;
using System.Collections.ObjectModel;
using ShopDAL.Models;
using ShopDAL.EFCore;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using ShopWpf.Commands;
using System;

namespace ShopWpf.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly ShopContext _shopContext;
        public ObservableCollection<Customer> CustomersView { get; set; }
        public ObservableCollection<Order> OrdersView { get; set; }

        public MainWindowViewModel()
        {
            CustomersView = new ObservableCollection<Customer>();
            OrdersView = new ObservableCollection<Order>();

            using (_shopContext = new ShopContext())
            {
                _shopContext.Customers.Load<Customer>();
                _shopContext.Orders.Load<Order>();
                
               CustomersView = _shopContext?.Customers.Local.ToObservableCollection();

               OrdersView = _shopContext?.Orders.Local.ToObservableCollection();
            }
        }

        #region Команды

        private RelayCommand selectOrdersCommand = null!;
        public RelayCommand SelectOrdersCommand =>
            selectOrdersCommand ?? (selectOrdersCommand = new RelayCommand(SelectOrders, CanSelectOrders));

        private bool CanSelectOrders()
        {
            throw new NotImplementedException();
        }

        private void SelectOrders()
        {
            throw new NotImplementedException();
        }


        #endregion
    }
}
