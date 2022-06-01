using AluraFlix.Data;
using AluraFlix.Data.Dtos.CategoriaDto;
using AluraFlix.Models;
using AutoMapper;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AluraFlix.Services
{
    public class CategoriaService
    {
        //Adicionando para manipular banco de dados
        private AppDbContext _context;

        //Adicionando para manipular models
        private IMapper _mapper;

        public CategoriaService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Adicionando categoria ao banco de dados
        public ReadCategoriaDto AdicionaCategoria(CreateCategoriaDto createDto)
        {
             
            Categoria categoria = _mapper.Map<Categoria>(createDto);
            //adicionando e salvando no banco de dados        
           
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return _mapper.Map<ReadCategoriaDto>(categoria);

        }


        //retorna todos os videos do banco de dados
        public List<ReadCategoriaDto> GetCategorias()
        {
            //Convertendo context para lista 
            List<Categoria> categorias = _context.Categorias.ToList();
            if (categorias == null) return null;

            return _mapper.Map<List<ReadCategoriaDto>>(categorias);
        }

        //Retorn video por id
        public ReadCategoriaDto GetCategoriaPorId(int id)
        {
            Categoria categoria = RecuperaId(id);
            if (categoria != null)
            {
                //mapeando para retornar video por id 
                ReadCategoriaDto readDto = _mapper.Map<ReadCategoriaDto>(categoria);

                return readDto;
            }
            return null;
        }


        //Atualiza video por id 
        public Result AtualizaCategoria(int id, UpdateCategoriaDto updateDto)
        {
            Categoria categoria = RecuperaId(id);
            if (categoria == null)
            {
                //retorna um erro caso video for nulo 
                return Result.Fail("Endereço não encontrado");
            }
            //mapeando para atualiza-lo
            _mapper.Map(updateDto, categoria);
            //salvando no banco de dados
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletaCategoria(int id)
        {
            Categoria categoria = RecuperaId(id);
            if (categoria == null)
            {
                //retorna um erro caso video for nulo 
                return Result.Fail("Endereco não encontrado");
            }
            //remove video do banco de dados
            _context.Remove(categoria);
            //salvando no banco de dados
            _context.SaveChanges();
            return Result.Ok();
        }
        //Recupera id para usa-lo nas requisições
        private Categoria RecuperaId(int id)
        {
            return _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);

        }
        
    }
}
