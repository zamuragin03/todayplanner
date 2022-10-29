using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

namespace todayplanner
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>


    delegate void OnChanged(string type);

    public partial class MainWindow : Window
    {
        SQLiteConnection db;
        private string content;
        private OnChanged onChangedHandler;
        public MainWindow()
        {
            InitializeComponent();
            db = new SQLiteConnection("Data Source = tasks.db;");
            db.Open();
            onChangedHandler = Update;
            Update("Срочно и важно");
            Update("Не срочно и важно");
            Update("Срочно и не важно");
            Update("Не срочно и не важно");

        }

        void Update(string type)
        {
            switch (type)
            {
                case "Срочно и важно":
                    UpdateListBox(ListBox1, type);
                    break;
                case "Не срочно и важно":
                    UpdateListBox(ListBox2, type);
                    break;
                case "Срочно и не важно":
                    UpdateListBox(ListBox3, type);
                    break;
                case "Не срочно и не важно":
                    UpdateListBox(ListBox4, type);
                    break;
            }
        }

        void UpdateListBox(ListBox listbox, string type)
        {
            SQLiteCommand command;
            SQLiteDataReader reader;
            command = new SQLiteCommand($"Select Text from data where column= '{type}'", db);
            reader = command.ExecuteReader();
            foreach (DbDataRecord el in reader)
            {
                listbox.Items.Add(el["Text"]);
            }
            command.Dispose();
        }
        //protected override void OnClosed(EventArgs e)
        //{
        //    MessageBoxResult result = MessageBox.Show("Сохранить данные?", "Planner", MessageBoxButton.YesNo);
        //    switch (result)
        //    {
        //        case MessageBoxResult.Yes:
        //            break;
        //        case MessageBoxResult.No:
        //            break;
        //    }
        //}

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TextRange textRange = new TextRange(Content_box.Document.ContentStart, Content_box.Document.ContentEnd);
            content = textRange.Text;
            string type = message_type.Text;
            Content_box.Document.Blocks.Clear();
            switch (type)
            {
                case "Срочно и важно":
                    SetData("Срочно и важно");
                    break;
                case "Не срочно и важно":
                    SetData("Не срочно и важно");
                    break;
                case "Срочно и не важно":
                    SetData("Срочно и не важно");
                    break;
                case "Не срочно и не важно":
                    SetData("Не срочно и не важно");
                    break;
                default:
                    MessageBox.Show("Выберите важность");
                    return;
            }

            content = "";
        }

        void SetData(string str)
        {
            SQLiteCommand command;
            command = new SQLiteCommand("insert into data values (@text,@column)", db);
            command.Parameters.AddWithValue("@text", content);
            command.Parameters.AddWithValue("@column", str);
            command.ExecuteNonQuery();
            onChangedHandler.Invoke(str);
        }

        private void ListBox1_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            string temp = ((ListBox)sender).SelectedItem.ToString();
            MessageBox.Show(temp);

        }

        //private void ListBox1_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    string temp = ((ListBox)sender).SelectedItem.ToString();
        //    MessageBox.Show(temp);
        //}

        private void Delete_selected(object sender, RoutedEventArgs e)
        {
            SQLiteCommand command = new SQLiteCommand("delete from data", db);
            command.ExecuteNonQuery();
            command.Dispose();
            Update("Срочно и важно");
            Update("Не срочно и важно");
            Update("Срочно и не важно");
            Update("Не срочно и не важно");

        }
    }

}
