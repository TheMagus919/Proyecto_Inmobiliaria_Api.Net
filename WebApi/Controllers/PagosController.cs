using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
    public class PagosController : Controller
    {
        private readonly DataContext contexto;
		private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;

		public PagosController(DataContext contexto, IConfiguration config, IWebHostEnvironment env)
		{
			this.contexto = contexto;
			this.config = config;
			environment = env;
		}
        //GET: Pagos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Pago>> Get(int id)
		{
			try
			{
                return Ok(contexto.Pagos.Include(p => p.contrato).Where(p => p.contrato.IdContrato == id));
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
    }
}