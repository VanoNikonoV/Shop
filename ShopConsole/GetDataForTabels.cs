using ShopDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopConsole
{
    static class GetDataForTabels
    {
        static readonly string[] firstNames;

        static readonly string[] middleNames;

        static readonly string[] secondNames;

        static readonly string[] productNames;

        static List<Customer> customers;

        /// <summary>
        /// Генератор псевдослучайных чисел
        /// </summary>
        static Random randomize;

        /// <summary>
        /// Статический конструктор, в котором "хранятся"
        /// данные о именах и фамилиях баз данных firstNames и lastNames
        /// </summary>
        static GetDataForTabels()
        {
            randomize = new Random();

            firstNames = new string[] {
                "Агата",
                "Агнес",
                "Мария",
                "Аделина",
                "Ольга",
                "Людмила",
                "Аманда",
                "Татьяна",
                "Вероника",
                "Жанна",
                "Крестина",
                "Анжела",
                "Маргарита"
            };

            middleNames = new string[]
            {
                "Ивановна",
                "Петровна",
                "Васильевна",
                "Сергеевна",
                "Дмитриевна",
                "Владимировна",
                "Александровна",
                "Тимофеевна"

            };

            secondNames = new string[]
            {
                "Иванова",
                "Петрова",
                "Васильева",
                "Кузнецова",
                "Ковалёва",
                "Попова",
                "Пономарёва",
                "Дьячкова",
                "Коновалова",
                "Соколова",
                "Лебедева",
                "Соловьёва",
                "Козлова",
                "Волкова",
                "Зайцева",
                "Ершова",
                "Карпова",
                "Щукина",
                "Виноградова",
                "Цветкова",
                "Калинина"
            };

            productNames = new string[] 
            {
                "телефон Xiaomi",
                "телефон LG",
                "телефон Samsung",
                "телевизор Xiaomi",
                "пылесос Xiaomi",
                "пылесос Samsung",
                "утюг Philips",
                "чайник Philips",
                "чайник Vitek",
                "кофемолка Philips",
                "кофемолка Vitek",
                "ноутбук Acer",
                "смартфон Appel",
                "смартфон Nokia",
                "смартфон Motorola",
                "пылесос Appel",
                "телевизор Sony",
                "телевизор Samsung",
                "телевизор LG",
                "видеомагнитафон Sharp"
            };

            customers = new List<Customer>();
        }


        public static List<Customer> GetCustomers(int count)
        {

            long telefon = 79020000000;
            string email = "@mail.ru";

            for (int i = 0; i < count; i++)
            {
                telefon += i;

                Customer _c = new Customer()
                {
                    FirstName = firstNames[GetDataForTabels.randomize.Next(GetDataForTabels.firstNames.Length)],
                    MiddleName = middleNames[GetDataForTabels.randomize.Next(GetDataForTabels.middleNames.Length)],
                    LastName = secondNames[GetDataForTabels.randomize.Next(GetDataForTabels.secondNames.Length)],
                    Telefon = "+" + telefon.ToString(),
                    E_mail = i.ToString() + email,
                };

                customers.Add(_c);
            }

            return customers;
        }

        public static List<Order> GetOrders(int count) 
        {
            List<Order> orders = new List<Order>();

            count  = count + count;

            for (int i = 0; i < count; i++)
            {
                Order o = new Order()
                {
                    CustomerE_mail = customers[randomize.Next(0, 49)].E_mail,
                    ProductCode = randomize.Next(0, 5000).ToString(),
                    ProductName = productNames[GetDataForTabels.randomize.Next(GetDataForTabels.productNames.Length)]
                };

                orders.Add(o);
            }

            return orders;
        }
    }
}
