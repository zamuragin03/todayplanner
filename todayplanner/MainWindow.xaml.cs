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

namespace todayplanner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            if (TextBox1.Text!="")
            {
                listbox1.Items?.Add(TextBox1?.Text);
            }

            TextBox1.Text = null;
        }

        private void Remove(object sender, RoutedEventArgs e)
        {
            try
            {
                listbox1.Items.RemoveAt(listbox1.Items.IndexOf(listbox1?.SelectedItem));
            }
            catch (InvalidOperationException)
            {

            }
            catch (ArgumentOutOfRangeException)
            {

            }

        }
    }
}
