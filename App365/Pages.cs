using System.Net;
using System.Net.Security;
using System.Security.RightsManagement;

namespace App365 {
	public class Pages {
		private static Connection connection;

		private static StartPage startPage;
		private static ProductPage productPage;
		private static ProductEdit productEditPage;
		private static CategoryPage categoryPage;
		private static ProductView productView;
		private static AuthorzationPage authorzationPage;

		public static Connection Connection { 
			set {
				connection = value;
			}
		}

		public static StartPage Start {
			get {
				if (startPage == null) {
					startPage = new StartPage(connection);
				}
				return startPage;
			}
		}
		public static ProductPage Product {
			get {
				if (productPage == null) {
					productPage = new ProductPage(connection);
				}
				return productPage;
			}
		}
		public static ProductEdit ProductEdit {
			get {
				if (productEditPage == null) {
					productEditPage = new ProductEdit(connection);
				}
				return productEditPage;
			}
		}
		public static CategoryPage Category {
			get {
				if (categoryPage == null) {
					categoryPage = new CategoryPage(connection);
				}
				return categoryPage;
			}
		}
		public static ProductView ProductView {
			get {
				if (productView == null) {
					productView = new ProductView(connection);
				}
				return productView;
			}
		}
		public static AuthorzationPage AuthorzationPage {
			get {
				if (authorzationPage == null) {
					authorzationPage = new AuthorzationPage(connection);
				}
				return authorzationPage;
			}
		}
	}
}
