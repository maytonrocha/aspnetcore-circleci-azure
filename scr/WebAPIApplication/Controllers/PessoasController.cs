using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace WebAPIApplication.Controllers
{ 
 
  [Route("api/pessoas")]
  public class PessoasController: Controller
  {
    private readonly  DataContext _dbContext;

    public PessoasController(DataContext dataContext)
    {
        _dbContext = dataContext;
    }

    [HttpGet]

    public async Task<IActionResult> GetPessoas()
    {
        var pessoas = await _dbContext.Pessoas.ToListAsync();
        return Json(pessoas);
    }

    [HttpPost]

    public async Task<IActionResult> CreatePessoas([FromBody]Pessoa modelo)
    {
      var pessoa = await _dbContext.Pessoas.AddAsync(modelo);
      await _dbContext.SaveChangesAsync();

      return Json(modelo);
    }

    
    [HttpPut("{id}")]

    public async Task<IActionResult> AlterPessoas(int id, [FromBody]Pessoa modelo)
    {
      Pessoa pessoa = await _dbContext.Pessoas.FindAsync(id);

      if (pessoa == null)
      {
        return NotFound();
      }

      pessoa.Name    = modelo.Name;
      pessoa.Twitter = modelo.Twitter;
      
      //await _dbContext.Pessoas.UpdateAsync(modelo);
      await _dbContext.SaveChangesAsync();

      return Json(modelo);
    }
  }
}