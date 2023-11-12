using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
    public class ContratosController : Controller
    {
        private readonly DataContext contexto;
		private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;

		public ContratosController(DataContext contexto, IConfiguration config, IWebHostEnvironment env)
		{
			this.contexto = contexto;
			this.config = config;
			environment = env;
		}

        //GET: Contratos
        [HttpGet]
        public async Task<ActionResult<Inmueble>> Get()
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

		//GET: Contratos/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Contrato>> Get(int id)
		{
			try
			{
				Contrato result = await contexto.Contratos.Include(c => c.Lugar).Include(c => c.Vive).SingleOrDefaultAsync(x => x.IdInmueble == id);
				return Ok(result);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

    }
}