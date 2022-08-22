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
        public Valida_FrmMoto(string id, string nomeMoto, string cat, string preco, string dataFabricacao)
        {
            Id = id;
            NomeMoto = nomeMoto;
            Cat = cat;
            Preco = preco;
            DataFabricacao = dataFabricacao;
        }

        public string Id { get; set; }
        public string NomeMoto { get; set; }
        public string Cat { get; set; }
        public string Preco { get; set; }
        public string DataFabricacao { get; set; }

        public bool ValidaCamposDoFormMotos()
        {
            bool cadastro = true;
            if (string.IsNullOrEmpty(Id))
            { MessageBox.Show("Informe o id da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); cadastro = false; }

            else if (string.IsNullOrEmpty(NomeMoto))
            { MessageBox.Show("Informe o nome da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); cadastro = false; }

            else if (string.IsNullOrEmpty(Cat))
            { MessageBox.Show("Informe a categoria da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); cadastro = false; }

            else if (string.IsNullOrEmpty(Preco))
            { MessageBox.Show("Informe o preço da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); cadastro = false; }

            else if (DataFabricacao.Length < 8)
            { MessageBox.Show("Informe os oitos digitos da data de fabricação da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); cadastro = false; }

            return cadastro;
        }
    }
}
