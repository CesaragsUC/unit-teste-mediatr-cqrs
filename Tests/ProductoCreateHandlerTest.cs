using Domain.Handlers;
using Domain.Handlers.Comands;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Moq;

//https://goatreview-com.cdn.ampproject.org/c/s/goatreview.com/mediatr-quickly-test-handlers-with-unit-tests/amp/

namespace Tests
{
    public class ProductoCreateHandlerTest : BaseConfig
    {
        private readonly Mock<IRepository<Produto>> _repository;
        private readonly Mock<IMediator> _mediator;
        private ProdutoCreateHandler _handler;
        public ProductoCreateHandlerTest()
        {
            InitializeMediatrService();

            _repository = new Mock<IRepository<Produto>>();
            _mediator = new Mock<IMediator>();
            _handler = new ProdutoCreateHandler(_repository.Object);
        }

        [Fact(DisplayName = "Teste 01 - Com sucesso")]
        [Trait("ProductoCreateHandler", "Unit")]
        public async Task Test1()
        {
            // Arrange

            var command = new CreateProdutoCommand
            {
                Nome = "Produto 01",
                Preco = 10.5m,
                Active = true
            };

            var result = await _handler.Handle(command, CancellationToken.None);

            // Act
            Assert.True(result);

            _repository.Verify(r => r.Add(It.IsAny<Produto>()), Times.Once);
        }

        [Fact(DisplayName = "Teste 02 - Com Falha")]
        [Trait("ProductoCreateHandler", "Unit")]
        public async Task Test2()
        {
            // Arrange

            var command = new CreateProdutoCommand();

            var result = await _handler.Handle(command, CancellationToken.None);

            // Act
            Assert.False(result);
            _repository.Verify(r => r.Add(It.IsAny<Produto>()), Times.Never);
        }
    }
}