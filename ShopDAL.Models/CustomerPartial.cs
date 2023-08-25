using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopDAL.Models
{
    public partial class Customer : INotifyPropertyChanged, IDataErrorInfo
    {
        #region DataErrorInfo
        public string this[string columnName]
        {
            get
            {
                if (validator == null)
                {
                    validator = new CustomerValidator();
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

        #region Поля
        private string firstName;
        private string lastName;
        private string middleName;
        private string telefon;
        private string e_mail;
        private CustomerValidator validator;
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
