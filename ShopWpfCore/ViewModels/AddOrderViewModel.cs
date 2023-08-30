using ShopDAL.Models;
using ShopDAL.Models.Validator;
using ShopWpf.Commands;
using System.Windows;

namespace ShopWpfCore.ViewModels
{
    /// <summary>
    /// Логика добавления заказа
    /// </summary>
    internal class AddOrderViewModel
    {
        private Window _window;

        private OrderValidator orderValidator;

        public Order NewOrder { get; }

        public AddOrderViewModel(Window OwnerWindow, string E_mail)
        {
            _window = OwnerWindow;

            NewOrder = new Order() { CustomerE_mail = E_mail};

            orderValidator = new();
        }

        #region Команды

        private RelayCommand addOrderCommand = null!;
        public RelayCommand AddOrderCommand => addOrderCommand ?? (new RelayCommand(AddOrder, CanAddOrder));


        private RelayCommand cancelCommand = null!;
        public RelayCommand CancelCommand => cancelCommand ?? (new RelayCommand(Cancel));

        #endregion


        private bool CanAddOrder()
        {
            var resultValidator = orderValidator?.Validate(NewOrder);

            return resultValidator.IsValid ? true : false;
        }

        private void AddOrder() => _window.DialogResult = true;

        /// <summary>
        /// Закртытия окна бесподверждения сохранения заказа
        /// </summary>
        private void Cancel() => _window.DialogResult = false;
    }
}
