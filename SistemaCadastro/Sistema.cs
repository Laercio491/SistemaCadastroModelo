using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace SistemaCadastro
{
    public partial class Sistema : Form
    {
        int idAlterarC;
        int idAlterarP;
        public Sistema()
        {
            InitializeComponent();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCadastra_Click(object sender, EventArgs e)
        {
            marcador.Height = btnCliente.Height;
            marcador.Top = btnCliente.Top;
            tabControl1.SelectedTab = tabControl1.TabPages[0];
            tabControl1.BringToFront();
        }


        private void btnBusca_Click(object sender, EventArgs e)
        {
            marcador.Height = btnProduto.Height;
            marcador.Top = btnProduto.Top;
            tabControl2.SelectedTab = tabControl2.TabPages[0];
            tabControl2.BringToFront();
        }







        private void Sistema_Load(object sender, EventArgs e)
        {
            listarClienteNoGrid();
            listarProdutoNoGrid();
        }

        void listarClienteNoGrid()
        {
            conectabanco banco = new conectabanco();
            DataTable dados = new DataTable();
            dados = banco.obterCliente();
            dgCliente.DataSource = dados;
            dgCliente.Columns[0].Visible = false;
            if (dados == null)
            {
                MessageBox.Show(banco.mensagem);
            }
            else
                dgCliente.DataSource = dados;
        }
        void listarProdutoNoGrid()
        {
            conectabanco banco = new conectabanco();
            DataTable dados = new DataTable();
            dados = banco.obterProduto();
            if (dados == null)
            {
                MessageBox.Show(banco.mensagem);
            }
            else
                dgProduto.DataSource = dados;
            DataTable clientes = banco.obterCliente();
            cbCliente.DataSource = clientes;
            cbCliente.DisplayMember = "nome";       // o que aparece para o usuário
            cbCliente.ValueMember = "cod_cliente";  // id que será usado para salvar produto
            cbCliente.SelectedIndex = -1;

            cbCliente2.DataSource = clientes.Copy();
            cbCliente2.DisplayMember = "nome";
            cbCliente2.ValueMember = "cod_cliente";
            cbCliente2.SelectedIndex = -1;

        }


        private void txtBusca_TextChanged(object sender, EventArgs e)
        {
            (dgCliente.DataSource as DataTable).DefaultView.RowFilter = string.Format("nome like'{0}%'", txtBusca.Text);
        }



        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int linha = dgCliente.CurrentRow.Index;
            idAlterarC = Convert.ToInt32(
                dgCliente.Rows[linha].Cells["cod_cliente"].Value.ToString());
            txtNomeAltC.Text= dgCliente.Rows[linha].Cells["nome"].Value.ToString();
            txtEmailAlt.Text = dgCliente.Rows[linha].Cells["email"].Value.ToString();
            txtSenhaAlt.Text = dgCliente.Rows[linha].Cells["senha"].Value.ToString();
            tabControl1.SelectedTab = tabControl1.TabPages[2];
        }

        private void btnConfirmaAlteracao_Click(object sender, EventArgs e)
        {
            listarClienteNoGrid();
            cliente novaCliente = new cliente();
            novaCliente.Nome = txtNomeAltC.Text;
            novaCliente.Email = txtEmailAlt.Text;
            novaCliente.Senha = txtSenhaAlt.Text;
            conectabanco banco = new conectabanco();
            bool retorno = banco.alteraCliente(novaCliente, idAlterarC);
            if (retorno == false)
            {
                MessageBox.Show(banco.mensagem);
            }
            else
            {
                MessageBox.Show("Alteração bem sucedida");
            }
            listarClienteNoGrid();
        }

        
        void LimpaCampos()
        {
            txtNome.Clear();
            txtNomeAltP.Clear();
            txtEmail.Clear();
            txtSenha.Clear();
            txtTamanhoAlt.Clear();
            txtValorAlt.Clear();
            cbCliente.Text = "";
            txtNome.Focus();
            txtNomeAltP.Focus();
        }

        private void BtnConfirmaCadastroCli_Click(object sender, EventArgs e)
        {
            listarClienteNoGrid();
            cliente novaCliente = new cliente();
            novaCliente.Nome = txtNome.Text;
            novaCliente.Email = txtEmail.Text;
            novaCliente.Senha = txtSenha.Text;
            conectabanco banco = new conectabanco();
            bool retorno = banco.insereCliente(novaCliente);
            if (retorno == false)
            {
                MessageBox.Show(banco.mensagem);
            }
            else
            {
                MessageBox.Show("Cadastro bem sucedido");
            }
            LimpaCampos();
            listarClienteNoGrid();
        }

        private void btn_cadastarPro_Click(object sender, EventArgs e)
        {
            listarProdutoNoGrid();
            produto novaProduto = new produto();
            novaProduto.Nome = txtNomeP.Text;
            novaProduto.Tamanho=  txtTamanho.Text;
            novaProduto.Valor = int.Parse(txtValor.Text);
            novaProduto.IdCliente = (int)cbCliente.SelectedValue;
            conectabanco banco = new conectabanco();
            bool retorno = banco.insereProduto(novaProduto);
            if (retorno == false)
            {
                MessageBox.Show(banco.mensagem);
            }
            else
            {
                MessageBox.Show("Cadastro bem sucedido");
            }
            LimpaCampos();
            listarProdutoNoGrid();
        }

        private void btnAlteracaoPro_Click(object sender, EventArgs e)
        {
            listarProdutoNoGrid();
            produto novaProduto = new produto();
            novaProduto.Nome = txtNomeAltP.Text;
            novaProduto.Tamanho = txtTamanhoAlt.Text;
            novaProduto.Valor = int.Parse(txtValorAlt.Text);
            novaProduto.IdCliente = (int)cbCliente.SelectedValue;
            conectabanco banco = new conectabanco();
            bool retorno = banco.alteraProduto(novaProduto, idAlterarP);
            if (retorno == false)
            {
                MessageBox.Show(banco.mensagem);
            }
            else
            {
                MessageBox.Show("Alteração bem sucedida");
            }
            listarProdutoNoGrid();
        }

        private void alterarPro_Click(object sender, EventArgs e)
        {
            int linha = dgProduto.CurrentRow.Index;
            idAlterarP = Convert.ToInt32(
                dgProduto.Rows[linha].Cells["cod_cliente"].Value.ToString());
            txtNomeAltP.Text = dgProduto.Rows[linha].Cells["nome"].Value.ToString();
            txtTamanhoAlt.Text = dgProduto.Rows[linha].Cells["tamanho"].Value.ToString();
            txtValorAlt.Text = dgProduto.Rows[linha].Cells["valor"].Value.ToString();
            cbCliente2.Text = dgProduto.Rows[linha].Cells["idcliente"].Value.ToString();
            tabControl2.SelectedTab = tabControl2.TabPages[2];
        }

        private void txtBuscaP_TextChanged(object sender, EventArgs e)
        {
            (dgProduto.DataSource as DataTable).DefaultView.RowFilter = string.Format("nome like'{0}%'", txtBuscaP.Text);
        }

        private void btnRemoveCliente_Click(object sender, EventArgs e)
        {
            int linha = dgCliente.CurrentRow.Index;
            int id = Convert.ToInt32(
                dgCliente.Rows[linha].Cells["cod_cliente"].Value.ToString());
            DialogResult resp = MessageBox.Show("Tem certeza que deseja Excluir ?", "Remove Cliente", MessageBoxButtons.OKCancel);
            if (resp == DialogResult.OK)
            {
                conectabanco con = new conectabanco();
                bool retorno = con.deletaCliente(id);
                if (retorno == true)
                {
                    MessageBox.Show("Cliente excluido com sucesso!");
                    listarClienteNoGrid();
                }
                else
                {
                    MessageBox.Show(con.mensagem);
                }
            }

            else
            {
                MessageBox.Show("Exclusão cancelada");
            }

        }

        private void removerPro_Click(object sender, EventArgs e)

        {
            int linha = dgProduto.CurrentRow.Index;
            int id = Convert.ToInt32(
                dgProduto.Rows[linha].Cells["cod_produto"].Value.ToString());
            DialogResult resp = MessageBox.Show("Tem certeza que deseja Excluir ?", "Remove Produto", MessageBoxButtons.OKCancel);
            if (resp == DialogResult.OK)
            {
                conectabanco con = new conectabanco();
                bool retorno = con.deletaProduto(id);
                if (retorno == true)
                {
                    MessageBox.Show("Produto excluido com sucesso!");
                    listarProdutoNoGrid();
                }
                else
                {
                    MessageBox.Show(con.mensagem);
                }
            }

            else
            {
                MessageBox.Show("Exclusão cancelada");
            }

        }

    }
}
