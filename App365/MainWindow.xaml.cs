using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace App365 {
	public partial class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();

			Pages.Connection = new Connection();

			AppFrame.Navigate(Pages.AuthorzationPage);
		}		
	}
}
