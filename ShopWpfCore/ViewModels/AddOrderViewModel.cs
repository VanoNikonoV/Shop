using ShopDAL.Models;
using ShopWpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;

namespace ShopWpfCore.ViewModels
{
    internal class AddOrderViewModel
    {
        private Window _window;

        //private CustomerValidator customerValidator;

        public Order NewOrder { get; }

        public AddOrderViewModel(Window OwnerWindow, string E_mail)
        {
            _window = OwnerWindow;

            NewOrder = new Order() { CustomerE_mail = E_mail};
        }

        #region Команды

        private RelayCommand addOrderCommand = null!;
        public RelayCommand AddOrderCommand => addOrderCommand ?? (new RelayCommand(AddOrder, CanAddOrder));


        private RelayCommand cancelCommand = null!;
        public RelayCommand CancelCommand => cancelCommand ?? (new RelayCommand(Cancel));

        #endregion


        private bool CanAddOrder()
        {
            //var resultValidator = customerValidator?.Validate(newCustomer);

            //return resultValidator.IsValid ? true : false;
            return true;
        }

        private void AddOrder() => _window.DialogResult = true;

        /// <summary>
        /// Закртытия окна бесподверждения сохранения заказа
        /// </summary>
        private void Cancel() => _window.DialogResult = false;
    }
}
