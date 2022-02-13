using Contatos.Application.Contratos;
using Contatos.Application.Dtos;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Contatos.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContatosController : ControllerBase
    {
        private readonly IContatoService _contatoService;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ContatosController(IContatoService contatoService, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            _contatoService = contatoService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var contatos = await _contatoService.GetAllContatosAsync();
                if (contatos == null) return NoContent();

                return Ok(contatos);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar contatos. Erro: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var contato = await _contatoService.GetContatoByIdAsync(id);
                if (contato == null) return NoContent();

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar recuperar contato. Erro: {ex.Message}");
            }
        }

        [HttpPost("upload-image/{id}")]
        public async Task<IActionResult> UploadImage(int id)
        {
            try
            {
                var contato = await _contatoService.GetContatoByIdAsync(id);
                if (contato == null) return NoContent();

                var file = Request.Form.Files[0];
                if (file.Length > 0)
                {
                    DeleteImage(contato.ImagemURL);
                    contato.ImagemURL = await SaveImage(file);
                }
                var contatoRetorno = await _contatoService.UpdateContato(id, contato);

                return Ok(contatoRetorno);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar contato. Erro: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(ContatoDto model)
        {
            try
            {
                var contato = await _contatoService.AddContatos(model);
                if (contato == null) return NoContent();

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar adicionar contato. Erro: {ex.Message}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, ContatoDto model)
        {
            try
            {
                var contato = await _contatoService.UpdateContato(id, model);
                if (contato == null) return NoContent();

                return Ok(contato);
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar atualizar contato. Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var contato = await _contatoService.GetContatoByIdAsync(id);
                if (contato == null) return NoContent();

                if (await _contatoService.DeleteContato(id))
                {
                    if (!string.IsNullOrEmpty(contato.ImagemURL))
                    {
                        DeleteImage(contato.ImagemURL);
                    }

                    return Ok(new { message = "Contato Excluido." });
                }
                else
                {
                    throw new Exception("Ocorreu um problem não específico ao tentar excluir contato.");
                }
            }
            catch (Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Erro ao tentar excluir contato. Erro: {ex.Message}");
            }
        }

        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName)
                                              .Take(10)
                                              .ToArray()
                                         ).Replace(' ', '-');

            imageName = $"{imageName}{DateTime.UtcNow.ToString("yymmssfff")}{Path.GetExtension(imageFile.FileName)}";

            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);

            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }

            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, @"Resources/images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
        }
    }
}
