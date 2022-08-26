using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Beauty_Motos
{
    internal class Valida_FrmCliente
    {
        public static bool ValidarCamposDoFormCliente(Client client)
        {

            bool retorno = true;

            if (string.IsNullOrEmpty(client.Nome))
            { MessageBox.Show("Informe o nome completo do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (string.IsNullOrEmpty(client.Telefone))
            { MessageBox.Show("Informe os onze digitos do numero de telefone do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (client.CPF.Length < 14)
            { MessageBox.Show("Informe os onze digitos do CPF do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (string.IsNullOrEmpty(client.Logradouro))
            { MessageBox.Show("Informe o logradouro do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (client.CEP.Length < 8)
            { MessageBox.Show("Informe os oito digitos do CEP do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (string.IsNullOrEmpty(client.Bairro))
            { MessageBox.Show("Informe o bairro do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (string.IsNullOrEmpty(client.Cidade))
            { MessageBox.Show("Informe a cidade do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            return retorno;
        }

    }
}
