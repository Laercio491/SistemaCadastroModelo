using MySql.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Rebar;

namespace SistemaCadastro
{
    internal class conectabanco
    {
        MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=1234;database=vista_chic;port=3307");
        public string mensagem;



        public bool insereCliente(cliente novaCliente)
        {
            string sen = Biblioteca.senhaHash(novaCliente.Senha.ToString());
            try
            {
                conexao.Open();
                MySqlCommand cmd =
                    new MySqlCommand("sp_insereCliente", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", novaCliente.Nome);
                cmd.Parameters.AddWithValue("email", novaCliente.Email);
                cmd.Parameters.AddWithValue("senha", sen);
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }

        }// fim do insereCliente

        public bool insereProduto(produto novaProduto)
        {
            try
            {
                conexao.Open();
                MySqlCommand cmd =
                    new MySqlCommand("sp_insereProduto", conexao);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("nome", novaProduto.Nome);
                cmd.Parameters.AddWithValue("tamanho", novaProduto.Tamanho);
                cmd.Parameters.AddWithValue("valor", novaProduto.Valor);
                cmd.Parameters.AddWithValue("idcliente", novaProduto.IdCliente);
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }

        }// fim do insereProduto

        public DataTable obterCliente()
        {
            MySqlCommand comando = new MySqlCommand("sp_listaCliente", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabelaDadosCliente = new DataTable();
                adapter.Fill(tabelaDadosCliente);
                return tabelaDadosCliente;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }
        }


        public DataTable obterProduto()
        {
            MySqlCommand comando = new MySqlCommand("sp_listaProduto", conexao);
            comando.CommandType = CommandType.StoredProcedure;
            try
            {
                conexao.Open();
                MySqlDataAdapter adapter = new MySqlDataAdapter(comando);
                DataTable tabelaDadosPro = new DataTable();
                adapter.Fill(tabelaDadosPro);
                return tabelaDadosPro;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return null;
            }
            finally
            {
                conexao.Close();
            }
        }
        public bool deletaCliente(int idcli)
        {
            MySqlCommand cmd = new MySqlCommand("sp_removeCliente", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idcliente", idcli);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro: " + e.Message;
                return false;
            }
        }
        public bool deletaProduto(int idpro)
        {
            MySqlCommand cmd = new MySqlCommand("sp_removeProduto", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("idproduto", idpro);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (MySqlException e)
            {
                mensagem = "Erro: " + e.Message;
                return false;
            }
        }
        public bool alteraCliente(cliente c, int idcliente)
        {
            MySqlCommand cmd = new MySqlCommand("sp_alteraCliente", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cod_cliente", idcliente);
            cmd.Parameters.AddWithValue("nome", c.Nome);
            cmd.Parameters.AddWithValue("email", c.Email);
            cmd.Parameters.AddWithValue("senha", c.Senha);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }
            finally { conexao.Close(); }

        }
        public bool alteraProduto(produto p, int idproduto)
        {
            MySqlCommand cmd = new MySqlCommand("sp_alteraProduto", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("cod_produto", idproduto);
            cmd.Parameters.AddWithValue("nome", p.Nome);
            cmd.Parameters.AddWithValue("tamanho", p.Tamanho);
            cmd.Parameters.AddWithValue("valor", p.Valor);
            cmd.Parameters.AddWithValue("idcliente", p.IdCliente);
            try
            {
                conexao.Open();
                cmd.ExecuteNonQuery();//executar no banco
                return true;
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }
            finally { conexao.Close(); }
        }

        public bool verifica(string user, string pass)
        {
            string sen = Biblioteca.senhaHash(pass);
            MySqlCommand cmd = new MySqlCommand("sp_consultaLogin", conexao);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("usuario", user);
            cmd.Parameters.AddWithValue("senha", sen);
            try
            {
                conexao.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (MySqlException erro)
            {
                mensagem = erro.Message;
                return false;
            }
            finally { conexao.Close(); }
        }
    }
}


