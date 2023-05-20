using MeuTodo.Data;
using MeuTodo.Models;
using MeuTodo.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeuTodo.Controller
{
    [ApiController]
    [Route(template: "V1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route(template:"todos")]
        public async Task<IActionResult> GetAll([FromServices] AppDbContext context)
        {
            var todos = await context.Todos
                .AsNoTracking()
                .ToListAsync();
            return Ok(todos);
        }

        [HttpGet]
        [Route(template: "todos/{id}")]
        public async Task<IActionResult> Get([FromServices] AppDbContext context, [FromRoute] int id)
        {
            var todo = await context.Todos
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id);
            return todo == null 
                ? NotFound()
                : Ok(todo);
        }

        [HttpPost]
        [Route(template: "todo/insert")]
        public async Task<IActionResult> Insert([FromServices] AppDbContext context, [FromBody] CreateTodoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var todo = new Todo
            {
                Date = DateTime.Now,
                Done = false,
                Title = model.Title
            };
            
            try
            {
                await context.Todos.AddAsync(todo);
                await context.SaveChangesAsync();
                return Created(uri: $"v1/todos/{todo.Id}", todo);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}