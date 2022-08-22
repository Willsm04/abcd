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
    /// Lógica interna para FrmPerguntaSeDeleta.xaml
    /// </summary>
    public partial class FrmPerguntaSeDeleta : Window
    {
        public FrmPerguntaSeDeleta()
        {
            InitializeComponent();
        }

        public bool retorno;

        private void btnSim_Click(object sender, RoutedEventArgs e)
        {
           retorno = true;
            Close();
        }

        private void btnNao_Click(object sender, RoutedEventArgs e)
        {
            retorno = false;
            Close();
        }
    }
}
