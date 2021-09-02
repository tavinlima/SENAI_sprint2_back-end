using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Repositories
{
    public class VeiculoRepository : IVeiculoRepository
    {
        private string stringConexao = @"Data Source=DESKTOP-8FOKHBA\SQLEXPRESS; initial catalog=T_Rental; user Id=sa; pwd=senai@132";
        public void AtualizarVeiculo(VeiculoDomain veiculoAtualizado)
        {
            using (SqlConnection con =  new SqlConnection(stringConexao))
            {
                string queryUpdate = "UPDATE VEICULO SET idEmpresa = @idEmpresa, idModelo = @idModelo, Placa = @Placa WHERE idVeiculo = @idVeiculo";

                using (SqlCommand cmd = new SqlCommand(queryUpdate, con))
                {
                    cmd.Parameters.AddWithValue("@idEmpresa", veiculoAtualizado.idEmpresa);
                    cmd.Parameters.AddWithValue("@idModelo", veiculoAtualizado.idModelo);
                    cmd.Parameters.AddWithValue("@Placa", veiculoAtualizado.Placa);
                    cmd.Parameters.AddWithValue("@idVeiculo", veiculoAtualizado.idVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public VeiculoDomain BuscarPorId(int IdVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySearch = $@"SELECT idVeiculo, nomeEmpresa, nomeModelo, nomeMarca, Placa FROM VEICULO V 
                                            INNER JOIN MODELO MO ON V.idModelo = MO.idModelo 
                                            INNER JOIN MARCA MA ON MO.idMarca = MA.idMarca
                                            INNER JOIN EMPRESA E ON V.idEmpresa = E.idEmpresa
                                            WHERE idVeiculo = @idVeiculo";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySearch, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", IdVeiculo);

                    rdr = cmd.ExecuteReader();

                    if (rdr.Read())
                    {
                        VeiculoDomain veiculoBuscado = new VeiculoDomain
                        {
                            idVeiculo = Convert.ToInt32(rdr["idVeiculo"]),
                            empresa = new EmpresaDomain()
                            {
                                nomeEmpresa = (rdr[1]).ToString()
                            },
                            modelo = new ModeloDomain()
                            {
                                nomeModelo = (rdr[2]).ToString(),
                                marca = new MarcaDomain()
                                {
                                    nomeMarca = (rdr[3]).ToString()
                                }
                            },
                            Placa = (rdr[4]).ToString(),
                        };
                        return veiculoBuscado;
                    }
                    return null;
                }
            }
        }

        public void Cadastrar(VeiculoDomain novoVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryInsert = "INSERT INTO VEICULO (idEmpresa, idModelo, Placa) VALUES (@idEmpresa, @idModelo, @Placa)";

                con.Open();

                using (SqlCommand cmd = new SqlCommand(queryInsert, con))
                {
                    cmd.Parameters.AddWithValue("@idEmpresa", novoVeiculo.idEmpresa);
                    cmd.Parameters.AddWithValue("@idModelo", novoVeiculo.idModelo);
                    cmd.Parameters.AddWithValue("@Placa", novoVeiculo.Placa);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Deletar(int IdVeiculo)
        {
            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string queryDelete = "DELETE FROM VEICULO WHERE idVeiculo = @idVeiculo";

                using (SqlCommand cmd =  new SqlCommand(queryDelete, con))
                {
                    cmd.Parameters.AddWithValue("@idVeiculo", IdVeiculo);

                    con.Open();

                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<VeiculoDomain> ListarTodos()
        {
            List<VeiculoDomain> listaVeiculos = new List<VeiculoDomain>();

            using (SqlConnection con = new SqlConnection(stringConexao))
            {
                string querySelectAll = $@"SELECT idVeiculo, nomeEmpresa, nomeModelo, nomeMarca, Placa FROM VEICULO V 
                                            INNER JOIN MODELO MO ON V.idModelo = MO.idModelo 
                                            INNER JOIN MARCA MA ON MO.idMarca = MA.idMarca
                                            INNER JOIN EMPRESA E ON V.idEmpresa = E.idEmpresa";

                con.Open();

                SqlDataReader rdr;

                using (SqlCommand cmd = new SqlCommand(querySelectAll, con))
                {
                    rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        VeiculoDomain veiculo = new VeiculoDomain()
                        {
                            
                            idVeiculo = Convert.ToInt32(rdr[0]),
                            empresa = new EmpresaDomain()
                            {
                                nomeEmpresa = (rdr[1]).ToString()
                            },
                            modelo = new ModeloDomain()
                            {
                                nomeModelo = (rdr[2]).ToString(),
                                marca = new MarcaDomain()
                                {
                                    nomeMarca = (rdr[3]).ToString()
                                }
                            },
                            Placa = (rdr[4]).ToString(),
                        };

                        listaVeiculos.Add(veiculo);
                    }
                }

            }
            return listaVeiculos;
        }
    }
}