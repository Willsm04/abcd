using System.Windows;



namespace Beauty_Motos
{
    /// <summary>
    /// Lógica interna para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            Validacao_Login validaLoginDoUsuario = new Validacao_Login(txtUsuario.Text, txtSenha.Password);

            if (validaLoginDoUsuario.ValidaLogin())
            {
                Close();
                var principal = new MainWindow1();
                principal.Show();

                principal.menuItemDesconectar.IsEnabled = true;
                principal.menuItemConectar.IsEnabled = false;
                principal.menuItemClientes.IsEnabled = true;
                principal.menuItemMotos.IsEnabled = true;

            }

        }

        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {
            Close();
            var principal = new MainWindow1();
            principal.Show();
        }

    }
}
