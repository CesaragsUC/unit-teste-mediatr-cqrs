using Domain.Handlers.Comands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Domain.Handlers
{
    public class ProdutoUpdateHandler : IRequestHandler<UpdateProdutoCommand, bool>
    {

        private readonly IRepository<Produto> _repository;

        public ProdutoUpdateHandler(IRepository<Produto> repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(UpdateProdutoCommand request, CancellationToken cancellationToken)
        {
            var produto = _repository.FindOne(x => x.Id == request.Id);

            if (produto == null) return false;

            produto.Nome = request.Nome;
            produto.Preco = request.Preco;
            produto.Active = request.Active;

            await _repository.Update(produto);

            return true;
        }
    }
}
