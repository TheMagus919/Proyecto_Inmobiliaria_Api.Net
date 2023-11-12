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
    public class InmueblesController : ControllerBase
    {
        private readonly DataContext contexto;
		private readonly IConfiguration config;
		private readonly IWebHostEnvironment environment;

		public InmueblesController(DataContext contexto, IConfiguration config, IWebHostEnvironment env)
		{
			this.contexto = contexto;
			this.config = config;
			environment = env;
		}

        // GET: Inmuebles/Lista
        [HttpGet("Lista")]
        public async Task<ActionResult<Inmueble>> GetLista()
		{
			try
			{
				var claimsList = User.Claims.ToList();
                int id = int.Parse(claimsList[1].Value);
                return Ok(contexto.Inmuebles.Where(x => x.IdPropietario == id));
            }
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        // GET: Inmuebles/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Inmueble>> Get(int id)
		{
			try
			{
                return await contexto.Inmuebles.SingleOrDefaultAsync(x => x.IdInmueble == id);
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

        //PUT: Inmuebles/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult<Inmueble>> Get(int id, [FromForm] Inmueble entidad)
		{
			try
			{
                var inmuebleUpdate=await contexto.Inmuebles.FindAsync(id);
                if(inmuebleUpdate!=null){
                    inmuebleUpdate.Disponible = entidad.Disponible;
                    contexto.Update(inmuebleUpdate);
                    contexto.SaveChanges();
                    return Ok(inmuebleUpdate);
                    }
                return BadRequest("Inmueble no encontrado.");
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}

		//POST: Inmuebles
		[HttpPost()]
		public async Task<ActionResult<Inmueble>> Post([FromForm] Inmueble entidad)
		{
			try
			{	var usuario = User.Identity.Name;
				Propietario propietario = await contexto.Propietarios.FirstAsync(p => p.Email == usuario);
				entidad.IdPropietario = propietario.IdPropietario;
				var inmuebleComprobar = await contexto.Inmuebles.FindAsync(entidad.IdInmueble);
				if(inmuebleComprobar==null){
					await contexto.AddAsync(entidad);
					contexto.SaveChanges();
					if((entidad.Imagen == null || entidad.Imagen == "")&& entidad.IdInmueble >0){
						string wwwPath = environment.WebRootPath;
						string path = Path.Combine(wwwPath,"uploads/fotosInmuebles");
						if(!Directory.Exists(path)){
							Directory.CreateDirectory(path);
						}
						string fileName = propietario.Dni+"Inmu"+entidad.IdInmueble+ Path.GetExtension(entidad.ImagenFile.FileName);
						string pathCompleto = Path.Combine(path, fileName);
                        entidad.Imagen = Path.Combine("uploads/fotosInmuebles", fileName);
                        using (FileStream stream = new FileStream(pathCompleto, FileMode.Create))
                        {
                            entidad.ImagenFile.CopyTo(stream);
                        }
                        contexto.Inmuebles.Update(entidad);
                        await contexto.SaveChangesAsync();
					}
					return CreatedAtAction(nameof(GetLista), new { id = entidad.IdInmueble }, entidad);
				}else{
					return Conflict($"El inmueble con ID {entidad.IdInmueble}, ya existe");
				}
			}
			catch (Exception ex)
			{
				return BadRequest(ex.Message);
			}
		}
    }
}