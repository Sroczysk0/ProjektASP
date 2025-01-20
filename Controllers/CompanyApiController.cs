﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication5.Data;

namespace WebApplication5.Controllers;
[ApiController]
[Route("api/companies")]
public class CompanyApiController : ControllerBase
{
    private MoviesDbContext _context;

    public CompanyApiController(MoviesDbContext context)
    {
        _context = context;
        
    }

    [HttpGet]
    public async Task<IActionResult> GetFiltered(string fragment)
    {
        return Ok(
            
            _context.ProductionCompanies
                .Where(m=>m.CompanyName != null & m.CompanyName.ToLower().Contains(fragment.ToLower()))
                .AsTracking().AsEnumerable()
            
        );
    }
}