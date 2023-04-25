using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace App365 {


	public partial class AuthorzationPage : Page {
		private Connection connection;

		public AuthorzationPage(Connection connection) {
			InitializeComponent();
			this.connection = connection;
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			string login = tbLogin.Text.Trim();
			if (login.Length == 0) {
				MessageBox.Show("Введите логин");
				return;
			}
			string password = tbPassword.Text.Trim();
			if (password.Length == 0) {
				MessageBox.Show("Введите пароль");
				return;
			}

			User user = connection.Users.Where(u => u.Login == login && u.Password == password).FirstOrDefault();

			NavigationService.Navigate(Pages.ProductView);
		}
	}
}
