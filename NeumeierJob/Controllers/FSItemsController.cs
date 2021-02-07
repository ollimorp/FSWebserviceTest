using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NeumeierJob.Models;

namespace NeumeierJob.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FSItemsController : ControllerBase
    {
        private readonly FSContext _context;

        private static List<FSItem> staticdata = new List<FSItem>
        {
            new FSItem{ Name = "by ctor1", IsComplete = true, Secret = "a"},
            new FSItem{ Name = "by ctor2", IsComplete = true, Secret = "b"},
            new FSItem{ Name = "by ctor3", IsComplete = false, Secret = "c"},
            new FSItem{ Name = "by ctor4", IsComplete = true, Secret = "d"},

        };

        public FSItemsController(FSContext context)
        {
            _context = context;

            if (_context.FSItems.Count() == 0)
            {
                _context.FSItems.AddRange(staticdata);
                _context.SaveChanges();
            }
        }

        // GET: api/FSItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FSItemDTO>>> GetFSItems()
        {
            return await _context.FSItems.Select(x => ItemToDTO(x)).ToListAsync();
        }

        // GET: api/FSItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FSItemDTO>> GetFSItem(long id)
        {
            var fSItem = await _context.FSItems.FindAsync(id);

            if (fSItem == null)
            {
                return NotFound();
            }

            return ItemToDTO(fSItem);
        }

        // PUT: api/FSItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFSItem(long id, FSItemDTO fSItemDTO)
        {
            if (id != fSItemDTO.Id)
            {
                return BadRequest();
            }

            _context.Entry(fSItemDTO).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!FSItemExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/FSItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<FSItemDTO>> PostFSItem(FSItemDTO fSItemDTO)
        {
            var fSItem = new FSItem
            {
                IsComplete = fSItemDTO.IsComplete,
                Name = fSItemDTO.Name
            };

            _context.FSItems.Add(fSItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetFSItem), new { id = fSItem.Id }, ItemToDTO(fSItem));
        }

        // DELETE: api/FSItems/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<FSItemDTO>> DeleteFSItem(long id)
        {
            var fSItem = await _context.FSItems.FindAsync(id);
            if (fSItem == null)
            {
                return NotFound();
            }

            _context.FSItems.Remove(fSItem);
            await _context.SaveChangesAsync();

            return ItemToDTO(fSItem);
        }

        private bool FSItemExists(long id)
        {
            return _context.FSItems.Any(e => e.Id == id);
        }

        private static FSItemDTO ItemToDTO(FSItem fsitem) => new FSItemDTO
        {
            Id = fsitem.Id,
            Name = fsitem.Name,
            IsComplete = fsitem.IsComplete
        };
    }
}
