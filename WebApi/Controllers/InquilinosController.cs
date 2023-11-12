using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;
using WebApi.Models;

namespace WebApi.Controllers
{   
    [Route("[controller]")]
	[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
	[ApiController]
    
    public class InquilinosController : ControllerBase
    {
        private readonly DataContext contexto;
		private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;

		public InquilinosController(DataContext contexto, IConfiguration config, IWebHostEnvironment env)
		{
			this.contexto = contexto;
			this.config = config;
			environment = env;
		}

        // GET: Inquilinos/Inmuebles
        [HttpGet("Inmuebles")]
        public async Task<ActionResult<Inquilino>> GetLista()
		{
			try
			{
				var claimsList = User.Claims.ToList();
                int idP = int.Parse(claimsList[1].Value);
				var lista = contexto.Contratos.Include(c => c.Lugar).Where(c => c.Lugar.IdPropietario == idP).ToList();
				List<Inmueble> listaInmueble = new List<Inmueble>();
				foreach(var item in lista){
					Inmueble asd = await contexto.Inmuebles.FirstOrDefaultAsync(x => x.IdInmueble == item.IdInmueble);
					listaInmueble.Add(asd);
				}
				return Ok(listaInmueble);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        // GET: Inquilino/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Inquilino>> Get(int id)
		{
			try
			{
				var asd = await contexto.Contratos.SingleOrDefaultAsync(x=> x.IdInmueble == id);
                return await contexto.Inquilinos.SingleOrDefaultAsync(x=> x.IdInquilino == asd.IdInquilino);

			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

    }
}