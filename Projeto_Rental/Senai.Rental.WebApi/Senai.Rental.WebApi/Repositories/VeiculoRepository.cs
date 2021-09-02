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
        public void Atualizar(VeiculoDomain veiculoAtualizado)
        {
            throw new NotImplementedException();
        }

        public VeiculoDomain BuscarPorId(int IdVeiculo)
        {
            throw new NotImplementedException();
        }

        public void Cadastrar(VeiculoDomain novoVeiculo)
        {
            throw new NotImplementedException();
        }

        public void Deletar(int IdVeiculo)
        {
            throw new NotImplementedException();
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

// new EmpresaDomain()
//{
  //  nomeEmpresa = (rdr[1]).ToString(),
 //                           },