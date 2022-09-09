using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Beauty_Motos
{
    internal class Mascara_Texbox
    {
        public static string RemoveMascara(string strSemMascara)
        {
            strSemMascara = Regex.Replace(strSemMascara, "[^0-9]+", "");
          
            return strSemMascara;
        }
      
        public static string MascaraTelefoneCelular(string telefoneCliente)
        {
            if (!string.IsNullOrEmpty(telefoneCliente))
            {
                string telefoneSemParenteses = telefoneCliente.Replace("(", "").Replace(")", "");
                long telefone = Convert.ToInt64(telefoneSemParenteses);

                if (telefoneSemParenteses.Length == 11)
                {
                    string telefoneComMascara = string.Format(@"{0:(00)000000000}", telefone);
                    telefoneCliente = telefoneComMascara;
                }
            }
            return telefoneCliente;

        }

        public static string MascaraCpf(string cpfCliente)
        {
            if (!string.IsNullOrEmpty(cpfCliente))
            {
                string cpfSemPontuacao = cpfCliente.Replace(".", "").Replace("-", "");
                long cpf = Convert.ToInt64(cpfSemPontuacao);

                if (cpfSemPontuacao.Length == 11)
                {
                    string cpfComMascara = String.Format(@"{0:000\.000\.000\-00}", cpf);
                    cpfCliente = cpfComMascara;
                }
            }
            return cpfCliente;
        }
        
        public static string MascaraCep(string cepCliente)
        {
            if (!string.IsNullOrEmpty(cepCliente))
            {
                string cepSemPontuacao = cepCliente.Replace("-", "");
                long cep = Convert.ToInt64(cepSemPontuacao);

                if (cepSemPontuacao.Length == 8)
                {
                    string cepFormatado = String.Format(@"{0:000\00\-000}", cep);
                    cepCliente = cepFormatado;
                }
            }
            return cepCliente;
        }
       
        public static string MascarDataFabricacao(string dataFabricacao)
        {
            if (!string.IsNullOrEmpty(dataFabricacao))
            {
                string dataSemPontuacao = dataFabricacao.Replace("/", "");
                long data = Convert.ToInt64(dataSemPontuacao);

                if (dataSemPontuacao.Length == 8)
                {
                    string dataFormatada = String.Format(@"{0:00\/00\/0000}", data);
                    dataFabricacao = dataFormatada;
                }
            }
            return dataFabricacao;
        }
    }
}
