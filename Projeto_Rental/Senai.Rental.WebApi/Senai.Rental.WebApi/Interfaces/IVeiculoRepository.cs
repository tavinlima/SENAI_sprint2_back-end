using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório VeiculoRepository
    /// </summary>
    interface IVeiculoRepository
    {
        /// <summary>
        /// Cadastra um novo veículo
        /// </summary>
        /// <param name="novoVeiculo">Objeto novoVeiculo com os novos dados</param>
        void Cadastrar(VeiculoDomain novoVeiculo);

        /// <summary>
        /// Lista todos os veiculos existentes
        /// </summary>
        /// <returns>Uma lista de todos os veiculos</returns>
        List<VeiculoDomain> ListarTodos();

        /// <summary>
        /// Atualiza um veiculo existente
        /// </summary>
        /// <param name="veiculoAtualizado">Objeto veiculoAtualizado com os novos dados atualizados</param>
        void Atualizar(VeiculoDomain veiculoAtualizado);

        /// <summary>
        /// Busca um veiculo por meio do seu ID
        /// </summary>
        /// <param name="IdVeiculo">Id do veiculo que será buscado</param>
        /// <returns>Um veiculo buscado</returns>
        VeiculoDomain BuscarPorId(int IdVeiculo);

        /// <summary>
        /// Deleta um veiculo existente
        /// </summary>
        /// <param name="IdVeiculo">Id do cliente que será deletado</param>
        void Deletar(int IdVeiculo);
    }
}
