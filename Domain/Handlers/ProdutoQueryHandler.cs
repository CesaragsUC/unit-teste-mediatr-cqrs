using Domain.Handlers.Queries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;

namespace Domain.Handlers
{
    public class ProdutoQueryHandler : 
        IRequestHandler<ProdutoQuery, List<Produto>>,
        IRequestHandler<ProdutoByIdQuery,Produto>
    {

        private readonly IRepository<Produto> _repository;

        public ProdutoQueryHandler(IRepository<Produto> repository)
        {
            _repository = repository;
        }

        public async Task<List<Produto?>> Handle(ProdutoQuery request, CancellationToken cancellationToken)
        {
            return _repository.GetAll().ToList();
        }

        public async Task<Produto?> Handle(ProdutoByIdQuery request, CancellationToken cancellationToken)
        {
            var produto = _repository.FindOne(x => x.Id == request.Id);

            if (produto == null) return null;

            return produto;
        }
    }
}
