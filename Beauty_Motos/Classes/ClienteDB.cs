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
using System.Windows.Controls;

namespace Beauty_Motos
{
    public class ClienteDB
    {
       public static List<Cliente> listaCliente = new List<Cliente>();

        public static void CarregarDadosNoDataGrid(DataGrid dataGrid)
        {

            listaCliente = new List<Cliente>();
            dataGrid.ItemsSource = null;
            string connstring = ConfigurationManager.ConnectionStrings["stringDeConexao"].ConnectionString;
       
            using (SqlConnection conexao = new SqlConnection(connstring))
            {
                conexao.Open();
                SqlCommand comando = new SqlCommand($"SELECT * FROM TB_Cliente", conexao);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                   
                    string nome = (string)reader["Nome"];
                    string telefone = (string)reader["Telefone"];
                    string cpf = (string)reader["CPF"];
                    string logradouro = (string)reader["Logradouro"];
                    string cep = (string)reader["CEP"];
                    string bairro = (string)reader["Bairro"];  
                    string cidade = (string)reader["Cidade"];

                    Cliente clienteAux = new Cliente(nome, telefone, cpf, logradouro, cep, bairro, cidade);

                    listaCliente.Add(clienteAux);

                }

                dataGrid.ItemsSource = listaCliente;
                conexao.Close();
            }
        }


        public static string InsertNoSQL(Cliente cliente)
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
            sql += "('" + cliente.Nome + "',";
            sql += "'" + Convert.ToString(cliente.Telefone) + "',";
            sql += "'" + Convert.ToString(cliente.CPF) + "',";
            sql += "'" + Convert.ToString(cliente.Logradouro) + "',";
            sql += "'" + Convert.ToString(cliente.CEP) + "',";
            sql += "'" + cliente.Bairro + "',";
            sql += "'" + cliente.Cidade + "');";

            return sql;
        }

        static string stringConn = System.Configuration.ConfigurationManager.ConnectionStrings["stringDeConexao"].ConnectionString;
        static SqlConnection conexao = null;

        public static bool AddClienteNoSQL(Cliente cliente)
        {
            
            bool retorno = false;
            IDbTransaction transacao = null;
            try
            {
             
                string sql = InsertNoSQL(cliente);
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

        public static string UpdateNoSQL(Cliente cliente)
        {
            string sql = @"Update TB_Cliente 
                            SET ";
            sql += "Nome = '" + cliente.Nome + "',";
            sql += "Telefone = '" + Convert.ToString(cliente.Telefone) + "',";
            sql += "CPF = '" + Convert.ToString(cliente.CPF) + "',";
            sql += "Logradouro = '" + cliente.Logradouro + "',";
            sql += "CEP = '" + Convert.ToString(cliente.CEP) + "',";
            sql += "Bairro = '" + cliente.Bairro + "',";
            sql += "Cidade = '" + cliente.Cidade + "'";
            sql += " WHERE CPF = '" + cliente.CPF + "';";

            return sql;
        }
       
        public static void AlterarDadosDoSQL(Cliente cliente)
        {     
            conexao = new SqlConnection(stringConn); 
            try
            {
                string sql = UpdateNoSQL(cliente);
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

        public static void DeletarClienteDoSQL(Cliente cliente)
        {
            ClienteDB.conexao = new SqlConnection(stringConn);
                try
                {
                    conexao.Open();
                    SqlCommand comando = conexao.CreateCommand();
                    comando.CommandType = CommandType.Text;
                    comando.CommandText = "DELETE FROM TB_Cliente WHERE CPF = '" + cliente.CPF + "'";
                    comando.ExecuteNonQuery();
                    conexao.Close();

                }
                catch (Exception ex)
                {
                    throw new Exception("Erro ao excluir dados no banco " + ex.Message);
                }   
        
        }

       
        public static void PesquisarCliente()
        { 
            Cliente cliente = new Cliente();
            conexao = new SqlConnection(ClienteDB.stringConn);

            try
            {
                conexao.Open();
                SqlCommand comando = conexao.CreateCommand();
                comando.CommandType = CommandType.Text;
                comando.CommandText = "SELECT FROM TB_Cliente WHERE CPF = '" + cliente.CPF + "'";
                comando.ExecuteNonQuery();
                conexao.Close();

            }
            catch (Exception ex)
            {
                throw new Exception("Erro ao pesquisar dados no banco " + ex.Message);
            }

        }
       

        public void BuscarPorCPF()
        {
            conexao = new SqlConnection(stringConn);
            Cliente cliente = new Cliente();

            try
            {
                conexao.Open();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
