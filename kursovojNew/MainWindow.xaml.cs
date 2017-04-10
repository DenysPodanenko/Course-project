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

namespace kursovojNew
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Uri uri = null;
        public MainWindow()
        {
            InitializeComponent();
            uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "main1.jpg");
            imgMainBack.Source = new BitmapImage(uri);
            uri = new Uri(AppDomain.CurrentDomain.BaseDirectory+"fiksiki.png");
            imgFiksiki.Source = new BitmapImage(uri);
            uri = new Uri(AppDomain.CurrentDomain.BaseDirectory+"luntik.png");
            imgLuntik.Source = new BitmapImage(uri);

        }

        private void clickFirstApp(object sender, MouseButtonEventArgs e)
        {
            FirstApp window1 = new FirstApp();
            this.Close();
            window1.ShowDialog();
        }

        private void clickSecondApp(object sender, MouseButtonEventArgs e)
        {
            SecondApp window2 = new SecondApp();
            this.Close();
            window2.ShowDialog();
        }

        private void lblFirstAppMouseEnter(object sender, MouseEventArgs e)
        {
            lblFirstApp.Foreground = Brushes.Red;
        }

        private void lblFirstAppMouseLeave(object sender, MouseEventArgs e)
        {
            lblFirstApp.Foreground = Brushes.Black;
        }

        private void lblSecondAppMouseEnter(object sender, MouseEventArgs e)
        {
            lblSecondApp.Foreground = Brushes.Red;
        }

        private void lblSecondAppMouseLeave(object sender, MouseEventArgs e)
        {
            lblSecondApp.Foreground = Brushes.Black;
        }
    }
}
