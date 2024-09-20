using MediatR;
using Domain.Models;

namespace Domain.Handlers.Queries
{
    public class ProdutoByIdQuery : IRequest<Produto>
    {
        public Guid Id { get; set; }
    }

    public class ProdutoQuery : IRequest<List<Produto>>
    {
    }
}
