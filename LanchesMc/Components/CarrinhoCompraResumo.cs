using LanchesMc.Models;
using LanchesMc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMc.Components
{
    public class CarrinhoCompraResumo : ViewComponent
    {
        public readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraResumo(CarrinhoCompra carrinhoCompra)
        {
            _carrinhoCompra = carrinhoCompra;
        }

        public IViewComponentResult Invoke()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
           
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVm = new CarrinhoCompraViewModel();
            carrinhoCompraVm.CarrinhoCompra = _carrinhoCompra;
            carrinhoCompraVm.CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal();
            return View(carrinhoCompraVm);
        }
    }
}
