using Domain.Handlers;
using Domain.Handlers.Comands;
using Domain.Handlers.Queries;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Moq;
using System.Linq.Expressions;

namespace Tests;

public class ProductoQueryHandlerTest : BaseConfig
{

    private readonly Mock<IRepository<Produto>> _repository;
    private readonly Mock<IMediator> _mediator;
    private ProdutoQueryHandler _handler;
    public ProductoQueryHandlerTest()
    {
        InitializeMediatrService();

        _repository = new Mock<IRepository<Produto>>();
        _mediator = new Mock<IMediator>();
        _handler = new ProdutoQueryHandler(_repository.Object);
    }

    [Fact(DisplayName = "Teste 01- - Rotornar lista com sucesso")]
    [Trait("ProductoQueryHandler", "Unit")]
    public async Task Test1()
    {
        var command = new ProdutoQuery();

        _repository.Setup(r => r.GetAll(It.IsAny<FindOptions>()))
                   .Callback<FindOptions>(p =>
                   { }).Returns(BaseProduto.CriarteProdutoLista().AsQueryable());


        var result = await _handler.Handle(command, CancellationToken.None);

        // Act
        Assert.True(result.Any());
        Assert.True(result.Count() > 0, "A lista não possui mais do que 1 item.");

    }

    [Fact(DisplayName = "Teste 02 - Obter por Id com sucesso")]
    [Trait("ProductoQueryHandler", "Unit")]
    public async Task Test2()
    {
        var command = new ProdutoByIdQuery { Id = Guid.NewGuid() };

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
                       return predicate.Compile().Invoke(produto) ? produto : null;
                   });


        var result = await _handler.Handle(command, CancellationToken.None);

        // Act
        Assert.NotNull(result);
    }



}
