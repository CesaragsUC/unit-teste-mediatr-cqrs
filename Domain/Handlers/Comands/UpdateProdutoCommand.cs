using MediatR;

namespace Domain.Handlers.Comands
{
    public class UpdateProdutoCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Active { get; set; }
    }
}
