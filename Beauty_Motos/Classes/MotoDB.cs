using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Beauty_Motos
{
    internal class MotoDB
    {

      public static List<Moto> listaMoto = new List<Moto>();
        public static void CarregarDadosNoDataGrid(DataGrid datagrid)
        {
            listaMoto = new List<Moto>();
            datagrid.ItemsSource = null;
            string connstring = ConfigurationManager.ConnectionStrings["stringDeConexao"].ConnectionString;

            using (SqlConnection conexao = new SqlConnection(connstring))
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand($"SELECT * FROM TB_Moto", conexao);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    string idMoto = (string)reader["IdMoto"];
                    string nome = (string)reader["Nome"];
                    string categoria = (string)reader["Categoria"];
                    string preco = (string)reader["Preco"];
                    string dataFabricacao = (string)reader["DataFabricacao"];

                    Moto moto = new Moto(idMoto, nome, categoria, preco, dataFabricacao);

                    listaMoto.Add(moto);
                }

                datagrid.ItemsSource = listaMoto;
                conexao.Close();
            }
        }

        #region CRUD
        public static string Insert(Moto moto)
        {
            string sql = @"INSERT INTO TB_Moto
                               (IdMoto
                               ,Nome
                               ,Categoria
                               ,Preco
                               ,DataFabricacao)
                                VALUES";
            sql += "('" + moto.Id + "',";
            sql += "'" + moto.NomeMoto + "',";
            sql += "'" + moto.Cat + "',";
            sql += "'" + Convert.ToString(moto.Preco) + "',";
            sql += "'" + Convert.ToString(moto.DataFabricacao) + "');";

            return sql;
        }

        static string stringConn = System.Configuration.ConfigurationManager.ConnectionStrings["stringDeConexao"].ConnectionString;
        static SqlConnection conexao = null;

        public static bool AddMotoNoSQL(Moto moto)
        {
            bool retorno = false;
            IDbTransaction transacao = null;           
            try
            {
                string sql = Insert(moto);
                conexao = new SqlConnection(stringConn);
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

        public static string Update(Moto moto)
        {
            string sql = @"UPDATE TB_Moto
                            SET ";
            sql += "IdMoto = '" + moto.Id + "',";
            sql += "Nome = '" + moto.NomeMoto + "',";
            sql += "Categoria = '" + moto.Cat + "',";
            sql += "Preco = '" + Convert.ToString(moto.Preco) + "',";
            sql += "DataFabricacao = '" + Convert.ToString(moto.DataFabricacao) + "'";
            sql += "WHERE IdMoto = '" + moto.Id + "';";
            return sql;
        }
           
        public static bool AlterarDadosDoSQL(Moto moto)
        {
            conexao = new SqlConnection(stringConn);
            bool retorno = false;
            try
            {
                string sql = Update(moto);
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

        public static bool DeletarMotoDoSQL(Moto moto)
        {
            bool retorno = false;

            conexao = new SqlConnection(stringConn);

            try
            {
                conexao.Open();
                SqlCommand comando = conexao.CreateCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "DELETE FROM TB_Moto WHERE IdMoto = '" + moto.Id + "'";
                comando.ExecuteNonQuery();
                conexao.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao excluir dados no banco " + ex.Message);
            }
            
            return retorno;

        }

        #endregion

    }
}
