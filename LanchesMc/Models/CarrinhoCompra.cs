using LanchesMc.Context;

namespace LanchesMc.Models
{
    public class CarrinhoCompra
    {
        private readonly AppDbContext _context;

        public CarrinhoCompra(AppDbContext context)
        {
                _context = context;
        }

        public string CarrinhoCompraId { get; set; }
        public List<CarrinhoCompraItem> CarrinhoCompraItems { get; set; }

        public static CarrinhoCompra GetCarrinho(IServiceProvider services)
        {
            // Define uma sessao
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;

            // Obtem um servico do tipo do contexto
            var context = services.GetService<AppDbContext>();

            // obtem ou gera o Id do carrinho
            string carrinhoId = session.GetString("CarrinhoId") ?? Guid.NewGuid().ToString();

            // Atribui o Id do carrinho na sessao
            session.SetString("CarrinhoId", carrinhoId);

            // Retorna o Carrinho Com o contexto e o Id atribuido ou obtido
            return new CarrinhoCompra(context)
            {
                CarrinhoCompraId = carrinhoId
            };
        }

        public void AdicionarAoCarrinho(Lanche lanche)
        {
            var carrinhoCompraItem = _context.CarrinhoCompraItems.SingleOrDefault
            (
                s => s.Lanche.LancheId == lanche.LancheId &&
                     s.CarrinhoCompraId == CarrinhoCompraId
            );

            if (carrinhoCompraItem == null)
            {
                carrinhoCompraItem = new CarrinhoCompraItem()
                {
                    CarrinhoCompraId = CarrinhoCompraId,
                    Lanche = lanche,
                    Quantidade = 1
                };

                _context.CarrinhoCompraItems.Add(carrinhoCompraItem);
            }
            else
            {
                carrinhoCompraItem.Quantidade++;
            }

            _context.SaveChanges();
        }


    }
}
