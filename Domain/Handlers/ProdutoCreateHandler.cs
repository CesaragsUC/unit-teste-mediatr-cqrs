using Domain.Handlers.Comands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Domain.Handlers
{
    public class ProdutoCreateHandler : IRequestHandler<CreateProdutoCommand, bool>
    {
        private readonly IRepository<Produto> _repository;

        public ProdutoCreateHandler(IRepository<Produto> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(CreateProdutoCommand request, CancellationToken cancellationToken)
        {
            if (request is null || 
                (request.Preco <= 0 || string.IsNullOrEmpty(request.Nome)))
                return false;

            var produto = new Produto
            {
                Nome = request.Nome,
                Preco = request.Preco,
                Active = request.Active,
                CreatAt = DateTime.Now
            };

            await _repository.Add(produto);

            return true;
        }

    }
}
