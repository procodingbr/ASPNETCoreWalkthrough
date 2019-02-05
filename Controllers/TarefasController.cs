using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProCoding.Demos.ASPNETCore.Walkthrough.Models;

namespace ProCoding.Demos.ASPNETCore.Walkthrough.Controllers
{
    public class TarefasController : Controller
    {
        private readonly TarefasDbContext _db;

        public TarefasController(TarefasDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
            => View(await _db.Tarefas.ToListAsync());

        [HttpGet]
        public IActionResult Criar()
            => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criar([Bind("Descricao", "DataTarefa")] Tarefa novaTarefa, DateTime dataTarefa)
        {
            if(ModelState.IsValid)
            {
                await _db.Tarefas.AddAsync(novaTarefa);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(novaTarefa);
        }

        [HttpGet]
        public async Task<IActionResult> Editar(int ID)
        {
            Tarefa tarefa = await _db.Tarefas.FirstOrDefaultAsync(t => t.ID == ID);
            if(tarefa == null)
                return NotFound();
            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(Tarefa tarefaEditada)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    _db.Tarefas.Update(tarefaEditada);
                    await _db.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!(await _db.Tarefas.AnyAsync(t => t.ID == tarefaEditada.ID)))
                        return NotFound();
                    throw;
                }

                RedirectToAction("Index");
            }
            return View(tarefaEditada);
        }

        [HttpGet]
        public async Task<IActionResult> Excluir(int ID)
        {
            Tarefa tarefa = await _db.Tarefas.FirstOrDefaultAsync(t => t.ID == ID);
            if(tarefa == null)
                return NotFound();
            return View(tarefa);
        }        

        [HttpPost]
        [ActionName("Excluir")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExcluirTarefa(int ID)
        {
            Tarefa tarefa = await _db.Tarefas.FirstOrDefaultAsync(t => t.ID == ID);
            if(tarefa == null)
                return NotFound();

            _db.Tarefas.Remove(tarefa);
            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }        
    }
}