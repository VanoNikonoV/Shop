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

        private RelayCommand addNewCustomerCommand = null!;

        public RelayCommand AddNewCustomerCommand =>
            addNewCustomerCommand ?? (addNewCustomerCommand = new RelayCommand(AddNewCustomer));

        private RelayCommandT<Customer> deleteCustomerCommand = null!;
        public RelayCommandT<Customer> DeletCustomerCommand =>
            deleteCustomerCommand ?? (deleteCustomerCommand = new RelayCommandT<Customer>(DeletCustomer, CanDeletCustomer));

        private bool CanDeletCustomer(Customer customer) => customer != null ? true: false;
       

        private void DeletCustomer(Customer customer)
        {
            using (_shopContext = new ShopContext())
            {
                _shopContext.Customers.Remove(customer);

                _shopContext.SaveChangesAsync();
            }
        }

        #endregion

        #region Методы для команд

        /// <summary>
        /// Определяе возможность выполнения команды SelectOrders
        /// </summary>
        /// <param name="customer">Выбранный клиент</param>
        /// <returns> true - если customer не null, иначе false</returns>
        private bool CanSelectOrders(Customer customer) => customer == null ? false : true;

        /// <summary>
        /// Создает окно выборки и передает в него данные с формированные по customer.E_mail
        /// </summary>
        /// <param name="customer">Выбранные клиент</param>
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

        /// <summary>
        /// Добавляет нового клиента в базу данных
        /// </summary>
        private void AddNewCustomer()
        {
            NewCustomerWindow newCustomerWindow = new NewCustomerWindow() 
                {  Owner = Application.Current.MainWindow };

            NewCustomerViewModel newCustomerViewModel = new NewCustomerViewModel(newCustomerWindow);

            newCustomerWindow.DataContext = newCustomerViewModel;

            newCustomerWindow.ShowDialog();

            if (newCustomerWindow.DialogResult == true)
            {
                using (_shopContext = new ShopContext())
                {
                    _shopContext.Customers.Add(newCustomerViewModel.NewCustomer);
                    _shopContext.SaveChangesAsync();
                }
            }
        }

        #endregion
    }
}
