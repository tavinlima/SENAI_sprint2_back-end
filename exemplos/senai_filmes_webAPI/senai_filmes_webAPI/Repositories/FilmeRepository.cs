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
    /// Classe responsável pelo repositório dos filmes
    /// </summary>
    public class FilmeRepository : IFilmeRepository
    {
        private string stringConexao = @"DATA SOURCE=DESKTOP-8FOKHBA\SQLEXPRESS; initial catalog=CATALOGO; user Id=sa; pwd=senai@132";
        public void AtualizarIdCorpo(FilmeDomain filmeAtualizado)
        {
            throw new NotImplementedException();
        }

        public FilmeDomain BuscaPorId(int idFilme)
        {
            throw new NotImplementedException();
        }
        
        public void Cadastrar(FilmeDomain novoFilme)
        {
             
            
             using (SqlConnection con = new SqlConnection(stringConexao))
             {
                string queryInsert;

                if (novoFilme.idGenero > 0)
                 {
                     queryInsert = $"INSERT INTO FILME (idGenero, tituloFilme) VALUES ({novoFilme.idGenero}, '{novoFilme.tituloFilme}')";
                }
                 else
                 {
                    queryInsert = "INSERT INTO FILME(tituloFilme) VALUES('" + novoFilme.tituloFilme +"')";
                 }


                 con.Open();

                 using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                 {
                     cmd.ExecuteNonQuery();
                 }
             }
        }


        public void Deletar(int idFilme)
        {
            throw new NotImplementedException();
        }

        public List<FilmeDomain> ListarTodos()
        {
            List<FilmeDomain> listaFilmes = new List<FilmeDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = "SELECT idFilme, tituloFilme, ISNULL(Filme.idGenero,0) idGenero, ISNULL(nomeGenero,'NÃO CADASTRADO') Gênero FROM FILME INNER JOIN GENERO ON Filme.idGenero = Genero.idGenero";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        FilmeDomain filme = new FilmeDomain()
                        {
                            idFilme = Convert.ToInt32(rdr[0]),
                            tituloFilme = rdr[1].ToString(),
                            idGenero = Convert.ToInt32(rdr[2]),
                        };

                        listaFilmes.Add(filme);
                    }
                }
            }

            return listaFilmes;
        }
    }
}
