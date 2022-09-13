using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Beauty_Motos
{
    internal class Validacao_Login
    {
        public Validacao_Login(string usuario, string senha)
        {
            Usuario = usuario;
            Senha = senha;
        }

        public string Usuario { get; set; }
        private string Senha { get; set; }

        private string passwordUser = "safeweb";

        public bool ValidaLogin()
        {
            bool senhaHeValida = false;

            if (string.IsNullOrEmpty(Usuario))
                MessageBox.Show("Informe o seu usuario", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (Senha != passwordUser)
                MessageBox.Show("Senha inválida", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            else
            {
                MessageBox.Show($"Bem vindo {Usuario}", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                senhaHeValida = true; 
                Senha = passwordUser;
            }

            return senhaHeValida;
        }
    }
}
