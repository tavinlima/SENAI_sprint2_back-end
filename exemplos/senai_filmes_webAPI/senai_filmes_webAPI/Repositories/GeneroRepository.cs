using senai_filmes_webAPI.Domains;
using senai_filmes_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webAPI.Repositories
{
    /// <summary>
    /// Classe responsável pelo repositório dos gêneros
    /// </summary>
    public class GeneroRepository : IGeneroRepository
    {
        /// <summary>
        /// String de conexão com o banco de dados que recebe os parâmetros.
        /// Data Source = Nome do Servidor
        /// initial catalog = Nome do Banco de Dados 
        /// user Id; pwd = Faz autenticação com o SQL SERVER passando o Login e a Senha.
        /// integrated security = true
        /// </summary>
        private string stringConexao = @"DATA SOURCE=DESKTOP-8FOKHBA\SQLEXPRESS; initial catalog=CATALOGO; user Id=sa; pwd=senai@132";

        public void AtualizarIdCorpo(GeneroDomain generoAtualizado)
        {
            throw new NotImplementedException();
        }

        public void AtualizarIdUrl(int idGenero, GeneroDomain generoAtualizado)
        {
            throw new NotImplementedException();
        }

        public GeneroDomain BuscaPorId(int idGenero)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(GeneroDomain novoGenero)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO GENERO (nomeGenero) VALUES ('" + novoGenero.nomeGenero + "')";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idGenero)
        {
            throw new NotImplementedException();
        }

        
        public List<GeneroDomain> ListarTodos()
        {
            List<GeneroDomain> listaGeneros = new List<GeneroDomain>();

            // Declara a SqlConnection con passando a string de conexão com o parâmetro
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idGenero, nomeGenero FROM GENERO";

                // Abre a conexão com o Banco de Dados.
                con.Open();

                // Declarando SqlDataReader rdr que irá percorrer a tabela do banco de dados
                SqlDataReader rdr;

                // Declara o SqlCommand passando da query que será executada e a conexão com parâmetros
                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    // Executa a query e armazena os dados no rdr.
                    rdr = cmd.ExecuteReader();

                    // Enquanto houver registros para serem lidos no rdr, o laço se repete
                    while (rdr.Read())
                    {
                        GeneroDomain genero = new GeneroDomain()
                        {
                            idGenero = Convert.ToInt32(rdr[0]),
                            nomeGenero = rdr[1].ToString(),
                        };

                        listaGeneros.Add(genero);
                    }
                }
            }

            return listaGeneros;

        }
    }
}
