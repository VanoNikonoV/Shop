using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDAL.Models
{
    public partial class Order
    {
        public int Id { get; set; }

        public string CustomerE_mail 
        {
            get => this.customerE_mail;
            set
            {
                if (this.customerE_mail == value) return;

                this.customerE_mail = value;
                OnPropertyChanged(nameof(CustomerE_mail));
            }
        }

        public string ProductCode
        {
            get => this.productCode;
            set
            {   if (this.productCode == value) return;

                this.productCode = value;
                OnPropertyChanged(nameof(ProductCode));
            }
        }

        public string ProductName 
        {
            get => this.productName;

            set
            {
                if (this.productName == value) return;
                
                this.productName = value;
                OnPropertyChanged($"{nameof(ProductName)}");
            }
        }

        public Customer Customer { get; }

        public override string ToString()
        {
            return $"{Id} {CustomerE_mail} {ProductName} {ProductCode}";
        }
    }
}
