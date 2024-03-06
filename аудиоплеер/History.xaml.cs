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
using System.Windows.Shapes;

namespace аудиоплеер
{
    /// <summary>
    /// Логика взаимодействия для History.xaml
    /// </summary>
    public partial class History : Window
    {
        public string list1;
        public History(List<string> history)
        {
            InitializeComponent();
            foreach (string file in history)
            {
                if (file.Contains(".mp3") || file.Contains(".m4a") || file.Contains(".wav"))
                {
                    list_history.Items.Add(file + "\n");
                }
            }

        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
             list1 = list_history.SelectedItem.ToString();
             DialogResult = false;
        }
    }
}
