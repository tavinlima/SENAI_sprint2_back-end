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
    // ex: http://localhost:5000/api/veiculos
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private IVeiculoRepository _veiculoRepository { get; set; }
        public VeiculosController()
        {
            _veiculoRepository = new VeiculoRepository();
        }

        [HttpPost]
        public IActionResult Cadastrar(VeiculoDomain novoVeiculo)
        {
            try
            {
                _veiculoRepository.Cadastrar(novoVeiculo);

                return StatusCode(201);
            }
            catch (Exception erro)
            {

                return BadRequest(erro);
            }
            
        }

        [HttpGet]
        public IActionResult Listar()
        {
            try
            {
                List<VeiculoDomain> listaVeiculos = _veiculoRepository.ListarTodos();

                return Ok(listaVeiculos);
            }
            catch (Exception)
            {

                return BadRequest(erro);
            }
            
        }

        [HttpGet("{id}")]
        public IActionResult BuscarVeiculo(int id)
        {
            VeiculoDomain veiculoBuscado = _veiculoRepository.BuscarPorId(id);

            if (veiculoBuscado != null)
            {
                return Ok(veiculoBuscado);
            }
            return NotFound($"Nenhum veículo com o id {id} foi encontrado");
        }

        [HttpPut]
        public IActionResult AtualizarVeiculo(VeiculoDomain veiculoAtualizado)
        {
            if (veiculoAtualizado.idVeiculo <= 0 || veiculoAtualizado.idEmpresa <= 0 || veiculoAtualizado.idModelo <= 0 || veiculoAtualizado.Placa == null)
            {
                return BadRequest(
                    new
                    {
                        mensagem = "Dados não informados"
                    }
                    ); ;
            }
                VeiculoDomain veiculoBuscado = _veiculoRepository.BuscarPorId(veiculoAtualizado.idVeiculo);

                if(veiculoBuscado != null)
                {
                    try
                    {
                        _veiculoRepository.AtualizarVeiculo(veiculoAtualizado);

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
                        mensagem = "Veículo não encontrado",
                        errorStatus = true
                    }
                    );
        }

        [HttpDelete("excluir/{id}")]
        public IActionResult DeletarVeiculo(int id)
        {
            VeiculoDomain veiculoBuscado = _veiculoRepository.BuscarPorId(id);

            if (veiculoBuscado != null)
            {
                try
                {
                    _veiculoRepository.Deletar(id);
                }
                catch (Exception codErro)
                {
                    return BadRequest(codErro);
                }
                return NoContent();
            }
            return NotFound($"Nenhum veiculo com o id {id} foi encontrado para ser deletado");
        }
    }
}
