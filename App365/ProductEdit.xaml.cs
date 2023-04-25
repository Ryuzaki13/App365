using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

	public partial class ProductEdit : Page {
		private Connection connection;

		public ProductEdit(Connection connection) {
			InitializeComponent();
			this.connection = connection;

			Binding binding = new Binding();
			binding.Source = connection.Category.ToList();
			cbCategory.SetBinding(ComboBox.ItemsSourceProperty, binding);
			cbCategory.DisplayMemberPath = "Name";
		}

		public void SetProduct(Product product) {
			ProductBlock.DataContext = product;
		}

		private void SelectImage_Click(object sender, RoutedEventArgs e) {
			Product product = ProductBlock.DataContext as Product;
			if (product == null)
				return;

			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Filter = "Файлы изображений|*.jpg;*.jpeg;*.png;";
			fileDialog.Multiselect = false;
			if (fileDialog.ShowDialog() == true) {
				Stream fileStream = fileDialog.OpenFile();
				product.Image = new byte[fileStream.Length];
				fileStream.Read(product.Image, 0, (int)fileStream.Length);

				fileStream.Seek(0, SeekOrigin.Begin);
				BitmapImage bitmap = new BitmapImage();
				bitmap.BeginInit();
				bitmap.StreamSource = fileStream;
				bitmap.EndInit();
				ImagePreview.Source = bitmap;
			}
		}

		private void buttonSave_Click(object sender, RoutedEventArgs e) {
			connection.SaveChanges();
			if (NavigationService.CanGoBack) {
				NavigationService.GoBack();
			}
		}

		private void buttonRemove_Click(object sender, RoutedEventArgs e) {
			Product product = ProductBlock.DataContext as Product;
			
			connection.Product.Remove(product);
			connection.SaveChanges();

			Pages.ProductView.ReloadProducts();
		}
	}
}
