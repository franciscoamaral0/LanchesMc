using LanchesMc.Context;
using LanchesMc.Models;
using LanchesMc.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace LanchesMc.Repositories
{
    public class PedidoRepository : IPedidoRepository
    {
        private readonly AppDbContext _appDbContext;
        private readonly CarrinhoCompra _carrinhoCompra;

        public PedidoRepository(AppDbContext appDbContext, CarrinhoCompra carrinhoCompra)
        {
            _appDbContext = appDbContext;
            _carrinhoCompra = carrinhoCompra;
        }
        public void CriarPedido(Pedido pedido)
        {
            pedido.PedidoEnviado = DateTime.Now;
            _appDbContext.Pedidos.Add(pedido);
            _appDbContext.SaveChanges();

            var carrinhoCompraItens = _carrinhoCompra.CarrinhoCompraItems;
            foreach (var items in carrinhoCompraItens)
            {
                var pedidoDetalhe = new PedidoDetalhe()
                {
                    Quantidade = items.Quantidade,
                    LancheId = items.Lanche.LancheId,
                    PedidoId = pedido.PedidoId,
                    Preco = items.Lanche.Preco


                };
                _appDbContext.PedidoDetalhes.Add(pedidoDetalhe);
            }

            _appDbContext.SaveChanges(); 
        }
    }
}
