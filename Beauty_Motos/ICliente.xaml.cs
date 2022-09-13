
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
    public partial class ICliente : Window
    {
        public ICliente()
        {
            InitializeComponent();
          
        }

        private void HabilitaBtn()
        {
            if (dataGrid.SelectedItem  == dataGrid.SelectedCells)
                btnSalvar.IsEnabled = false;

            else
                btnSalvar.IsEnabled = true;   
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {      
            ClienteDB.CarregarDadosNoDataGrid(dataGrid);       
        }
        
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var linha in ClienteDB.listaCliente)
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
                    btnSalvar.IsEnabled = false;
                    break;
                }
                else
                    btnSalvar.IsEnabled = true;   
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
            bool existeCPF = false;
            foreach (var linha in ClienteDB.listaCliente)
            {
                if (linha.CPF.Equals(Mascara_Texbox.RemoveMascara(txtCPF.Text)))
                {
                    existeCPF = true;
                    break;
                }                              
            }
            return existeCPF;
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
            if (txtPesquisa.Text.Length < 14)
            {
                MessageBox.Show("Informe os onze digitos do cpf.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                PesquisarCliente();
            }
        }

        private void SalvarDadosDoFormulario()
        {
            Cliente cliente = new Cliente(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);

            if (Valida_FrmCliente.ValidarCamposDoFormCliente(cliente))
            {
                if (VerificarSeExiteCPFCliente() == true)
                {
                    MessageBox.Show("CPF ja existente na base de dados", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {  
                    ClienteDB.AddClienteNoSQL(cliente);
                    ClienteDB.CarregarDadosNoDataGrid(dataGrid);
                    MessageBox.Show("Cadastro concluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCamposDoForm();
                }
                   
            }
                     
        }

        private void EventoClickBtnFechar(object sender, RoutedEventArgs e)
        {
            FrmPerguntaSeDeleta pergunta = new FrmPerguntaSeDeleta();
            pergunta.label.Content = "Deseja salvar o cliente antes de sair?";
            pergunta.ShowDialog();

            if (pergunta.retorno == true)
            {
                SalvarDadosDoFormulario();
                Close();
            }
            else
            {
                Close();
            }
              
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarDadosDoFormulario();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (txtCPF.Text != "")
            {
                if (VerificarSeExiteCPFCliente() == true)
                {
                    Cliente cliente = new Cliente(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);
                    ClienteDB.AlterarDadosDoSQL(cliente);
                    MessageBox.Show("Dados alterados com sucesso. ", "Mensagem de sucesso ", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClienteDB.CarregarDadosNoDataGrid(dataGrid);
                    LimparCamposDoForm();
                }
                else
                {
                    MessageBox.Show("CPF inexistente na base de dados.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }         
            }
            else
            {
                MessageBox.Show("Informe o CPF do cliente.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
               
        }

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            FrmPerguntaSeDeleta pergunta = new FrmPerguntaSeDeleta();
            pergunta.label.Content = "Deseja realmente excluir?";
            pergunta.ShowDialog();

            if (pergunta.retorno == true)
            {
                if (VerificarSeExiteCPFCliente() == false)
                    MessageBox.Show("CPF inexistente na base de dados.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error); 
                
                else
                {
                    Cliente cliente = new Cliente(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);
                    ClienteDB.DeletarClienteDoSQL(cliente);
                    MessageBox.Show("Cliente excluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClienteDB.CarregarDadosNoDataGrid(dataGrid);
                    LimparCamposDoForm();
                }
            }
            else
                MessageBox.Show("Informe o CPF do cliente.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);            
        }

        public void PesquisarCliente()
        {
           /* ClienteDB.conexao = new SqlConnection(ClienteDB.stringConn);
            string sql = $"SELECT * FROM TB_Cliente WHERE CPF LIKE '%{ txtPesquisa.Text}%';";
            SqlCommand comando = new SqlCommand(sql, ClienteDB.conexao);
            ClienteDB.conexao.Open();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(comando);
            da.Fill(dt);
            ClienteDB.conexao.Close();
            da.Dispose();*/
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


        private void txtTelefoneCelular_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtTelefoneCelular.Text = Mascara_Texbox.MascaraTelefoneCelular(txtTelefoneCelular.Text);
        }
        private void txtCPF_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCPF.Text = Mascara_Texbox.MascaraCpf(txtCPF.Text);
        }
        private void txtCEP_TextChanged(object sender, TextChangedEventArgs e)
        {
            txtCEP.Text = Mascara_Texbox.MascaraCep(txtCEP.Text);
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

        private void txtNomeCompleto_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNomeCompleto.Text))
            {
                MessageBox.Show("Informe seu nome completo");
                //txtNomeCompleto.Focus();
            }
        }

        private void txtTelefoneCelular_LostFocus(object sender, RoutedEventArgs e)
        {
           if (string.IsNullOrEmpty(txtTelefoneCelular.Text))
           {
              
               MessageBox.Show("Informe seu telefone");
             //   txtTelefoneCelular.Focus();
           }
        }

        private void txtNomeCompleto_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void txtPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            string qntdCaracter = txtPesquisa.Text;
            if( qntdCaracter.Length > 0)
            {
                var consulta = ClienteDB.listaCliente.Where(cliente => cliente.CPF.Contains (Mascara_Texbox.RemoveMascara(txtPesquisa.Text)));
                dataGrid.ItemsSource = consulta;
                // var consulta = from cliente in ClienteDB.listaCliente where cliente.CPF == txtpesquisa select cliente;
            }
            txtPesquisa.Text = Mascara_Texbox.MascaraCpf(txtPesquisa.Text);
        }
    }
}
