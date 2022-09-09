using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Beauty_Motos
{
    internal class Valida_FrmMoto
    {

        public static bool ValidaCamposDoFormMotos(Moto moto)
        {
            bool cadastroHeValido = false;

            if (string.IsNullOrEmpty(moto.Id))
                 MessageBox.Show("Informe o id da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (string.IsNullOrEmpty(moto.NomeMoto))
                MessageBox.Show("Informe o nome da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (string.IsNullOrEmpty(moto.Cat))
                 MessageBox.Show("Informe a categoria da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            else if (string.IsNullOrEmpty(moto.Preco))
                MessageBox.Show("Informe o preço da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); 

            else if (moto.DataFabricacao.Length < 8)
                 MessageBox.Show("Informe os oitos digitos da data de fabricação da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);

            else
                cadastroHeValido = true;
            
            return cadastroHeValido;
        }
    }
}
