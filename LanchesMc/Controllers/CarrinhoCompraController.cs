using LanchesMc.Models;
using LanchesMc.Repositories.Interfaces;
using LanchesMc.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMc.Controllers
{
    public class CarrinhoCompraController : Controller
    {
        private readonly ILancheRepository _Lancherepository;
        private readonly CarrinhoCompra _carrinhoCompra;

        public CarrinhoCompraController(ILancheRepository lancherepository, CarrinhoCompra carrinhoCompra)
        {
            _Lancherepository = lancherepository;
            _carrinhoCompra = carrinhoCompra;   
        }
        public IActionResult Index()
        {
            var itens = _carrinhoCompra.GetCarrinhoCompraItems();
            _carrinhoCompra.CarrinhoCompraItems = itens;

            var carrinhoCompraVm = new CarrinhoCompraViewModel();
            carrinhoCompraVm.CarrinhoCompra = _carrinhoCompra;
            carrinhoCompraVm.CarrinhoCompraTotal = _carrinhoCompra.GetCarrinhoCompraTotal();
            return View(carrinhoCompraVm);
        }

        public IActionResult AdicionarItemNoCarrinho(int lancheId)
        {
            var lancheSelecionado = _Lancherepository.Lanches.FirstOrDefault
                (s => s.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.AdicionarAoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");
        }

        public IActionResult RemoverItemDoCarrinho(int lancheId)
        {
            var lancheSelecionado = _Lancherepository.Lanches.FirstOrDefault
                (s => s.LancheId == lancheId);

            if (lancheSelecionado != null)
            {
                _carrinhoCompra.RemoverDoCarrinho(lancheSelecionado);
            }

            return RedirectToAction("Index");

        }
    }
}
