using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Beauty_Motos
{
    internal class MotoDB
    {
        public MotoDB(string id, string nomeMoto, string cat, string preco, string dataFabricacao)
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

        SqlConnection conexao = null;
        public string Insert()
        {
            string sql = @"INSERT INTO TB_Moto
                               (IdMoto
                               ,Nome
                               ,Categoria
                               ,Preco
                               ,DataFabricacao)
                                VALUES";
            sql += "('" + Id + "',";
            sql += "'" + NomeMoto + "',";
            sql += "'" + Cat + "',";
            sql += "'" + Convert.ToString(Preco) + "',";
            sql += "'" + Convert.ToString(DataFabricacao) + "');";

            return sql;
        }

        public bool AddMotoNoSQL()
        {
            bool retorno = false;
            IDbTransaction transacao = null;
            SqlConnection conexao = null;
            
            try
            {

                string sql = Insert();
                conexao = new SqlConnection(@"Data Source = WK-DEV-06; Initial Catalog = Banco de Dados Concessionaria; Integrated Security = True");
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

        public string Update()
        {
            string sql = @"UPDATE TB_Moto
                            SET ";
            sql += "IdMoto = '" + Id + "',";
            sql += "Nome = '" + NomeMoto + "',";
            sql += "Categoria = '" + Cat + "',";
            sql += "Preco = '" + Convert.ToString(Preco) + "',";
            sql += "DataFabricacao = '" + Convert.ToString(DataFabricacao) + "'";
            sql += "WHERE IdMoto = '" + Id + "';";
            return sql;
        }
           
        public bool AlterarDadosDoSQL()
        {
            conexao = new SqlConnection(@"Data Source = WK-DEV-06; Initial Catalog = Banco de Dados Concessionaria; Integrated Security = True");
            bool retorno = false;
            try
            {
                string sql = Update();
                conexao.Open();
                SqlCommand command = new SqlCommand(sql, conexao);
                command.ExecuteNonQuery();
                conexao.Close();
            }

            catch (Exception ex)
            {
                throw new Exception("Erro ao alterar dados no banco " + ex.Message);
            }
            return retorno;
        }

        public bool DeletarMotoDoSQL()
        {
            bool retorno = false;
            
            conexao = new SqlConnection(@"Data Source = WK-DEV-06; Initial Catalog = Banco de Dados Concessionaria; Integrated Security = True");

            try
            {
                conexao.Open();
                SqlCommand comando = conexao.CreateCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM TB_Moto WHERE IdMoto = '" + Id + "'";
                comando.ExecuteNonQuery();
                conexao.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir dados no banco " + ex.Message);
            }
            
            return retorno;

        }

    }
}
