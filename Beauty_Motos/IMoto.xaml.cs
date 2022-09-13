using System;
using System.Collections;
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
        }

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {
            MotoDB.CarregarDadosNoDataGrid(dataGrid);
        }
        private void dataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var linha in MotoDB.listaMoto)
            {
                if (dataGrid.SelectedItem == linha)
                {
                    txtId.Text = linha.Id;
                    txtNomeMoto.Text = linha.NomeMoto;
                    txtCat.Text = linha.Cat;
                    txtPreco.Text = linha.Preco;
                    txtDataFabricacao.Text = linha.DataFabricacao;
                    break;
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
            bool existe = false;

            var consulta = MotoDB.listaMoto.Where(moto => moto.Id.Equals(txtId.Text)).ToList();
            if (consulta.Count == 1)
                existe = true;

            return existe;    
        }

        private void btnSalvar_Click(object sender, RoutedEventArgs e)
        {
            Moto moto = new Moto(txtId.Text, txtNomeMoto.Text, txtCat.Text, txtPreco.Text, txtDataFabricacao.Text);

            if (Valida_FrmMoto.ValidaCamposDoFormMotos(moto))
            {
                if (VerificaSeExiteIdMoto() == true)
                    MessageBox.Show("Id da moto ja existente na base de dados", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);

                else
                {
                    MotoDB.AddMotoNoSQL(moto);
                    MotoDB.CarregarDadosNoDataGrid(dataGrid);
                    MessageBox.Show("Cadastro concluido com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCamposDoForm();
                }
            }
        }

        private void btnAlterar_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text != "")
            {
                if (VerificaSeExiteIdMoto() == true)
                {
                    Moto moto = new Moto(txtId.Text, txtNomeMoto.Text, txtCat.Text, txtPreco.Text, txtDataFabricacao.Text);
                    MotoDB.AlterarDadosDoSQL(moto);
                    MotoDB.CarregarDadosNoDataGrid(dataGrid);
                    MessageBox.Show("Dados alterados com sucesso. ", "Mensagem de sucesso ", MessageBoxButton.OK, MessageBoxImage.Information);
                    LimparCamposDoForm();
                }
            }
            else
                MessageBox.Show("Id da moto inexistente na base de dados.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            
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
                    MessageBox.Show("Digite ou selecione o Id que você deseja deletar.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    Moto moto = new Moto(txtId.Text, txtNomeMoto.Text, txtCat.Text, txtPreco.Text, txtDataFabricacao.Text);
                    MotoDB.DeletarMotoDoSQL(moto);
                    MessageBox.Show("Moto excluida com sucesso.", "Mensagem de Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
                    MotoDB.CarregarDadosNoDataGrid(dataGrid);
                    LimparCamposDoForm();
                }
            }
            else
                MessageBox.Show("Informe o Id da moto.", "Mensagem de Erro", MessageBoxButton.OK, MessageBoxImage.Error);
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
            txtDataFabricacao.Text = Mascara_Texbox.MascarDataFabricacao(txtDataFabricacao.Text);
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

        private void txtPesquisa_TextChanged(object sender, TextChangedEventArgs e)
        {
            string qntdCaracter = txtPesquisa.Text;
            if(qntdCaracter.Length > 0)
            {
                var consulta = MotoDB.listaMoto.Where(moto => moto.Id.Contains(txtPesquisa.Text));
                dataGrid.ItemsSource = consulta;
            }
        }
    }
}

