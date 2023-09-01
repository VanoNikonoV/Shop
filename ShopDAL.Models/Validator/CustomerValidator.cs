using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDAL.Models
{
    /// <summary>
    /// Выполняет проверку вводимых данных о клиенте
    /// </summary>
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(customer => customer.FirstName)
                        .NotEmpty().WithMessage("Имя не заполнено")
                        .Must(client => client.All(Char.IsLetter)).WithMessage("Имя должно собержать только буквы")
                        .Must(StartsWithUpper).WithMessage("Имя должно начинаться с заглавной буквы");

            RuleFor(customer => customer.MiddleName)
                        .NotEmpty().WithMessage("Отчество не заполнено")
                        .Must(customer => customer.All(Char.IsLetter)).WithMessage("Отчество  должно собержать только буквы")
                        .Must(StartsWithUpper).WithMessage("Отчество должно начинаться с заглавной буквы");

            RuleFor(customer => customer.LastName)
                        .NotEmpty().WithMessage("Фамилия не заполнена")
                        .Must(customer => customer.All(Char.IsLetter)).WithMessage("Фамилия должно собержать только буквы")
                        .Must(StartsWithUpper).WithMessage("Фамилия должно начинаться с заглавной буквы");

            RuleFor(t => t.Telefon)
                        .NotEmpty().WithMessage("Нужно указать значение")
                        .Must(t => t.StartsWith("+79")).WithMessage("Номер долже начинаться +79...")
                        .Length(12).WithMessage("Длина должна быть {MinLength}. Текущая длина: {TotalLength}")
                        .Must(TelefonIsDigit).WithMessage("Номер долже содержать только цифры");
            
            RuleFor(customer => customer.E_mail).EmailAddress();
        }

        /// <summary>
        /// Показывает, относится ли указанные символы Юникода к категории десятичных цифр, 
        /// если value - не равно hull или string.Empty
        /// </summary>
        /// <param name="value">Номер телефона</param>
        /// <returns>true - если телефон состоит только из цифр,
        /// в противном случае - false</returns>
        private bool TelefonIsDigit(string value)
        {
            if (string.IsNullOrEmpty(value)) return true;

            bool isDigit = value.Skip(1).All(Char.IsDigit);

            return isDigit;
        }

        /// <summary>
        /// Определяет является ли первым символом 
        /// текущего экземпляра строки символ верхнего регистра
        /// </summary>
        /// <param name="str">Проеряемая строка</param>
        /// <returns>true - если превая буква заглавная
        /// false - если превая буква незаглавная </returns>
        public bool StartsWithUpper( string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            char ch = str[0];
            return char.IsUpper(ch);
        }

    }

    
}
