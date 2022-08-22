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
    /// Lógica interna para TelaDeAviso.xaml
    /// </summary>
    public partial class TelaDeAviso : Window
    {
        public TelaDeAviso()
        {
            InitializeComponent();
        }

        private void btnSim_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow1 principal = new MainWindow1();
            principal.Show();
            principal.menuItemClientes.IsEnabled = false;
            principal.menuItemMotos.IsEnabled = false;
            principal.menuItemDesconectar.IsEnabled = false;
        }

        private void btnNao_Click(object sender, RoutedEventArgs e)
        {
            Close();
            MainWindow1 principal = new MainWindow1();
            principal.Show();
            principal.menuItemClientes.IsEnabled = true;
            principal.menuItemMotos.IsEnabled = true;
            principal.menuItemDesconectar.IsEnabled = true;
            principal.menuItemConectar.IsEnabled = false;

        }
    }
}
