using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty_Motos
{
    internal class Moto
    {
        public Moto(string id, string nomeMoto, string cat, string preco, string dataFabricacao)
        {
            Id = id;
            NomeMoto = nomeMoto;
            Cat = cat;
            Preco = preco;
            DataFabricacao = dataFabricacao;
        }
        public Moto() { }
        
        public string Id { get; set; }
        public string NomeMoto { get; set; }
        public string Cat { get; set; }
        public string Preco { get; set; }
        public string DataFabricacao { get; set; }
    }
}
