using Domain.Handlers;
using Domain.Handlers.Comands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Moq;
using System.Linq.Expressions;

namespace Tests
{
    public class ProductDeleteHandlerTest : BaseConfig
    {

        private readonly Mock<IRepository<Produto>> _repository;
        private readonly Mock<IMediator> _mediator;
        private ProdutoDeleteHandler _handler;
        public ProductDeleteHandlerTest()
        {
            InitializeMediatrService();

            _repository = new Mock<IRepository<Produto>>();
            _mediator = new Mock<IMediator>();

            _handler = new ProdutoDeleteHandler(_repository.Object);
        }


        [Fact(DisplayName = "Teste 01 - Deletar com sucesso")]
        [Trait(" ProductDeleteHandler", "Unit")]
        public async Task Test1()
        {
            var command = new DeleteProdutoCommand
            {
                Id = Guid.NewGuid()
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


            _repository.Setup(r => r.Delete(It.IsAny<Produto>()))
               .Callback<Produto>(p =>
               { })
               .Returns(Task.CompletedTask);

            var result = await _handler.Handle(command, CancellationToken.None);

            // Act
            Assert.True(result);
            _repository.Verify(r => r.FindOne(It.IsAny<Expression<Func<Produto, bool>>>(), null), Times.Once);
            _repository.Verify(r => r.Delete(It.IsAny<Produto>()), Times.Once);
        }

        [Fact(DisplayName = "Teste 02 - Deletar com erro")]
        [Trait(" ProductDeleteHandler", "Unit")]
        public async Task Test2()
        {
            // Arrange

            var command = new DeleteProdutoCommand
            {
                Id = Guid.Empty
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            // Act
            Assert.False(result);
            _repository.Verify(r => r.FindOne(It.IsAny<Expression<Func<Produto, bool>>>(), null), Times.Never);
            _repository.Verify(r => r.Delete(It.IsAny<Produto>()), Times.Never);
        }
    }
}