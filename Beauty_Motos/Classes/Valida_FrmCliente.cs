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
        public Valida_FrmCliente (string nome, string telefone, string cpf, string logradouro, string cep, string bairro, string cidade)
        {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
            Logradouro = logradouro;
            CEP = cep;
            Bairro = bairro;
            Cidade = cidade;
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; private set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }

        public bool ValidarCamposDoFormCliente()
        {
            bool retorno = true;

            //if (string.IsNullOrEmpty(Nome))
            //     { MessageBox.Show("Informe o nome completo do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

             if (string.IsNullOrEmpty(Telefone))
                { MessageBox.Show("Informe os onze digitos do numero de telefone do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (CPF.Length < 14) 
                { MessageBox.Show("Informe os onze digitos do CPF do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (string.IsNullOrEmpty(Logradouro)) 
                { MessageBox.Show("Informe o logradouro do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (CEP.Length < 8) 
                { MessageBox.Show("Informe os oito digitos do CEP do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (string.IsNullOrEmpty(Bairro))
                { MessageBox.Show("Informe o bairro do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            else if (string.IsNullOrEmpty(Cidade)) 
                { MessageBox.Show("Informe a cidade do Cliente.", "Menssagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); retorno = false; }

            return retorno;
        }
    }
}
