using Domain.Handlers;
using Domain.Handlers.Comands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Moq;
using System.Linq.Expressions;

namespace Tests
{
    public class ProductUpdateHandlerTest : BaseConfig
    {

        private readonly Mock<IRepository<Produto>> _repository;
        private readonly Mock<IMediator> _mediator;
        private ProdutoUpdateHandler _handler;
        public ProductUpdateHandlerTest()
        {
            InitializeMediatrService();

            _repository = new Mock<IRepository<Produto>>();
            _mediator = new Mock<IMediator>();
            _handler = new ProdutoUpdateHandler(_repository.Object);
        }

        [Fact(DisplayName = "Teste 01 - Atualizar com sucesso")]
        [Trait(" ProductUpdateHandler", "Unit")]
        public async Task Test1()
        {
            // Arrange
            var command = new UpdateProdutoCommand
            {
                Id =  Guid.NewGuid(),
                Nome = "Produto 01",
                Preco = 10.5m,
                Active = true
            };

            // Configura o callback para o método FindOne
            _repository.Setup(r => r.FindOne(It.IsAny<Expression<Func<Produto, bool>>>(), null))
                       .Callback<Expression<Func<Produto, bool>>, FindOptions?>((predicate, options) =>
                       { })
                       .Returns<Expression<Func<Produto, bool>>, FindOptions?>((predicate, options) =>
                       {
                           var produto = new Produto
                           {
                               Id = command.Id,
                               Nome = "Produto Teste",
                               Preco = 20.00m,
                               Active = true,
                               CreatAt = DateTime.Now
                           };
                           // Se o predicate for válido, retorne o produto
                           return predicate.Compile().Invoke(produto) ? produto : null;
                       });


            _repository.Setup(r => r.Update(It.IsAny<Produto>()))
               .Callback<Produto>(p =>
               { })
               .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            // Act
            Assert.True(result);

            _repository.Verify(r => r.FindOne(It.IsAny<Expression<Func<Produto, bool>>>(), null), Times.Once);
            _repository.Verify(r => r.Update(It.IsAny<Produto>()), Times.Once);
        }

        [Fact(DisplayName = "Teste 02 - Atualizar erro")]
        [Trait(" ProductUpdateHandler", "Unit")]
        public async Task Test2()
        {
            // Arrange

            var command = new UpdateProdutoCommand
            {
                Id = Guid.NewGuid(),
                Nome = "Produto 01",
                Preco = 10.5m,
                Active = true
            };


            // Configura o callback para o método FindOne
            _repository.Setup(r => r.FindOne(It.IsAny<Expression<Func<Produto, bool>>>(), null))
                       .Callback<Expression<Func<Produto, bool>>, FindOptions?>((predicate, options) =>
                       { })
                       .Returns<Expression<Func<Produto, bool>>, FindOptions?>((predicate, options) =>
                       {
                           var produto = new Produto
                           {
                               Id = Guid.NewGuid(),
                               Nome = "Produto Teste",
                               Preco = 20.00m,
                               Active = true,
                               CreatAt = DateTime.Now
                           };
                           // Se o predicate for válido, retorne o produto
                           return predicate.Compile().Invoke(produto) ? produto : null;
                       });


            _repository.Setup(r => r.Update(It.IsAny<Produto>()))
               .Callback<Produto>(p =>
               { })
               .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            // Act
            Assert.False(result);
            _repository.Verify(r => r.Update(It.IsAny<Produto>()), Times.Never);
        }
    }
}