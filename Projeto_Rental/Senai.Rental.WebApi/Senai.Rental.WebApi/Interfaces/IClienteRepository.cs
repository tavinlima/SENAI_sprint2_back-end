using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório ClienteRepository
    /// </summary>
    interface IClienteRepository
    {
        /// <summary>
        /// Cadastra um novo Cliente
        /// </summary>
        /// <param name="novoCliente">Objeto novoCliente com os novos dados</param>
        void Cadastrar(ClienteDomain novoCliente);

        /// <summary>
        /// Lista todos os clientes existentes
        /// </summary>
        /// <returns>Uma lista de todos os clientes</returns>
        List<ClienteDomain> ListarTodos();

        /// <summary>
        /// Atualiza um cliente existente
        /// </summary>
        /// <param name="clienteAtualizado">Objeto clienteAtualizado com os novos dados atualizados</param>
        void AtualizarCliente(ClienteDomain ClienteAtualizado);

        /// <summary>
        /// Busca um cliente por meio do seu ID
        /// </summary>
        /// <param name="idCliente">Id do cliente que será buscado</param>
        /// <returns>Um cliente buscado</returns>
        ClienteDomain BuscarPorId(int idCliente);

        /// <summary>
        /// Deleta um cliente existente
        /// </summary>
        /// <param name="idCliente">Id do cliente que será deletado</param>
        void Deletar(int idCliente);
    }
}
