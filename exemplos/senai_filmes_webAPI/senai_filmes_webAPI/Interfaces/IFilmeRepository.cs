using senai_filmes_webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_filmes_webAPI.Interfaces
{
    /// <summary>
    /// Interface responsável pelo repositório DilmeRepository
    /// </summary>
    interface IFilmeRepository
    {
        List<FilmeDomain> ListarTodos();
        FilmeDomain BuscaPorId(int idFilme);
        void Cadastrar(FilmeDomain novoFilme);
        void AtualizarIdCorpo(FilmeDomain filmeAtualizado);
        void Deletar(int idFilme);
    }
}
