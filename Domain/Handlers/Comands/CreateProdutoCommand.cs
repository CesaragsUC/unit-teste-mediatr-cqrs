using MediatR;

namespace Domain.Handlers.Comands
{
    public class CreateProdutoCommand : IRequest<bool>
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Active { get; set; }

    }
}
