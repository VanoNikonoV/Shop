using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDAL.Models
{
    public class Customer
    {
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName { get; set; } 
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        public string Telefon { get; set; }

        public string E_mail { get; set; } 

        public ICollection<Order> Orders { get; private set; } = new ObservableCollection<Order>();

        public override string ToString()
        {
            return $"{Id} {FirstName}  {LastName}  {MiddleName} {Telefon} {E_mail}";
        }

    }
}
