using Application.DTO;
using Application.Features.Products.Command.Create;
using Application.Features.Products.Command.Delete;
using Application.Features.Products.Command.Update;
using Application.Features.Products.Querie.GetAllProduct;
using Application.Features.Products.Querie.GetProductByID;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
   
    public class ProductController : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAllProduct()
        {

            return Ok(await Mediator.Send(new GetAllProductQuerie()));
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuerie { Id = id }));
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateProductCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await Mediator.Send(new DeleteProductCommand { Id = id }));
        }
    }
}
