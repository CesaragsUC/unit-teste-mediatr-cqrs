using Bogus;
using Domain.Models;

namespace Infrasctructure
{
    public static class ProdutoFactory
    {
        public static Produto CreateProduto()
        {
            Faker bogus = new Faker();
            return new Produto
            {
                Id = bogus.Random.Guid(),
                Nome = bogus.Commerce.ProductName(),
                Preco = bogus.Random.Decimal(1, 1000),
                Active = bogus.Random.Bool(),

            };

        }

        public static Produto CreateNullProduto()
        {
            return null;
        }

        public static List<Produto> CreateProdutoList(int total)
        {
            var produtos = new List<Produto>();
            for (int i = 0; i < total; i++)
            {
                produtos.Add(CreateProduto());
            }

            Faker bogus = new Faker();
            var produto = new Produto
            {
                Id = Guid.Parse("dfa9057b-9d9e-427f-9e94-4fbd0d3d02a2"),
                Nome = bogus.Commerce.ProductName(),
                Preco = bogus.Random.Decimal(1, 1000),
                Active = bogus.Random.Bool(),

            };

            produtos.Add(produto);

            return produtos;

        }
    }
}
