using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SistemaCadastro
{
    public class cliente
    {
        public string Nome;
        public string Email;
        public string Senha;
    }
    public class produto
    {
        public string Nome;
        public string Tamanho;
        public int Valor;
        public int IdCliente;
    }
}
