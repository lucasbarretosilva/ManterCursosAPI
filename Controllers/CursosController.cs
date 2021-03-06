using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ManterCursosAPI.Data;
using ManterCursosAPI.Models;

namespace ManterCursosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CursosController : ControllerBase
    {
        private readonly ManterCursosAPIContext _context;

        public CursosController(ManterCursosAPIContext context)
        {
            _context = context;
        }

        // GET: api/Cursos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCurso()
        {
          if (_context.Curso == null)
          {
              return NotFound();
          }
            return await _context.Curso.ToListAsync();
        }

        [HttpGet("cursosAtivos/")]
        public async Task<ActionResult<IEnumerable<Curso>>> GetCursosAtivos()
        {
            if (_context.Curso == null)
            {
                return NotFound();
            }
            return await _context.Curso.Where(x=>x.Ativo).Include(y=>y.Categoria).ToListAsync();
        }

        // GET: api/Cursos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Curso>> GetCurso(int id)
        {
          if (_context.Curso == null)
          {
              return NotFound();
          }
            var curso = await _context.Curso.FindAsync(id);

            if (curso == null)
            {
                return NotFound();
            }

            return curso;
        }

        // PUT: api/Cursos/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCurso(int id, Curso curso)
        {
            var logPorCurso = await _context.Log.FirstOrDefaultAsync(x => x.CursoId == curso.CursoId);

            logPorCurso.DataUltAlteracao = DateTime.Now;
            logPorCurso.Alteracao = "Curso Atualizado";
            curso.Ativo = true;
            _context.Log.Update(logPorCurso);

            try
            {
                Boolean AgendaCheia = (_context.Curso.Any(c => c.DataInicio <= curso.DataTermino && c.DataTermino >= curso.DataInicio && c.CursoId !=curso.CursoId && c.Ativo || c.DataInicio == curso.DataInicio && c.DataTermino == curso.DataTermino && c.CursoId != curso.CursoId && c.Ativo));

            if (AgendaCheia)
            {

                return BadRequest(new { message = "Existe(m) curso(s) planejados(s) dentro do período informado. Error(AC)" });

            }

            int filtraErros = BuscarErros(curso.DataInicio, curso.DataTermino, curso.CursoId);

            switch (filtraErros)
            {
                case 1:
                    return BadRequest(new { message = "A data de fim do curso não pode ser menor que a de início" });
                    break;

                case 2:
                    return BadRequest(new { message = "A data de inicio do curso não pode ser menor que a de hoje" });
                    break;

            }



            if (id != curso.CursoId)
            {
                return BadRequest(new { message= "Teste" });
            }

            _context.Entry(curso).State = EntityState.Modified;

           
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CursoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Cursos
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Curso>> PostCurso(Curso curso)
        {
            try {





                Boolean AgendaCheia = (_context.Curso.Any(c => c.DataInicio <= curso.DataTermino && c.DataTermino >= curso.DataInicio && c.Ativo || c.DataInicio == curso.DataInicio && c.DataTermino == curso.DataTermino && c.Ativo));

                if (AgendaCheia)
                {

                    return BadRequest(new { message = "Existe(m) curso(s) planejados(s) dentro do período informado. Error(AC)" });

                }


                int filtraErros = BuscarErros(curso.DataInicio, curso.DataTermino, curso.CursoId);

                switch (filtraErros)
                {
                    case 1:
                        return BadRequest(new { message = "A data de fim do curso não pode ser menor que a de início" });
                        break;

                    case 2:
                        return BadRequest(new { message = "A data de inicio do curso não pode ser menor que a de hoje" });
                        break;



                }



                if (_context.Curso == null)
              {
              return Problem("Entity set 'ManterCursosAPIContext.Curso'  is null.");
              }

                Curso existeIgual = AcharIgual(curso);

                if (existeIgual != default && existeIgual.CursoId != 0)
                {
                    return BadRequest(new { message = "Curso já cadastrado." });
                }
                curso.Ativo = true;
                _context.Curso.Add(curso);

            await _context.SaveChangesAsync();

                Log log = new Log
                {
                    DataInclusao = DateTime.Now,
                    DataUltAlteracao = DateTime.Now,
                    Alteracao = "Curso Cadastrado",
                    AdministradorId = 1,
                    CursoId = curso.CursoId
                };
                 _context.Log.Add(log);


                await _context.SaveChangesAsync();



                return CreatedAtAction("GetCurso", new { id = curso.CursoId }, curso);

            }
            catch
            {
                return BadRequest(new { message = "Algo de errado não está certo." });
            }
        }

        // DELETE: api/Cursos/5
        [HttpPut("DeleteLogico/{CursoId}")]
        public async Task<IActionResult> DeleteCurso(int CursoId)
        {
            
            
            
            var logPorCurso = await _context.Log.FirstOrDefaultAsync(x => x.CursoId == CursoId);

            logPorCurso.DataUltAlteracao = DateTime.Now;
            logPorCurso.Alteracao = "Curso Excluído Logicamente";


            if (_context.Curso == null)
            {
                return NotFound();
            }
            var curso = await _context.Curso.FindAsync(CursoId);

            if(curso.DataTermino < DateTime.Now)
            {
                return BadRequest(new { message = "Um Curso já concluído não pode ser deletado."});
            }



            if (curso == null)
            {
                return NotFound();
            }

            curso.Ativo = false;
            _context.Curso.Update(curso);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CursoExists(int CursoId)
        {
            return (_context.Curso?.Any(e => e.CursoId == CursoId)).GetValueOrDefault();
        }


       


        int BuscarErros(DateTime inicioCurso, DateTime fimCurso, int cursoId)
        {
            
            
            DateTime hoje = DateTime.Now.Date;


            if (fimCurso < inicioCurso)
            {
                // requisito 1 "A data de fim do curso não pode ser menor que a de início"
                return 1;

            }



            if (inicioCurso < hoje)
            {
                // requisito 2 "A data de inicio não pode ser menor que a de hoje"
                return 2;
            }

           

           //erro não encontrado
           return 0;

        }

        Curso AcharIgual(Curso curso)
        {
            Curso CursoIgual =  _context.Curso.Where(x=> x.Descricao.ToUpper() == curso.Descricao.ToUpper() && x.CursoId != curso.CursoId && x.Ativo==true).FirstOrDefault();
             

            return CursoIgual;
        }

    }
}
