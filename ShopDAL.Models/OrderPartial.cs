using ShopDAL.Models.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDAL.Models
{
    public partial class Order : INotifyPropertyChanged, IDataErrorInfo
    {

        private string customerE_mail;

        private string productCode;

        private string productName;

        private OrderValidator validator;

        #region DataErrorInfo
        public string this[string columnName]
        {
            get
            {
                if (validator == null)
                {
                    validator = new OrderValidator();
                }
                var firstOrDefault = validator.Validate(this)
                    .Errors.FirstOrDefault(lol => lol.PropertyName == columnName);
                return firstOrDefault?.ErrorMessage;
            }
        }

        public string Error
        {
            get
            {
                var results = validator.Validate(this);

                if (results != null && results.Errors.Any())
                {
                    var errors = string.Join(Environment.NewLine, results.Errors.Select(x => x.ErrorMessage).ToArray());

                    return errors;
                }

                return string.Empty;
            }
        }
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

    }
}
