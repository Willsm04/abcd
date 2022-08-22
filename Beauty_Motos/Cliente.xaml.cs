
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace Beauty_Motos
{
    /// <summary>
    /// Lógica interna para Cliente.xaml
    /// </summary>
    public partial class Cliente : Window
    {
        public Cliente()
        {
            InitializeComponent(); 
        }


        List<ClienteDB> listaCliente = new List<ClienteDB>();
        public void CarregarDadosNoDataGrid()
        {
            listaCliente = new List<ClienteDB>();
            string connstring = ConfigurationManager.ConnectionStrings["stringDeConexao"].ConnectionString;
            dataGrid.ItemsSource = null;
            using (SqlConnection conexao = new SqlConnection(connstring))
            {        
                conexao.Open();
                SqlCommand comando = new SqlCommand($"SELECT * FROM TB_Cliente", conexao);
                SqlDataReader reader = comando.ExecuteReader();
                while (reader.Read())
                {
                    string cpf = (string)reader["CPF"];
                    string nome = (string)reader["Nome"];
                    string cep = (string)reader["CEP"];
                    string logradouro = (string)reader["Logradouro"];
                    string telefone = (string)reader["Telefone"];
                    string bairro = (string)reader["Bairro"];
                    string cidade = (string)reader["Cidade"];

                    ClienteDB clienteDB = new ClienteDB(nome, telefone, cpf, logradouro, cep, bairro, cidade);

                    listaCliente.Add(clienteDB);
                   
                }

                dataGrid.ItemsSource = listaCliente;
                conexao.Close();
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
            CarregarDadosNoDataGrid();
        }
        
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var linha in listaCliente)
            {
                if (dataGrid.SelectedItem == linha)
                {
                    txtNomeCompleto.Text = linha.Nome;
                    txtTelefoneCelular.Text = linha.Telefone;
                    txtCPF.Text = linha.CPF;
                    txtLogradouro.Text = linha.Logradouro;
                    txtCEP.Text = linha.CEP;
                    txtBairro.Text = linha.Bairro;
                    txtCidade.Text = linha.Cidade;
                }

            }
        }

        public void LimparCamposDoForm()
        {
            txtNomeCompleto.Clear();
            txtTelefoneCelular.Clear();
            txtCPF.Clear();
            txtLogradouro.Clear();
            txtCEP.Clear();
            txtBairro.Clear();
            txtCidade.Clear();
        }

        public bool VerificarSeExiteCPFCliente()
        {
        
            bool retorno = true;;
            foreach (var linha in listaCliente)
            {
              
                if (linha.CPF.Equals(txtCPF.Text))
                {       
                    retorno = false;    
                     break;
                }
               
            }
            
            return retorno;
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
 
            foreach (var linha in listaCliente)
            {
                if (txtPesquisa.Text.Equals(linha.CPF))
                {
                    
                }
            }
        }
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
              Valida_FrmCliente clsQueValida = new Valida_FrmCliente(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);
            if (clsQueValida.ValidarCamposDoFormCliente())
            {
                if (VerificarSeExiteCPFCliente() == false)
                {
                    MessageBox.Show("CPF ja existente na base de dados", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ClienteDB clsClienteDB = new ClienteDB(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);
                    clsClienteDB.AddClienteNoSQL();
                    CarregarDadosNoDataGrid();
                    MessageBox.Show("Cadastro concluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCamposDoForm();
                }
            }
           

        }
        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            bool retorno = VerificarSeExiteCPFCliente();
            ClienteDB clsClienteDB = new ClienteDB(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);
            if (txtCPF.Text != "")
            {
                if (retorno == false)
                {
                    clsClienteDB.AlterarDadosDoSQL();
                    MessageBox.Show("Dados alterados com sucesso. ", "Mensagem de sucesso ", MessageBoxButton.OK, MessageBoxImage.Information);
                    CarregarDadosNoDataGrid();
                    LimparCamposDoForm();
                }
                else
                    MessageBox.Show("CPF inexistente na base de dados.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                
            }
            else  
                MessageBox.Show("CPF inexistente na base de dados.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            

        }
        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            
            FrmPerguntaSeDeleta pergunta = new FrmPerguntaSeDeleta();
            pergunta.label.Content = "Deseja realmente excluir?";
            pergunta.ShowDialog();
            if (pergunta.retorno == true)
            {
                if (string.IsNullOrEmpty(txtCPF.Text))
                {
                    MessageBox.Show("Digite ou selecione o CPF que você deseja excluir.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);        
                }
                else
                {
                    ClienteDB clsClienteDB = new ClienteDB(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);
                    clsClienteDB.DeletarClienteDoSQL();
                    MessageBox.Show("Cliente excluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    CarregarDadosNoDataGrid();
                    LimparCamposDoForm();
                }
            }
           
        }
          
        private void btnVoltar_Click(object sender, RoutedEventArgs e)
        {
            MainWindow1 principal = new MainWindow1();
            principal.Show();
            Close();
            principal.menuItemMotos.IsEnabled = true;
            principal.menuItemClientes.IsEnabled = true;
            principal.menuItemDesconectar.IsEnabled = true;
            principal.menuItemConectar.IsEnabled = false;
        }
   
        private void EventoClickBtnFechar(object sender, RoutedEventArgs e)
        {
            Close();
        }


        private void txtTelefoneCelular_TextChanged(object sender, TextChangedEventArgs e)
        {
            var mascara = new Mascara_Texbox();
            txtTelefoneCelular.Text = mascara.MascaraTelefoneCelular(txtTelefoneCelular.Text);
        }
        private void txtCPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            var mascara = new Mascara_Texbox();
            txtCPF.Text = mascara.MascaraCpf(txtCPF.Text);
        }
        private void txtCEP_TextChanged(object sender, TextChangedEventArgs e)
        {
            var mascara = new Mascara_Texbox();
            txtCEP.Text = mascara.MascaraCep(txtCEP.Text);
        }
        private void txtPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            var mascara = new Mascara_Texbox();
            txtPesquisa.Text = mascara.MascaraCpf(txtPesquisa.Text);
        }

        private void BloquearLetrasDaTexbox(object sender, TextCompositionEventArgs e)
        {
            var texbox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtCPF_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            BloquearLetrasDaTexbox(sender, e);
        }
        private void txtTelefoneCelular_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            BloquearLetrasDaTexbox(sender, e);
        }
        private void txtCEP_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            BloquearLetrasDaTexbox(sender, e);
        }


        private void BloquearNumerosDaTexbox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void txtNomeCompleto_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            BloquearNumerosDaTexbox(sender, e);
        }
        private void txtBairro_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            BloquearNumerosDaTexbox(sender, e);
        }
        private void txtCidade_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            BloquearNumerosDaTexbox(sender, e);
        }

     
    }
}
