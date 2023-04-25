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

	public partial class CategoryPage : Page {
		private Connection connection;

		public List<Category> categories { get; set; }

		public CategoryPage(Connection connection) {
			InitializeComponent();
			this.connection = connection;

			categories = new List<Category>(
				connection.Category.ToList());

			CategoryBlock.DataContext = new Category();
		}

		private void Button_Click_1(object sender, RoutedEventArgs e) {
			Category category = CategoryBlock.DataContext as Category;
			category.Name = category.Name.Trim();

			if (category.Name.Length == 0) {
				MessageBox.Show("Необходимо ввести название категории");
				return;
			}

			foreach (Category cat in categories) {
				if (cat.Name.ToLower() == category.Name.ToLower()) {
					MessageBox.Show("Данная категория уже есть");
					return;
				}
			}

			connection.Category.Add(category);
			connection.SaveChanges();
			categories.Add(category);

			CategoryBlock.DataContext = new Category();
		}
	}
}
