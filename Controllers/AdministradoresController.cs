﻿using System;
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
    public class AdministradoresController : ControllerBase
    {
        private readonly ManterCursosAPIContext _context;

        public AdministradoresController(ManterCursosAPIContext context)
        {
            _context = context;
        }

        // GET: api/Administradores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Administrador>>> GetAdministrador()
        {
          if (_context.Administrador == null)
          {
              return NotFound();
          }
            return await _context.Administrador.ToListAsync();
        }

        // GET: api/Administradores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Administrador>> GetAdministrador(int id)
        {
          if (_context.Administrador == null)
          {
              return NotFound();
          }
            var administrador = await _context.Administrador.FindAsync(id);

            if (administrador == null)
            {
                return NotFound();
            }

            return administrador;
        }

        // PUT: api/Administradores/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdministrador(int id, Administrador administrador)
        {
            if (id != administrador.AdministradorId)
            {
                return BadRequest();
            }

            _context.Entry(administrador).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdministradorExists(id))
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

        // POST: api/Administradores
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Administrador>> PostAdministrador(Administrador administrador)
        {
          if (_context.Administrador == null)
          {
              return Problem("Entity set 'ManterCursosAPIContext.Administrador'  is null.");
          }
            _context.Administrador.Add(administrador);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdministrador", new { id = administrador.AdministradorId }, administrador);
        }

        // DELETE: api/Administradores/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdministrador(int id)
        {
            if (_context.Administrador == null)
            {
                return NotFound();
            }
            var administrador = await _context.Administrador.FindAsync(id);
            if (administrador == null)
            {
                return NotFound();
            }

            _context.Administrador.Remove(administrador);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdministradorExists(int id)
        {
            return (_context.Administrador?.Any(e => e.AdministradorId == id)).GetValueOrDefault();
        }
    }
}
