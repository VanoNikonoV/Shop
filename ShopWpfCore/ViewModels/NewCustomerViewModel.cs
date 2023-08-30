using ShopDAL.Models;
using ShopWpf.Commands;
using System.Collections.Generic;
using System.Windows;



namespace ShopWpfCore.ViewModels
{
    /// <summary>
    /// Логика создания нового клиента
    /// </summary>
    internal class NewCustomerViewModel
    {
        /// <summary>
        /// Новый клиент
        /// </summary>
        private Customer newCustomer;

        private CustomerValidator customerValidator;

        /// <summary>
        /// Список всех клиентов для проверки поля e-mail на уникальность
        /// </summary>
        private List<Customer> allCustomer;

        private Window _window;
        public Customer NewCustomer { get => newCustomer; } 

        public NewCustomerViewModel(Window window, List<Customer> AllCustomer)
        {
            //данные чтобы не ругался FluentValidation ---> customerValidator?.Validate(newCustomer);
            newCustomer = new Customer() 
                {   FirstName = "Имя", 
                    MiddleName="Отчество", 
                    LastName = "Фамилия", 
                    E_mail = "@mail.ru", 
                    Telefon = "+79" 
                };

            customerValidator = new CustomerValidator();

            allCustomer = AllCustomer;

            this._window = window;
        }

        #region Команды

        private RelayCommand addCustomerCommand = null!;
        public RelayCommand AddCustomerCommand => addCustomerCommand ?? (new RelayCommand(AddClient, CanAddClient));


        private RelayCommand cancelCommand = null!;
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

            if(resultValidator.IsValid) 
            {
                if(allCustomer.Exists(f => f.E_mail == newCustomer.E_mail))
                {
                    MessageBox.Show(messageBoxText: "Клинет с таки e-mail уже существует",
                                                caption: "Ощибка в данных",
                                                MessageBoxButton.OK,
                                                icon: MessageBoxImage.Error);

                    newCustomer.E_mail = "";

                    return false;
                }
                return true; 
            }
            return false;
        }
        /// <summary>
        /// Заверщение работы окна с подвержение нового клиента
        /// </summary>
        private void AddClient() => _window.DialogResult = true;

        /// <summary>
        /// Закртытие окна бесподверждения сохранения клиента
        /// </summary>
        private void Cancel() =>  _window.DialogResult = false;
        
    }

}
