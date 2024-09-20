using MediatR;

namespace Domain.Handlers.Comands
{
    public class DeleteProdutoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
