using Domain.Handlers.Comands;
using Domain.Handlers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/produtos")]
    public class ProdutoController(IMediator _mediator) : Controller
    {

        [Route("all")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery]ProdutoQuery produto)
        {
            var result = await _mediator.Send(produto);

            return Ok(result);
        }

        [Route("get")]
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] ProdutoByIdQuery produto)
        {
            var result = await _mediator.Send(produto);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] CreateProdutoCommand produto)
        {
            var result =  await _mediator.Send(produto);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateProdutoCommand produto)
        {
            var result = await _mediator.Send(produto);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteProdutoCommand produto)
        {
            var result = await _mediator.Send(produto);

            return Ok(result);
        }
    }
}
