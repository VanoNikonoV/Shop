using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDAL.Models
{
    /// <summary>
    /// Класс описывающий модель клиента
    /// </summary>
    public partial class Customer
    {
        public int Id { get; set; }
        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName
        {
            get { return this.firstName; }

            set
            {
                if (this.firstName == value) return;

                this.firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        /// <summary>
        /// Отчество
        /// </summary>
        public string MiddleName
        {
            get { return this.middleName; }

            set
            {
                if (this.middleName == value) return;

                this.middleName = value;
                OnPropertyChanged(nameof(MiddleName));
            }
        }
        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName
        {
            get { return this.lastName; }

            set
            {
                if (this.lastName == value) return;

                this.lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Telefon
        {
            get { return this.telefon; }

            set
            {
                if (this.telefon == value) return;

                this.telefon = value;
                OnPropertyChanged(nameof(Telefon));
            }
        }
        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string E_mail
        {
            get { return this.e_mail; }

            set
            {
                if (this.e_mail == value) return;

                this.e_mail = value;
                OnPropertyChanged(nameof(E_mail));
            }
        }

        public ICollection<Order> Orders { get; private set; } = new ObservableCollection<Order>();

        public override string ToString()
        {
            return $"{Id} {FirstName}  {LastName}  {MiddleName} {Telefon} {E_mail}";
        }

    }
}
