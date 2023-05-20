using MeuTodo.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace MeuTodo.Controller
{
    [ApiController]
    [Route(template: "V1")]
    public class TodoController : ControllerBase
    {
        [HttpGet]
        [Route(template:"todos")]
        public List<Todo> GetAll()
        {
            return new List<Todo>();
        }
    }
}