using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Beauty_Motos
{
    internal class Mascara_Texbox
    {
        public string TelefoneCelular { get; set; }
        public string CPF { get; set; }
        public string CEP { get; set; }
        public string DataFabricacao { get; set; }


        public string MascaraTelefoneCelular(string Telefone)
        {
            TelefoneCelular = Telefone;

            if (!string.IsNullOrEmpty(TelefoneCelular))
            {
                string telefoneSemParenteses = TelefoneCelular.Replace("(", "").Replace(")", "");
                long telefone = Convert.ToInt64(telefoneSemParenteses);

                if (telefoneSemParenteses.Length == 11)
                {
                    string telefoneComMascara = string.Format(@"{0:(00)000000000}", telefone);
                    TelefoneCelular = telefoneComMascara;
                }
            }
            return TelefoneCelular;

        }

        public string MascaraCpf(string Cpf)
        {
            CPF = Cpf;

            if (!string.IsNullOrEmpty(Cpf))
            {
                string cpfSemPontuacao = CPF.Replace(".", "").Replace("-", "");
                long cpf = Convert.ToInt64(cpfSemPontuacao);

                if (cpfSemPontuacao.Length == 11)
                {
                    string cpfComMascara = String.Format(@"{0:000\.000\.000\-00}", cpf);
                    CPF = cpfComMascara;
                }


            }
            return CPF;
        }

        public string MascaraCep(string Cep)
        {
            CEP = Cep;
            if (!string.IsNullOrEmpty(Cep))
            {
                string cepSemPontuacao = Cep.Replace("-", "");
                long cep = Convert.ToInt64(cepSemPontuacao);

                if (cepSemPontuacao.Length == 8)
                {
                    string cepFormatado = String.Format(@"{0:000\00\-000}", cep);
                    CEP = cepFormatado;
                }
            }
            return CEP;
        }

        public string MascarDataFabricacao(string Data)
        {
            DataFabricacao = Data;

            if (!string.IsNullOrEmpty(Data))
            {
                string dataSemPontuacao = Data.Replace("/", "");
                long data = Convert.ToInt64(dataSemPontuacao);

                if (dataSemPontuacao.Length == 8)
                {
                    string dataFormatada = String.Format(@"{0:00\/00\/0000}", data);
                    DataFabricacao = dataFormatada;
                }
            }
            return DataFabricacao;
        }
    }
}
