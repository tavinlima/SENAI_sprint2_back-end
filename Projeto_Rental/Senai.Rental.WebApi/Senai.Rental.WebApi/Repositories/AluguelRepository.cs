using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class AluguelRepository : IAluguelRepository
    {
        private string stringConexao = @"Data Source=DESKTOP-8FOKHBA\SQLEXPRESS; initial catalog=T_Rental; user Id=sa; pwd=senai@132";

        public void Atualizar(AluguelDomain aluguelAtualizado)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryUpdate = $@"UPDATE ALUGUEL SET idVeiculo = @idVeiculo, 
                                        idCliente = @idCliente, 
                                        dataEmpresetimo = @dataEmprestimo, 
                                        dataDevolucao = @dataDevolucao 
                                        WHERE idAluguel = @idAluguel";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", aluguelAtualizado.idVeiculo);
                    cmd.Parameters.AddWithValue("@idCliente", aluguelAtualizado.idCliente);
                    cmd.Parameters.AddWithValue("@dataEmprestimo", aluguelAtualizado.dataEmprestimo);
                    cmd.Parameters.AddWithValue("@dataDevolucao ", aluguelAtualizado.dataDevolucao) ;
                    cmd.Parameters.AddWithValue("@idAluguel", aluguelAtualizado.idAluguel);

                    con.Open();

                    cmd.ExecuteNonQuery();
                };
            }
        }

        public AluguelDomain BuscaPorId(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySearch = @"SELECT idAluguel, idVeiculo, dataEmpresetimo, dataDevolucao, nomeCliente, nomeModelo 
                                          FROM ALUGUEL
                                          INNER JOIN CLIENTE
                                          ON ALUGUEL.idCliente = CLIENTE.idCliente
                                          LEFT JOIN MODELO
                                          ON ALUGUEL.idVeiculo = MODELO.idModelo
                                          WHERE idAluguel = @idAluguel";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearch, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        AluguelDomain aluguelBuscado = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(rdr[0]),
                            idVeiculo = Convert.ToInt32(rdr[1]),
                            dataEmprestimo = Convert.ToDateTime(rdr[2]),
                            dataDevolucao = Convert.ToDateTime(rdr[3]),
                            cliente = new ClienteDomain
                            {
                                nomeCliente = (rdr[4]).ToString(),
                            },
                            modelo = new ModeloDomain
                            {
                                nomeModelo = (rdr[5]).ToString()
                            }
                        };

                        return aluguelBuscado;
                    }
                    return null;
                }
                
            }
        }

        public void Cadastrar(AluguelDomain novoAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO ALUGUEL (idVeiculo, idCliente, dataEmpresetimo, dataDevolucao) VALUES (@idVeiculo, @idCliente, @dataEmprestimo, @dataDevolucao)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", novoAluguel.idVeiculo);
                    cmd.Parameters.AddWithValue("@idCliente", novoAluguel.idCliente);
                    cmd.Parameters.AddWithValue("@dataEmprestimo", novoAluguel.dataEmprestimo);
                    cmd.Parameters.AddWithValue("@dataDevolucao", novoAluguel.dataDevolucao);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int idAluguel)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM ALUGUEL WHERE idAluguel = @idAluguel";

                using (SqlCommand cmd = new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idAluguel", idAluguel);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<AluguelDomain> ListarTodos()
        {
            List<AluguelDomain> listaAlugueis = new List<AluguelDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = @"SELECT idAluguel, idVeiculo, dataEmpresetimo, dataDevolucao, nomeCliente, nomeModelo, nomeMarca 
                                          FROM ALUGUEL
                                          INNER JOIN CLIENTE
                                          ON ALUGUEL.idCliente = CLIENTE.idCliente
                                          LEFT JOIN MODELO
                                          ON ALUGUEL.idVeiculo = MODELO.idModelo
                                          LEFT JOIN MARCA
										  ON MODELO.idMarca = MARCA.idMarca";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {

                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        AluguelDomain aluguel = new AluguelDomain()
                        {
                            idAluguel = Convert.ToInt32(rdr[0]),
                            idVeiculo = Convert.ToInt32(rdr[1]),
                            dataEmprestimo = Convert.ToDateTime(rdr[2]),
                            dataDevolucao = Convert.ToDateTime(rdr[3]),
                            cliente = new ClienteDomain
                            {
                                nomeCliente = (rdr[4]).ToString(),
                            },
                            modelo = new ModeloDomain
                            {
                                nomeModelo = (rdr[5]).ToString(),
                                marca = new MarcaDomain
                                {
                                    nomeMarca = (rdr[6]).ToString()
                                }
                            }
                        };
                        listaAlugueis.Add(aluguel);
                    }
                }
            }
            return listaAlugueis;
        }
    }
}
