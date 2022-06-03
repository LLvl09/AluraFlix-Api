using AluraFlix.Data;
using AluraFlix.Data.Dtos.VideoDto;
using AluraFlix.Models;
using AluraFlix.Profiles;
using AluraFlix.Services;
using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AluraFlix.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class VideoController : ControllerBase
    {
        private AppDbContext _context;
        private VideoService _videoService;

        public VideoController(VideoService videoService, AppDbContext context)
        {
            _videoService = videoService;
            _context = context;
        }


        [HttpPost]
        //Adiciona Video ao banco de dados
        public IActionResult AdicionaVideo([FromBody] CreateVideoDto createDto)
        {
            //criando adicionaFilmes no videoService
            ReadVideoDto readDto = _videoService.AdicionaVideo(createDto);
            //Ele retorna o id quando voce cria ele
            if (readDto != null)
            {
                return CreatedAtAction(nameof(GetVideoPorId), new { Id = readDto.Id }, readDto);

            }
            return NotFound("Url incessivel");
        }



        [HttpGet("take/{take:int}/page/{page:int}")]
        //Retorna todos os videos criados
        public async Task<IActionResult> GetVideos([FromQuery] string categoria, [FromRoute] int page = 0, [FromRoute] int take = 5)
        {
            var total = await _context.Videos.CountAsync();
            if (take > 20)
            {
                return BadRequest("nao pode pegar mais de 20 videos por pagina");
            }
            if (page == null && take == null)
            {
                return NotFound("Tem que informar o take e a page");

            }

            List<Video> todos = await GetVideoPage(page, take);
            return VerificaQuery(categoria, todos, total);
        }

        //Criando pagina free para usuarios   
        [HttpGet("/Video/Free")]
        public async Task<IActionResult> GetVideosFree()
        {
            List<Video> todo = await PageGetVideosFree();

            return Ok(todo);
        }


        [HttpGet("{id}")]
        //Pega video por id
        public IActionResult GetVideoPorId(int id)
        {
            //Criando GetVideoPorId
            ReadVideoDto readDto = _videoService.GetVideoPorId(id);
            if (readDto == null) return NotFound();
            return Ok(readDto);
        }


        [HttpPut("{id}")]
        //Atualiza video por id 
        public IActionResult AtualizaVideo(int id, UpdateVideoDto updateDto)
        {
            //criando AtualizaVideo
            Result resultado = _videoService.AtualizaVideo(id, updateDto);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }


        [HttpDelete("{id}")]
        //Deleta video por id 
        public IActionResult DeletaVideo(int id)
        {
            //Criando DeletaVideo
            Result resultado = _videoService.DeletaVideo(id);
            if (resultado.IsFailed) return NotFound();
            return NoContent();
        }
        //Verifica se o valor da query é 


        //Cria um sistema de paginacao
        private async Task<List<Video>> GetVideoPage(int page, int take)
        {
            return await _context
                                            .Videos
                                            .AsNoTracking()
                                            .Include(c => c.Categoria)
                                            .Skip(page)
                                            .Take(take)
                                            .ToListAsync();
        }
        //sistema de paginacao de graca
        private async Task<List<Video>> PageGetVideosFree()
        {
            return await _context
                                .Videos
                                .AsNoTracking()
                                .Include(c => c.Categoria)
                                .Skip(0)
                                .Take(5)
                                .ToListAsync();
        }
        //Verifica query
        private IActionResult VerificaQuery(string categoria, List<Video> todos, int total)
        {
            if (categoria != null && _context.Categorias.All(c => c.Titulo != categoria))
            {
                return NotFound("categoria não encotrada");
            };

            return Ok(new
            {
                data = total,
                todos
            });
        }
    }
}
