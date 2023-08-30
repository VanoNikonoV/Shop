namespace ShopDAL.Models
{
    public partial class Order
    {
        /// <summary>
        /// Идентификатор заказа
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Внешний ключ. Е-mail клиента
        /// </summary>
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
        /// <summary>
        /// Код продукта
        /// </summary>
        public string ProductCode
        {
            get => this.productCode;
            set
            {   if (this.productCode == value) return;

                this.productCode = value;
                OnPropertyChanged(nameof(ProductCode));
            }
        }
        /// <summary>
        /// Наименование продукта
        /// </summary>
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
        /// <summary>
        /// Навигационное свойство
        /// </summary>
        public Customer Customer { get; }

        public override string ToString()
        {
            return $"{Id} {CustomerE_mail} {ProductName} {ProductCode}";
        }
    }
}
