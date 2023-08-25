using ShopDAL.Models;
using ShopWpf.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ShopWpfCore.ViewModels
{
    /// <summary>
    /// Логика создания нового клиента
    /// </summary>
    internal class NewCustomerViewModel
    {
        private Customer newCustomer;

        private CustomerValidator customerValidator;

        private Window _window;
        public Customer NewCustomer { get => newCustomer; } 

        public NewCustomerViewModel(Window window)
        {
            //данные чтобы не ругался FluentValidation ---> customerValidator?.Validate(newCustomer);
            newCustomer = new Customer() 
                {   FirstName = "Имя", 
                    MiddleName="Отчество", 
                    LastName = "Фамилия", 
                    E_mail = "...@mail.ru", 
                    Telefon = "+79......" 
                };

            customerValidator = new CustomerValidator();

            this._window = window;
        }

        #region Команды

        private RelayCommand addCustomerCommand = null;
        public RelayCommand AddCustomerCommand => addCustomerCommand ?? (new RelayCommand(AddClient, CanAddClient));


        private RelayCommand cancelCommand = null;
        public RelayCommand CancelCommand => cancelCommand ?? (new RelayCommand(Cancel));

        #endregion

        /// <summary>
        /// Выпоняется проверка данных клиента
        /// </summary>
        /// <param name="window">view - нового клиента</param>
        /// <returns>true - если данные валидны
        /// false - если есть ощибки(а)</returns>
        private bool CanAddClient()
        {
            var resultValidator = customerValidator?.Validate(newCustomer);

            return resultValidator.IsValid ? true : false;
        }
        /// <summary>
        /// Заверщение работы окна с подвержение нового клиента
        /// </summary>
        private void AddClient() => _window.DialogResult = true;

        /// <summary>
        /// Закртытие окна бесподверждения сохранения клиента
        /// </summary>
        private void Cancel()
        {
            _window.DialogResult = false;

            newCustomer = null!;
        }
    }

}
