namespace Domain.Models
{
    public class Produto
    {
        public Produto()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
        public string Nome { get; set; }
        public decimal Preco { get; set; }
        public bool Active { get; set; }
        public DateTime? CreatAt { get; set; }
    }
}
