using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Senai.Rental.WebApi.Domains;
using Senai.Rental.WebApi.Interfaces;
using Senai.Rental.WebApi.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Senai.Rental.WebApi.Controllers
{
    [Produces("application/json")]

    //Define que a rota de uma requisição será no formato domino/api/nomeController.
    // ex: http://localhost:5000/api/alugueis
    [Route("api/[controller]")]
    [ApiController]
    public class AlugueisController : ControllerBase
    {
        private IAluguelRepository _aluguelRepository { get; set; }
        public AlugueisController()
        {
            _aluguelRepository = new AluguelRepository();
        }

        [HttpPost]
        public IActionResult CadastrarAluguel(AluguelDomain novoAluguel)
        {
            try
            {
                _aluguelRepository.Cadastrar(novoAluguel);
                return StatusCode(201);
            }
            catch (Exception codErro)
            {
                return BadRequest(codErro);
            }
                
            
        }

        [HttpGet]
        public IActionResult ListarAluguel()
        {
            try
            {
                List<AluguelDomain> listaAlugueis = _aluguelRepository.ListarTodos();

                return Ok(listaAlugueis);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
           
        }

        [HttpGet("{id}")]
        public IActionResult BuscarAluguel(int id)
        {
            AluguelDomain aluguelBuscado = _aluguelRepository.BuscaPorId(id);

            if (aluguelBuscado == null)
            {
                return NotFound($"Nenhum Aluguel com o id {id} foi encontrado");
            }
            return Ok(aluguelBuscado);
        }

        [HttpPut]
        public IActionResult AtualizarAluguel(AluguelDomain aluguelAtualizado)
        {
            if (aluguelAtualizado.idCliente <= 0 || aluguelAtualizado.idVeiculo <= 0 || aluguelAtualizado.idAluguel <= 0)
            {
                return BadRequest(
                    new
                    {
                        mensagem = "Dados não informados",
                    }
                    );
            }

            AluguelDomain aluguelBuscado = _aluguelRepository.BuscaPorId(aluguelAtualizado.idAluguel);

            if (aluguelBuscado != null)
            {
                try
                {
                    _aluguelRepository.Atualizar(aluguelAtualizado);

                    return NoContent();
                }
                catch (Exception erro)
                {
                    return BadRequest(erro);
                }
            }

            return NotFound(
                new
                {
                    mensagem = "Aluguel não encontrado",
                    errorStatus = true
                }
                );
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult DeletarAluguel(int id)
        {
            AluguelDomain aluguelBuscado = _aluguelRepository.BuscaPorId(id);

            if (aluguelBuscado != null)
            {
                try
                {
                    _aluguelRepository.Deletar(id);

                }
                catch (Exception codErro)
                {
                    return BadRequest(codErro);
                }
                return NoContent();
            }

            return NotFound($"Nenhum aluguel de id {id} foi encontrado para ser deletado");
        }
    }
}
