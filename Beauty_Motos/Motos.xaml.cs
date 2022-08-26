using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Beauty_Motos
{
    /// <summary>
    /// Lógica interna para Motos.xaml
    /// </summary>
    public partial class Motos : Window
    {
        public Motos()
        {
            InitializeComponent();
            CarregarDadosNoDataGrid();
        }

        List<MotoDB> listaMoto = new List<MotoDB>();
        public void CarregarDadosNoDataGrid()
        {
            listaMoto = new List<MotoDB>();
            string connstring = ConfigurationManager.ConnectionStrings["stringDeConexao"].ConnectionString;
            dataGrid.ItemsSource = null;
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

                    MotoDB cliente = new MotoDB(idMoto, nome, categoria, preco, dataFabricacao);

                    listaMoto.Add(cliente);

                }

                dataGrid.ItemsSource = listaMoto;
                conexao.Close();
            }
        }
        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            CarregarDadosNoDataGrid();
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var linha in listaMoto)
            {
                if (dataGrid.SelectedItem == linha)
                {
                    txtId.Text = linha.Id;
                    txtNomeMoto.Text = linha.NomeMoto;
                    txtCat.Text = linha.Cat;
                    txtPreco.Text = linha.Preco;
                    txtDataFabricacao.Text = linha.DataFabricacao;

                }

            }
        }
        public void LimparCamposDoForm()
        {
            txtId.Clear();
            txtNomeMoto.Clear();
            txtCat.Clear();
            txtPreco.Clear();
            txtDataFabricacao.Clear();
        }
        public bool VerificaSeExiteIdMoto()
        {
            bool retorno = true;
            foreach (var linha in listaMoto)
            {

                if (linha.Id.Equals(txtId.Text))
                {
                    retorno = false;
                    break;
                }

            }

            return retorno;
        }
        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Valida_FrmMoto clsQueValidaFrmMoto = new Valida_FrmMoto(txtId.Text, txtNomeMoto.Text, txtCat.Text, txtPreco.Text, txtDataFabricacao.Text);        
            if (clsQueValidaFrmMoto.ValidaCamposDoFormMotos())
            {
                bool retorno = VerificaSeExiteIdMoto();
                if (retorno == false)
                {
                    MessageBox.Show("Id da moto ja existente na base de dados", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MotoDB clsMotoDB= new MotoDB(txtId.Text, txtNomeMoto.Text, txtCat.Text, txtPreco.Text, txtDataFabricacao.Text);
                    clsMotoDB.AddMotoNoSQL();
                    CarregarDadosNoDataGrid();
                    MessageBox.Show("Cadastro concluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCamposDoForm();
                }
            }
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            MotoDB clsMotoDB = new MotoDB(txtId.Text, txtNomeMoto.Text, txtCat.Text, txtPreco.Text, txtDataFabricacao.Text);
            if (txtId.Text != "")
            {
                bool retorno = VerificaSeExiteIdMoto();
                if (retorno == false)
                {
                    clsMotoDB.AlterarDadosDoSQL();
                    MessageBox.Show("Dados alterados com sucesso. ", "Mensagem de sucesso ", MessageBoxButton.OK, MessageBoxImage.Information);
                    CarregarDadosNoDataGrid();
                    LimparCamposDoForm();
                }
            }
            else
            {
                MessageBox.Show("Id da moto inexistente na base de dados.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDeletar_Click(object sender, RoutedEventArgs e)
        {
            FrmPerguntaSeDeleta pergunta = new FrmPerguntaSeDeleta();
            pergunta.label.Content = "Deseja realmente excluir?";
            pergunta.ShowDialog();
            if (pergunta.retorno == true)
            {
                if (string.IsNullOrEmpty(txtId.Text))
                {
                    MessageBox.Show("Digite ou selecione o CPF que você deseja alterar.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    MotoDB clsMotoDB = new MotoDB(txtId.Text, txtNomeMoto.Text, txtCat.Text, txtPreco.Text, txtDataFabricacao.Text);
                    clsMotoDB.DeletarMotoDoSQL();
                    MessageBox.Show("Moto excluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
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
        private void txtDataFabricacao_TextChanged(object sender, TextChangedEventArgs e)
        {
            var mascara = new Mascara_Texbox();
            txtDataFabricacao.Text = mascara.MascarDataFabricacao(txtDataFabricacao.Text);
        }


        private void BloquearLetrasDaTexbox(object sender, TextCompositionEventArgs e)
        {
            var texbox = sender as TextBox;
            e.Handled = Regex.IsMatch(e.Text, "[^0-9]+");
        }

        private void txtPreco_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            BloquearLetrasDaTexbox(sender, e);
        }

        private void txtDataFabricacao_PreviewTextInput_1(object sender, TextCompositionEventArgs e)
        {
            BloquearLetrasDaTexbox(sender, e);
        }


        private void BloquearNumerosDaTexbox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^a-zA-Z]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void txtCat_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            BloquearNumerosDaTexbox(sender, e);
        }
    
    }
}

