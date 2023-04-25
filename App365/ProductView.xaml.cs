using System;
using System.Collections.Generic;
using System.ComponentModel;
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

	public class SortItem {
		public string Name { get; set; }
		public SortDescription Sort { get; set; }
	}

	public partial class ProductView : Page {
		private Connection connection;

		public List<SortItem> SortDescriptions { get; set; }
		public SortItem SelectedSortDescription { get; set; }
		public Category SelectedCategory { get; set; }
	
		public ProductView(Connection connection) {
			InitializeComponent();
			this.connection = connection;

			ReloadProducts();

			Binding categoryBinding = new Binding();
			categoryBinding.Source = connection.Category.ToList();
			FilterByCategory.SetBinding(
				ItemsControl.ItemsSourceProperty, categoryBinding);

			SortDescriptions = new List<SortItem>()  {
				new SortItem() {
					Name = "По возврастанию (цена)",
					Sort = new SortDescription("Price", ListSortDirection.Ascending),
				},
				new SortItem() {
					Name = "По убыванию (цена)",
					Sort = new SortDescription("Price", ListSortDirection.Descending),
				},
				new SortItem() {
					Name = "По возврастанию (количество)",
					Sort = new SortDescription("Count", ListSortDirection.Ascending),
				},
				new SortItem() {
					Name = "По убыванию (количество)",
					Sort = new SortDescription("Count", ListSortDirection.Descending),
				}
			};

			DataContext = this;
		}

		public void ReloadProducts() {
			Binding binding = new Binding();
			binding.Source = connection.Product.ToList();
			ProductList.SetBinding(ItemsControl.ItemsSourceProperty, binding);
		}

		private void ApplySort() {
			var view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource);
			if (view == null || SelectedSortDescription == null)
				return;

			view.SortDescriptions.Clear();
			view.SortDescriptions.Add(SelectedSortDescription.Sort);
		}

		private void Filter() {
			var view = CollectionViewSource.GetDefaultView(ProductList.ItemsSource);
			if (view == null)
				return;

			var selectedCategory = FilterByCategory.SelectedItem as Category;
			string searchText = SearchText.Text.Trim();

			view.Filter = o => {
				Product product = o as Product;
				if (product == null)
					return false;

				bool isVisible = true;

				// "Hello world"
				// "Rr"
				// -1

				if (searchText.Length > 0) {
					isVisible = product.Name.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1 ||
						product.Description.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) != -1;
				}

				if (selectedCategory != null) {
					isVisible = isVisible && product.Category == selectedCategory.Name;
				}

				return isVisible;
			};

		}

		private bool ASD(object o) {
			return true;
		}

		private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			ApplySort();
		}

		private void TextBox_TextChanged(object sender, TextChangedEventArgs e) {
			Filter();
		}

		private void FilterByCategory_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Filter();
		}

		private void ProductList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
			Product selectedProduct =
				ProductList.SelectedItem as Product;
			if (selectedProduct == null)
				return;

			Pages.ProductEdit.SetProduct(selectedProduct);
			NavigationService.Navigate(Pages.ProductEdit);
		}

		
	}
}
