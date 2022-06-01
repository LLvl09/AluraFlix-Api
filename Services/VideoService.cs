using AluraFlix.Data;
using AluraFlix.Data.Dtos.VideoDto;
using AluraFlix.Models;
using AutoMapper;
using FluentResults;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AluraFlix.Services
{
    public class VideoService
    {
        //Adicionando para manipular banco de dados
        private AppDbContext _context;

        //Adicionando para manipular models
        private IMapper _mapper;

        public VideoService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Adicionando video ao banco de dados
        public ReadVideoDto AdicionaVideo(CreateVideoDto createDto)
        {
            //convertendo o create dto para 
            Video video = _mapper.Map<Video>(createDto);
            //verificando se  a url é compativel
            return UrlValidate(video);

        }




        //Retorn video por id
        public ReadVideoDto GetVideoPorId(int id)
        {
            Video video = RecuperaId(id);
            if (video != null)
            {
                //mapeando para retornar video por id 
                ReadVideoDto readDto = _mapper.Map<ReadVideoDto>(video);

                return readDto;
            }
            return null;
        }


        //Atualiza video por id 
        public Result AtualizaVideo(int id, UpdateVideoDto updateDto)
        {
            Video video = RecuperaId(id);
            if (video == null)
            {
                //retorna um erro caso video for nulo 
                return Result.Fail("Endereço não encontrado");
            }
            //mapeando para atualiza-lo
            _mapper.Map(updateDto, video);
            //salvando no banco de dados
            _context.SaveChanges();
            return Result.Ok();
        }




        public Result DeletaVideo(int id)
        {
            Video video = RecuperaId(id);
            if (video == null)
            {
                //retorna um erro caso video for nulo 
                return Result.Fail("Endereco não encontrado");
            }
            //remove video do banco de dados
            _context.Remove(video);
            //salvando no banco de dados
            _context.SaveChanges();
            return Result.Ok();
        }



        //Recupera id para usa-lo nas requisições
        private Video RecuperaId(int id)
        {
            return _context.Videos.FirstOrDefault(video => video.Id == id);

        }

        private ReadVideoDto UrlValidate(Video video)
        {
            var url = video.Url;
            //verificando se a url é valida
            var youtubeMatch =
                new Regex(@"youtu(?:\.be|be\.com)/(?:.*v(?:/|=)|(?:.*/)?)([a-zA-Z0-9-_]+)")
                .Match(url);
            //se a url for valida ele continua a aplicação  
            if (youtubeMatch.Success)
            {
                _context.Videos.Add(video);
                _context.SaveChanges();
                return _mapper.Map<ReadVideoDto>(video);
            }
            return null;
        }

    }
}
