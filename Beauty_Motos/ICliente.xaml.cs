
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

        private void HabilitaBtn()
        {
            if(txtNomeCompleto.Text != "" || txtTelefoneCelular.Text != "")
                btnSalvar.IsEnabled = true;
            else
                btnSalvar.IsEnabled = false;
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
                    txtNomeCompleto.Text = linha.Nome;
                    txtTelefoneCelular.Text = linha.Telefone;
                    txtCPF.Text = linha.CPF;
                    txtLogradouro.Text = linha.Logradouro;
                    txtCEP.Text = linha.CEP;
                    txtBairro.Text = linha.Bairro;
                    txtCidade.Text = linha.Cidade;
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
            bool retorno = false;
            foreach (var linha in ClienteDB.listaCliente)
            {
              
                if (linha.CPF.Equals(txtCPF.Text))
                    
                    retorno = true;    
                     break;             
            }
            
            return retorno;
        }

        private void btnPesquisar_Click(object sender, RoutedEventArgs e)
        {
 
            foreach (var linha in ClienteDB.listaCliente)
            {
                if (txtPesquisa.Text.Equals(linha.CPF))
                {
                    
                }
            }
        }

        private void SalvarDadosDoFormulario()
        {
            Client cliente = new Client(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);

            if (Valida_FrmCliente.ValidarCamposDoFormCliente(cliente)) 
            
                if (VerificarSeExiteCPFCliente() == true)
                    MessageBox.Show("CPF ja existente na base de dados", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                
                else
                    ClienteDB.AddClienteNoSQL(cliente);
                    ClienteDB.CarregarDadosNoDataGrid(dataGrid);
                    MessageBox.Show("Cadastro concluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCamposDoForm(); 
        }

        private void EventoClickBtnFechar(object sender, RoutedEventArgs e)
        {
            FrmPerguntaSeDeleta pergunta = new FrmPerguntaSeDeleta();
            pergunta.label.Content = "Deseja salvar o cliente antes de sair?";
            pergunta.ShowDialog();

            if (pergunta.retorno == true)
                SalvarDadosDoFormulario();

            else
                Close();
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            SalvarDadosDoFormulario();
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            Client cliente = new Client(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text,  txtBairro.Text, txtCidade.Text);

            bool retorno = VerificarSeExiteCPFCliente();
            if (txtCPF.Text != "")
            {
                if (retorno == true)
                {
                    ClienteDB.AlterarDadosDoSQL(cliente);
                    MessageBox.Show("Dados alterados com sucesso. ", "Mensagem de sucesso ", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClienteDB.CarregarDadosNoDataGrid(dataGrid);
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
                    Client cliente = new Client(txtNomeCompleto.Text, txtTelefoneCelular.Text, txtCPF.Text, txtLogradouro.Text, txtCEP.Text, txtBairro.Text, txtCidade.Text);
                    ClienteDB.DeletarClienteDoSQL(cliente);
                    MessageBox.Show("Cliente excluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClienteDB.CarregarDadosNoDataGrid(dataGrid);
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
    }
}
