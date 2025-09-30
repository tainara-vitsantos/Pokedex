using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Pokedex.Data;
using Pokedex.Models;

namespace Pokedex.Controllers
{
    public class PokemonsController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _host;

        public IWebHostEnvironment IHost { get; }

        public PokemonsController(AppDbContext context, IWebHostEnvironment host)
        {
            _context = context;
            _host = host;
        }


        // GET: Pokemons
        public async Task<IActionResult> Index()
        {
            var pokemons = _context.Pokemons
            .Include(p => p.Genero)
            .Include(p => p.Regiao)
            .Include(p => p.Tipos)
            .ThenInclude(t => t.Tipo);
            return View(await pokemons.ToListAsync());
        }

        // GET: Pokemons/Details/5
        public async Task<IActionResult> Details(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.Genero)
                .Include(p => p.Regiao)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // GET: Pokemons/Create
        public IActionResult Create()
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nome");
            ViewData["RegiaoId"] = new SelectList(_context.Regioes, "Id", "Nome");
            return View();
        }

        // POST: Pokemons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Numero,RegiaoId,GeneroId,Nome,Descricao,Altura,Peso,Imagem,Animacao")] Pokemon pokemon, IFormFile Arquivo)
        {
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nome", pokemon.GeneroId);
            ViewData["RegiaoId"] = new SelectList(_context.Regioes, "Id", "Nome", pokemon.RegiaoId);
            if (ModelState.IsValid) /* verifica se as regras do pokemon são validas */
            {
                if (PokemonExists(pokemon.Numero))
                {
                    ModelState.AddModelError("", "Número já cadastrado, verifique!");
                    return View(pokemon);
                }
                if(Arquivo != null){
                    string wwwRootPath = _host.WebRootPath;
                    string filename = pokemon.Numero.ToString("000") + Path.GetExtension(Arquivo.FileName);
                    string uploads = Path.Combine(wwwRootPath, @"img\pokemons"); /* // ou @ pra falar que é texto */
                    string newFile = Path.Combine(uploads, filename);
                    using(var stream = new FileStream(newFile, FileMode.Create)){
                        Arquivo.CopyTo(stream);
                    }
                    pokemon.Imagem = @"\img\pokemons\" + filename;
                }
                _context.Add(pokemon);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(pokemon);
        }

        // GET: Pokemons/Edit/5
        public async Task<IActionResult> Edit(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon == null)
            {
                return NotFound();
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nome", pokemon.GeneroId);
            ViewData["RegiaoId"] = new SelectList(_context.Regioes, "Id", "Nome", pokemon.RegiaoId);
            return View(pokemon);
        }

        // POST: Pokemons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(uint id, [Bind("Numero,RegiaoId,GeneroId,Nome,Descricao,Altura,Peso,Imagem,Animacao")] Pokemon pokemon)
        {
            if (id != pokemon.Numero)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pokemon);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PokemonExists(pokemon.Numero))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["GeneroId"] = new SelectList(_context.Generos, "Id", "Nome", pokemon.GeneroId);
            ViewData["RegiaoId"] = new SelectList(_context.Regioes, "Id", "Nome", pokemon.RegiaoId);
            return View(pokemon);
        }

        // GET: Pokemons/Delete/5
        public async Task<IActionResult> Delete(uint? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pokemon = await _context.Pokemons
                .Include(p => p.Genero)
                .Include(p => p.Regiao)
                .FirstOrDefaultAsync(m => m.Numero == id);
            if (pokemon == null)
            {
                return NotFound();
            }

            return View(pokemon);
        }

        // POST: Pokemons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(uint id)
        {
            var pokemon = await _context.Pokemons.FindAsync(id);
            if (pokemon != null)
            {
                _context.Pokemons.Remove(pokemon);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PokemonExists(uint id)
        {
            return _context.Pokemons.Any(e => e.Numero == id);
        }
    }
}
