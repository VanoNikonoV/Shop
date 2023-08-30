using ShopWpf.View.Base;
using System.Collections.ObjectModel;
using ShopDAL.Models;
using ShopDAL.EFCore;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShopWpf.Commands;
using ShopWpfCore.View;
using System.Windows;
using ShopWpfCore.ViewModels;

namespace ShopWpf.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        #region Поля и свойства
        public ShopContext? ShopContext { get; private set; }


        private ObservableCollection<Customer>? customersView;
        /// <summary>
        /// Коллекция клиентов
        /// </summary>
        public ObservableCollection<Customer> CustomersView
        {
            get
            {
                if (customersView == null && ShopContext != null)
                {
                    ShopContext.Customers.Load<Customer>();
                    customersView = ShopContext?.Customers.Local.ToObservableCollection();
                }
                return customersView!;
            }
        }


        private ObservableCollection<Order>? ordersView;
        /// <summary>
        /// Коллеция заказов
        /// </summary>
        public ObservableCollection<Order> OrdersView
        {
            get
            {
                if (ordersView == null && ShopContext != null)
                {
                    ShopContext.Orders.Load<Order>();
                    ordersView = ShopContext?.Orders.Local.ToObservableCollection();
                }
                return ordersView;
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            ShopContext = new();
            ShopContext.Database.EnsureCreated();
        }

        #region Команды

        private RelayCommandT<Customer> selectOrdersCommand = null!;
        public RelayCommandT<Customer> SelectOrdersCommand =>
            selectOrdersCommand ?? (selectOrdersCommand = new RelayCommandT<Customer>(SelectOrders, CanSelectOrders));


        private RelayCommand addNewCustomerCommand = null!;
        public RelayCommand AddNewCustomerCommand =>
            addNewCustomerCommand ?? (addNewCustomerCommand = new RelayCommand(AddNewCustomerAsync));


        private RelayCommandT<Customer> deleteCustomerCommand = null!;
        public RelayCommandT<Customer> DeletCustomerCommand =>
            deleteCustomerCommand ?? (deleteCustomerCommand = new RelayCommandT<Customer>(DeletCustomer, CanDeletCustomer));


        private RelayCommandT<Customer> addOrderCommand = null!;
        public RelayCommandT<Customer> AddOrderCommand => 
            addOrderCommand ?? (addOrderCommand = new RelayCommandT<Customer>(AddOrderAsync));


        private RelayCommand clearTableCommand = null!;
        public RelayCommand ClearTableCommand => 
            clearTableCommand ?? (clearTableCommand = new RelayCommand(ClearTableAsync));


        private RelayCommandT<Customer> editCustomerCommand = null!;
        public RelayCommandT<Customer> EditCustomerCommand => 
            editCustomerCommand ?? (editCustomerCommand = new RelayCommandT<Customer>(EditCustomerAsync, CanEditCustomer));

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

            var ordersForCustomer = ShopContext.Orders
                .Where(x => x.CustomerE_mail == e_mailCustomer)
                .ToList();

            SelectOrdersViewModel selectOrdersViewModel = new SelectOrdersViewModel(ordersForCustomer);

            selectOrders.DataContext = selectOrdersViewModel;

            selectOrders.ShowDialog();
            
        }

        /// <summary>
        /// Добавляет нового клиента в базу данных
        /// </summary>
        private async void AddNewCustomerAsync()
        {
            NewCustomerWindow newCustomerWindow = new NewCustomerWindow() 
                {  Owner = Application.Current.MainWindow };

            List<Customer> AllCustomer = ShopContext?.Customers.Local.ToList();

            NewCustomerViewModel newCustomerViewModel = new NewCustomerViewModel(newCustomerWindow, AllCustomer);

            newCustomerWindow.DataContext = newCustomerViewModel;

            newCustomerWindow.ShowDialog();

            if (newCustomerWindow.DialogResult == true)
            {
                await ShopContext.Customers.AddAsync(newCustomerViewModel.NewCustomer);

                await ShopContext.SaveChangesAsync();
            }
        }

        private bool CanDeletCustomer(Customer customer) => customer != null ? true : false;

        /// <summary>
        /// Удаляет выделенного клиента и его заказы
        /// </summary>
        /// <param name="customer"></param>
        private void DeletCustomer(Customer customer)
        {
            ShopContext.Customers.Remove(customer);

            ShopContext.SaveChangesAsync();
        }

        /// <summary>
        /// Добавление нового заказа для выбранного клиента
        /// </summary>
        /// <param name="currentCustomer"></param>
        private async void AddOrderAsync(Customer currentCustomer)
        {
            AddOrderWindow addOrderWindow = new AddOrderWindow() 
                {  Owner = Application.Current.MainWindow };

            AddOrderViewModel addOrderViewModel = new AddOrderViewModel(addOrderWindow, currentCustomer.E_mail);

            addOrderWindow.DataContext = addOrderViewModel;

            addOrderWindow.ShowDialog();

            if (addOrderWindow.DialogResult == true)
            {
                await ShopContext.Orders.AddAsync(addOrderViewModel.NewOrder);

                await ShopContext.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Удаление всех записей в таблицах
        /// </summary>
        private async void ClearTableAsync()
        {
            var allRecord = ShopContext.Customers;

            ShopContext.Customers.RemoveRange(allRecord);

            await ShopContext.SaveChangesAsync();
        }

        /// <summary>
        /// Включение/выключение кнопки в зависимость от условий:
        /// - отсутсвие ощибок в данных customer.Error == string.Empty,
        /// - наличие изменений в данных EntityState.Modified
        /// </summary>
        /// <param name="customer">Выбранный клиент</param>
        /// <returns>true - если customer не null и уловия выполены, иначе false</returns>
        private bool CanEditCustomer(Customer customer)
        {
            if (customer != null && customer.Error == string.Empty) 
            {
                return ShopContext.Entry(customer).State == EntityState.Modified ? true : false;
            }
            return false;
        }
           
        /// <summary>
        /// Метод изменения данных клиента
        /// </summary>
        /// <param name="currentCustomer">Выбранные клиент</param>
        private async void EditCustomerAsync(Customer currentCustomer)
        {
            ShopContext.Customers.Update(currentCustomer);
            
            await ShopContext.SaveChangesAsync();
        }

        #endregion
    }
}
