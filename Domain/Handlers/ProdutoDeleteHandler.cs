using Domain.Handlers.Comands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Domain.Handlers
{
    public class ProdutoDeleteHandler : IRequestHandler<DeleteProdutoCommand, bool>
    {
        private readonly IRepository<Produto> _repository;

        public ProdutoDeleteHandler(IRepository<Produto> repository)
        {
            _repository = repository;
        }
        public async Task<bool> Handle(DeleteProdutoCommand request, CancellationToken cancellationToken)
        {
            if (request.Id == Guid.Empty) return false;

            var produto =  _repository.FindOne(x=> x.Id == request.Id);
            if (produto == null) return false;

            await _repository.Delete(produto);

            return true;
        }
    }
}
