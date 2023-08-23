using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDAL.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerE_mail { get; set; }

        public string ProductCode { get; set; }

        public string ProductName { get; set; }

        public Customer Customer { get; set; }

        public override string ToString()
        {
            return $"{Id} {CustomerE_mail} {ProductName} {ProductCode}";
        }
    }
}
