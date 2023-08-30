using System.Windows;
using System.Windows.Input;

namespace ShopWpfCore.View
{
    /// <summary>
    /// Логика взаимодействия для NewCustomerWindow.xaml
    /// </summary>
    public partial class NewCustomerWindow : Window
    {
        public NewCustomerWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Перемещение окна по рабочему столу
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left) { this.DragMove(); }
        }
    }
}
