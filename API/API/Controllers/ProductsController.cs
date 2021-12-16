﻿using API.Data;
using API.Entities;
using API.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class ProductsController : BaseApiController
    {
        private readonly StoreContext _context;

        public ProductsController(StoreContext context) => _context = context;

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetProducts(string orderBy, string searchTerm)
        {
            var query = _context.Products
                .Sort(orderBy)
                .Search(searchTerm)
                .AsQueryable();

            return await query.ToListAsync();
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);

            return product == null ? NotFound() : Ok(product);
        }
    }
}
