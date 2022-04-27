using AutoMapper;
using listaToDoAPI.Data;
using listaToDoAPI.Data.Dtos;
using listaToDoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace listaToDoAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ListaController : ControllerBase
    {

        private TarefaContext _context;
        private IMapper _mapper;
        
        public ListaController(TarefaContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult AdicionaTarefa([FromBody] CreateTarefaDto tarefaDto)
        {
            Tarefa tarefa = _mapper.Map<Tarefa>(tarefaDto);
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            return CreatedAtAction(nameof(RecuperarTarefaPorId), new { Id = tarefa.Id }, tarefa);
        }
        

        [HttpGet]
        public IEnumerable<Tarefa> RecuperarTarefa()
        {
            return _context.Tarefas.Where(c => c.Concluida == false);
        }

        //recupera concluidas
        [HttpGet("recuperarConcluidas")]
        public IEnumerable<Tarefa> RecuperarTarefaConcluida()
        {
            return _context.Tarefas.Where(c =>c.Concluida == true);
        }

        
        [HttpGet("{id}")]
        public IActionResult RecuperarTarefaPorId(int id)
        {
            Tarefa tarefa = _context.Tarefas.FirstOrDefault(tarefa => tarefa.Id == id);
            if(tarefa != null)
            {
                ReadTarefaDto tarefaDto = _mapper.Map<ReadTarefaDto>(tarefa);
                return Ok(tarefaDto);
            }
            return NotFound();
        }

        [HttpPut("{id}")]
        public IActionResult AtualizaTarefa(int id, [FromBody] UpdateTarefaDto tarefaDto)
        {
            Tarefa tarefa = _context.Tarefas.FirstOrDefault(tarefa => tarefa.Id == id);
            if(tarefa == null)
            {
                return NotFound();
            }
            _mapper.Map(tarefaDto, tarefa);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPut("marcarTarefa/{id}")]
        public IActionResult MarcarTarefa(int id)
        {
            var tarefa = _context.Tarefas.Where(c => c.Id == id).FirstOrDefault();
            tarefa.Concluida = false;
            _context.SaveChanges();
            Console.WriteLine("teste1");
            return NoContent();
            
        }

        [HttpPut("desmarcarTarefa/{id}")]
        public IActionResult DesmarcarTarefa(int id)
        {
            var tarefa = _context.Tarefas.Where(c =>c.Id == id).FirstOrDefault();
            tarefa.Concluida = true;
            _context.SaveChanges();            
            return NoContent();
            
        }



        [HttpDelete("{id}")]
        public IActionResult DeletaFilme(int id)
        {
            Tarefa tarefa = _context.Tarefas.FirstOrDefault(tarefa => tarefa.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }
            _context.Remove(tarefa);
            _context.SaveChanges();
            return NoContent();

        }
    }
}
