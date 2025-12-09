# Sistema de Cadastro de Bandas

Sistema desktop desenvolvido em C# Windows Forms para gerenciamento e cadastro de bandas musicais.

## 📋 Descrição

Este projeto é um sistema de cadastro que permite gerenciar informações sobre bandas musicais, incluindo nome, número de integrantes, ranking e gênero musical. O sistema utiliza MySQL como banco de dados e oferece uma interface gráfica intuitiva para cadastro e busca de informações.

## 🚀 Tecnologias Utilizadas

- **C# (.NET Framework)** - Linguagem de programação
- **Windows Forms** - Framework para interface gráfica
- **MySQL 8.1.0** - Sistema de gerenciamento de banco de dados
- **ADO.NET** - Acesso a dados
- **Visual Studio** - IDE de desenvolvimento

## 📦 Dependências

O projeto utiliza as seguintes bibliotecas NuGet:

- MySql.Data 8.1.0


## 🗄️ Estrutura do Banco de Dados

O sistema utiliza duas tabelas principais:

### Tabela `cliente`
- `cod_cliente` (INT, AUTO_INCREMENT, PRIMARY KEY)
- `nome` (VARCHAR(45))
- `email` (VARCHAR(45))
- `senha` (VARCHAR(200))

cliente adm pré-cadastrado: adm / aaaa@gmail.com / a665a45920422f9d417e4867efdc4fb8a04a1f3fff1fa07e998e86f7f7a27ae3

### Tabela `produto`
- `cod_produto` (INT, AUTO_INCREMENT, PRIMARY KEY)
- `nome` (VARCHAR(100))
- `tamanho` (INT)
- `valor` (INT)
- `idcliente` (INT, FOREIGN KEY)

### Stored Procedures

- `sp_insereCliente` - Insere um novo cliente no banco
- `sp_insereProduto` - Insere um novo produto no banco
- `sp_listaCliente` - Lista todos os clientes
- `sp_listaProdutos` - Lista todos os produtos que tem algum cliente comprando
- `sp_removeCliente` - Remove o Cliente selecionado
- `sp_removeProduto` - Remove o Produto selecionado
- `sp_alteraCliente` - Altera o Clente selecionado
- `sp_alteraProduto` - Altera o Produto selecionado

## ⚙️ Instalação e Configuração

### Pré-requisitos

- Visual Studio 2019 ou superior
- MySQL Server 8.0 ou superior
- .NET Framework 4.7.2 ou superior

### Passo a Passo

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd "projeto_final"
   ```

2. **Configure o Banco de Dados**
   - Abra o MySQL Workbench ou outro cliente MySQL
   - Execute o script `DumpBancoCadastro.sql` para criar as tabelas e procedures
   ```sql
   source DumpBancoCadastro.sql
   ```

3. **Configure a String de Conexão**
   - Abra o arquivo `conectabanco.cs` no projeto SistemaCadastro
   - Atualize a string de conexão com suas credenciais do MySQL
   ```xml
    <!-- MySqlConnection conexao = new MySqlConnection("server=localhost;user id=root;password=1234;database=vista_chic;port=3307"); -->
   ```

4. **Restaure os Pacotes NuGet**
   - No Visual Studio, clique com o botão direito na solução
   - Selecione "Restore NuGet Packages"

5. **Compile e Execute**
   - Pressione `F5` ou clique em "Start" no Visual Studio

## 🎯 Funcionalidades

- ✅ **Cadastro de Clientes e Produtos** - Adicione novos CLientes e Produtos com informações completas
- 🔍 **Busca de Clientes e Produtos** - Pesquise Clientes ou Produtos cadastradas
- 📝 **Alteração de Dados** - Edite informações de Clientes ou Produtos existentes
- 🗑️ **Remoção de Clientes e Produtos** - Exclua registros do sistema
- 📊 **Visualização em Lista** - Veja todos os Clientes e Produtos cadastrados

## 📁 Estrutura do Projeto

```
SistemaCadastro/
├── Program.cs              # Ponto de entrada da aplicação
├── Sistema.cs              # Lógica principal do formulário
├── Sistema.Designer.cs     # Designer do Windows Forms
├── App.config             # Configurações da aplicação
├── packages.config        # Configuração de pacotes NuGet
└── Properties/            # Propriedades do projeto
    ├── AssemblyInfo.cs
    ├── Resources.resx
    └── Settings.settings
```

## 🎨 Interface

O sistema possui uma interface com navegação por abas:

- **Aba Cliente** - Inserção, Alteração, Busca e Exclusão referente aos Clientes
- **Aba Produto** - Inserção, Alteração, Busca e Exclusão referente aos Produtos

A navegação é facilitada por botões laterais com indicador visual de aba selecionada.


## 📝 Licença

Este projeto é um trabalho acadêmico desenvolvido para fins educacionais.

## 👥 Autor

Desenvolvido como projeto do curso de Linguagem I

---

**Nota**: Este é um projeto modelo para fins educacionais. Certifique-se de implementar as validações e tratamento de erros adequados antes de usar em produção.
