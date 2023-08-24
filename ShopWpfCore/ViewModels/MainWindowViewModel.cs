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
using ShopWpfCore.View;
using System.Windows;
using ShopWpfCore.ViewModels;
using System.Diagnostics;

namespace ShopWpf.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private ShopContext _shopContext;
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

        private RelayCommandT<Customer> selectOrdersCommand = null!;
        public RelayCommandT<Customer> SelectOrdersCommand =>
            selectOrdersCommand ?? (selectOrdersCommand = new RelayCommandT<Customer>(SelectOrders, CanSelectOrders));



        #endregion

        #region Методы для команд

        private bool CanSelectOrders(Customer customer)
        {
            return customer == null ? false : true;
        }

        private void SelectOrders(Customer customer)
        {
            SelectOrdersWindow selectOrders = new SelectOrdersWindow() { Owner = Application.Current.MainWindow };

            string e_mailCustomer = customer.E_mail;

            using (_shopContext = new ShopContext())
            {
                var ordersForCustomer = _shopContext.Orders
                    .Where(x => x.CustomerE_mail == e_mailCustomer)
                    .ToList();

                SelectOrdersViewModel selectOrdersViewModel = new SelectOrdersViewModel(ordersForCustomer);

                selectOrders.DataContext = selectOrdersViewModel;

                selectOrders.ShowDialog();
            }
        }

        #endregion
    }
}
