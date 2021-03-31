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

namespace Projekat.Dijalozi
{
    /// <summary>
    /// Interaction logic for Tutorijal.xaml
    /// </summary>
    public partial class Tutorijal : Window
    {
        public Tutorijal()
        {
            InitializeComponent();
        }
        private void Play_btn(object sender, RoutedEventArgs e)
        {
            myMedia.Play();
        }

        private void Pause_btn(object sender, RoutedEventArgs e)
        {
            myMedia.Pause();
        }
        private void Stop_btn(object sender, RoutedEventArgs e)
        {
            myMedia.Stop();
        }
    }
}
