using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using API.ViewModels.ProductModel;
using AutoMapper;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly DbthinhContext _context;
        public readonly IMapper _mapper;


        public ProductsController(DbthinhContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<List<ProductViewModel>>> GetProducts()
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<ProductViewModel>>(await _context.Products.Include(x => x.Cate).ToListAsync());
        }

        [HttpGet("Search")]
        public async Task<ActionResult<List<ProductViewModel>>> SearchProducts([FromQuery] SearchProductModel model)
        {
            if (model.Id == null && model.ProductName == null && model.CateId == null && model.Price == null) 
            {
                return await GetProducts();
            }

            if (_context.Products == null)
            {
                return NotFound();
            }
            return _mapper.Map<List<ProductViewModel>>(await _context.Products.Where(x => x.Id == model.Id
                                                || x.ProductName.Contains(model.ProductName)
                                                || x.Price == model.Price
                                                || x.CateId == model.CateId
                                                ).ToListAsync());
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductViewModel>> GetProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = _mapper.Map<ProductViewModel>(_context.Products.Include(x => x.Cate).Where(x => x.Id == id).FirstOrDefault());

            if (product == null)
            {
                return NotFound();
            }

            return product;
        }

        // PUT: api/Products/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, CreateProductModel model)
        {
            var findProduct = _context.Products.Find(id);
            var mapper = _mapper.Map(model, findProduct);

            _context.Entry(mapper).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductExists(id))
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

        // POST: api/Products
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(CreateProductModel model)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'DbthinhContext.Products'  is null.");
            }
            var product = _mapper.Map<Product>(model);
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProduct", new { id = product.Id }, product);
        }

        // DELETE: api/Products/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            if (_context.Products == null)
            {
                return NotFound();
            }
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
