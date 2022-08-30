using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty_Motos
{
    public class Cliente
    {
       public Cliente(string nome, string telefone, string cpf, string logradouro, string cep,  string cidade, string bairro)
       {
            Nome = nome;
            Telefone = telefone;
            CPF = cpf;
            Logradouro = logradouro;
            CEP = cep;
            Cidade = cidade;
            Bairro = bairro;
        }
        public Cliente()
        {
        }

        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string CPF { get; private set; }
        public string Logradouro { get; set; }
        public string CEP { get; set; }
        public string Cidade { get; set; }
        public string Bairro { get; set; }

    }
}
