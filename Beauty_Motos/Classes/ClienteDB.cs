using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Beauty_Motos
{
    internal class ClienteDB
    {
        public  ClienteDB(string nome, string telefone, string cpf, string logradouro, string cep, string bairro, string cidade)
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

        public string InsertNoSQL()
        {

            string sql = @"INSERT INTO TB_Cliente
                               (Nome
                               ,Telefone
                               ,CPF
                               ,Logradouro
                               ,CEP
                               ,Bairro
                               ,Cidade)
                                VALUES";
            sql += "('" + Nome + "',";
            sql += "'" + Convert.ToString(Telefone) + "',";
            sql += "'" + Convert.ToString(CPF) + "',";
            sql += "'" + Convert.ToString(Logradouro) + "',";
            sql += "'" + Convert.ToString(CEP) + "',";
            sql += "'" + Bairro + "',";
            sql += "'" + Cidade + "');";

            return sql;
        }
      
        public bool AddClienteNoSQL()
        {
            bool retorno = false;
            IDbTransaction transacao = null;
            SqlConnection conexao = null;
            try
            {
                
                string sql = InsertNoSQL();
                conexao = new SqlConnection(@"Data Source = WK-DEV-06; Initial Catalog = Banco de Dados Concessionaria; Integrated Security = True" );
                SqlCommand cmd = new SqlCommand(sql, (SqlConnection)conexao, (SqlTransaction)transacao);
                IDbCommand comando = (IDbCommand)cmd;
 
                comando.CommandType = CommandType.Text;
                conexao.Open();
                transacao = conexao.BeginTransaction();
                comando.Transaction = transacao;
                
                int ValidarTransacao = comando.ExecuteNonQuery();

                if (ValidarTransacao < 1)
                {
                    throw new Exception("Nenhuma linha foi afetada.");
                }
                else
                {
                    retorno = true;
                }
                transacao.Commit();
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();

                return retorno;

            }
            catch (Exception ex)
            {
                if (transacao != null) transacao.Rollback();
                throw new Exception("Erro ao inserir dados no banco " + ex.Message);
            }
            finally
            {
                if (transacao != null) transacao.Dispose();
                if (conexao != null) conexao.Close();
            }
        }

        public string UpdateNoSQL()
        {
       
            string sql = @"Update TB_Cliente 
                            SET ";
            sql += "Nome = '" + Nome + "',";
            sql += "Telefone = '" + Convert.ToString(Telefone) + "',";
            sql += "CPF = '" + Convert.ToString(CPF) + "',";
            sql += "Logradouro = '" + Logradouro + "',";
            sql += "CEP = '" + Convert.ToString(CEP) + "',";
            sql += "Bairro = '" + Bairro + "',";
            sql += "Cidade = '" + Cidade + "'";
            sql += " WHERE CPF = '" + CPF + "';";

            return sql;
        }

        public void AlterarDadosDoSQL()
        {
            SqlConnection conexao = null;
            conexao = new SqlConnection(@"Data Source = WK-DEV-06; Initial Catalog = Banco de Dados Concessionaria; Integrated Security = True");
            
            try
            {
                string sql = UpdateNoSQL();
                conexao.Open();
                SqlCommand command = new SqlCommand(sql, conexao);
                command.ExecuteNonQuery();
                conexao.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar dados no banco " + ex.Message);
            }

        }

        public void DeletarClienteDoSQL()
        {
            SqlConnection conexao = null;
            conexao = new SqlConnection(@"Data Source = WK-DEV-06; Initial Catalog = Banco de Dados Concessionaria; Integrated Security = True");

                try
                {
                    conexao.Open();
                    SqlCommand comando = conexao.CreateCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "DELETE FROM TB_Cliente WHERE CPF = '" + CPF + "'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao excluir dados no banco " + ex.Message);
                }   
        
        }

        public void PesquisarCliente()
        {
            SqlConnection conexao = null;
            conexao = new SqlConnection(@"Data Source = WK-DEV-06; Initial Catalog = Banco de Dados Concessionaria; Integrated Security = True");

            try
            {
                conexao.Open();
                SqlCommand comando = conexao.CreateCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT FROM TB_Cliente WHERE CPF = '" + CPF + "'";
                comando.ExecuteNonQuery();
                conexao.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao pesquisar dados no banco " + ex.Message);
            }

        }
    }
}
