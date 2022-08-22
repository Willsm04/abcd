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

namespace Beauty_Motos
{
    /// <summary>
    /// Lógica interna para MainWindow1.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        public MainWindow1()
        {
            InitializeComponent();
            menuItemClientes.IsEnabled = false;
            menuItemMotos.IsEnabled = false;
            menuItemDesconectar.IsEnabled = false;

        }
 private void EventoClickConectar(object sender, RoutedEventArgs e)
        {   
            Login f = new Login();
            f.ShowDialog();
            Close();
        }

        private void EventoClickMotos(object sender, RoutedEventArgs e)
        {
            Motos f = new Motos();
            f.Show();
            Close();
        }

        private void EventoClickClientes(object sender, RoutedEventArgs e)
        {
            Cliente f = new Cliente();
            f.Show();
            Close();
        }

        private void EventoClickDesconectar(object sender, RoutedEventArgs e)
        {
            TelaDeAviso aviso = new TelaDeAviso();
            aviso.ShowDialog();
            Close();
            
        }

        private void EventoClickBtnFechar(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
