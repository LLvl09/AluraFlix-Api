using AluraFlix.Data.Dtos.CategoriaDto;
using AluraFlix.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AluraFlix.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private CategoriaService _categoriaService;

        public CategoriaController(CategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        //Adiciona Video ao banco de dados
        public IActionResult AdicionaCategoria([FromBody] CreateCategoriaDto createDto)
        {
            //criando adiciona categorias  no categoriaService
            ReadCategoriaDto readDto = _categoriaService.AdicionaCategoria(createDto);
            //Ele retorna o id quando voce cria 
            return CreatedAtAction(nameof(GetCategoriaPorId), new { Id = readDto.Id }, readDto);

        }


        [Authorize(Roles = "admin")]
        [HttpGet]
        //Retorna todos os categorias criadas
        public IActionResult GetCategorias()
        {
            //criando GetCategorias
            List<ReadCategoriaDto> readDto = _categoriaService.GetCategorias();
            if (readDto == null) return NotFound();
            return Ok(readDto);

        }


        [Authorize(Roles = "admin, regular")]
        [HttpGet("{id}")]
        //Pega categoria por id
        public IActionResult GetCategoriaPorId(int id)
        {
            //Criando GetCategoriaPorId
            ReadCategoriaDto readDto = _categoriaService.GetCategoriaPorId(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }


        [Authorize(Roles = "admin")]
        [HttpPut("{id}")]
        //Atualiza categoria por id 
        public IActionResult AtualizaCategoria(int id, UpdateCategoriaDto updateDto)
        {
            //criando AtualizaCategoria
            Result resultado = _categoriaService.AtualizaCategoria(id, updateDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }
        [Authorize(Roles = "admin")]
        [HttpDelete("{id}")]
        //Deleta categoira por id 
        public IActionResult DeletaVideo(int id)
        {
            //Criando DeletaCategoria
            Result resultado = _categoriaService.DeletaCategoria(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }
    }
}
