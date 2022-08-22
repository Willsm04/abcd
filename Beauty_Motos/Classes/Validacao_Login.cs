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
        public string Senha { get; private set; }

        private string passwordUser = "safeweb";

        public bool ValidaLogin()
        {
            bool senha = true;

            if (string.IsNullOrEmpty(Usuario))
            {
                MessageBox.Show("Informe o seu usuario", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                senha = false;
            }
            else if (Senha != passwordUser)
            { 
                MessageBox.Show("Senha inválida", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                senha = false;          
            }
            else
            {
                MessageBox.Show("Bem vindo " + Usuario, "Mensagem de Sucesso",MessageBoxButton.OK,MessageBoxImage.Information);
                Senha = passwordUser;
            }
            return senha;
        }
    }
}
