using Senai.Rental.WebApi.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório AluguelRepository
    /// </summary>
    interface IAluguelRepository
    {
        void Cadastrar(AluguelDomain novoAluguel);
        List<AluguelDomain> ListarTodos();
        void Atualizar(AluguelDomain aluguelAtualizado);
        AluguelDomain BuscaPorId(int idAluguel);
        void Deletar(int idAluguel);
    }
}
