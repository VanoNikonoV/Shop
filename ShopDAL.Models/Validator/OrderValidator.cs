using FluentValidation;
using System;
using System.Linq;

namespace ShopDAL.Models.Validator
{
    public class OrderValidator : AbstractValidator<Order>
    {
        public OrderValidator()
        {
            RuleFor(order => order.ProductName)
                        .NotEmpty().WithMessage("Наменование не заполнено")
                        .Must(StartsWithUpper).WithMessage("Наменование должно начинаться с заглавной буквы");

            RuleFor(order => order.ProductName)
                        .NotEmpty().WithMessage("Код не заполнен");             
        }



        /// <summary>
        /// Определяет является ли первым символом 
        /// текущего экземпляра строки символ верхнего регистра
        /// </summary>
        /// <param name="str">Проеряемая строка</param>
        /// <returns>true - если превая буква заглавная
        /// false - если превая буква незаглавная </returns>
        public bool StartsWithUpper(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return false;

            char ch = str[0];

            return char.IsUpper(ch);
        }
    }
}
