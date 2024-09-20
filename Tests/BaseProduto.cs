using Bogus;
using Domain.Models;

namespace Tests;

public static class BaseProduto
{
    public static List<Produto> CriarteProdutoLista(int total = 10)
    {
        Faker faker = new Faker("pt_BR");
        var produtos = new List<Produto>();
        for (int i = 0; i < total; i++)
        {
            produtos.Add(new Produto
            {
                Id = Guid.NewGuid(),
                Nome = faker.Commerce.ProductName(),
                Preco = faker.Random.Decimal(1, 100),
                Active = faker.Random.Bool()
            });
        }

        return produtos;
    }

    public static Produto CriarteProduto()
    {
        Faker faker = new Faker("pt_BR");

        var produto = new Produto
        {
            Id = Guid.NewGuid(),
            Nome = faker.Commerce.ProductName(),
            Preco = faker.Random.Decimal(1, 100),
            Active = faker.Random.Bool()
        };


        return produto;
    }
}