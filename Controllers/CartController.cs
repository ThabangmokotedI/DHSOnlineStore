using DHSOnlineStore.DTOs;
using DHSOnlineStore.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CologneStore.Controllers
{
    [Authorize(Roles = "User")]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }

        public async Task<IActionResult> AddItem(int productId, int quantity = 1, int redirect = 0)
        {
            var cartCount = await _cartRepository.AddItem(productId, quantity);
            if (redirect == 0)
                return Ok(cartCount);

            return RedirectToAction("GetUserCart");

        }

        public async Task<IActionResult> RemoveItem(int productId)
        {
            var cartCount = await _cartRepository.RemoveItem(productId);
            return RedirectToAction("GetUserCart");
        }
        public async Task<IActionResult> GetUserCart()
        {
            var cart = await _cartRepository.GetUserCart();
            return View(cart);
        }

        [HttpGet]
        public IActionResult Checkout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutDTO checkout)
        {
            bool isCheckedOut = await _cartRepository.DoCheckOut(checkout);
            //if (!ModelState.IsValid)
            //    return View(checkout);
            if (isCheckedOut == false)
            {
                TempData["errorMessage"] = "Payment failed.";
                return View(checkout);
            }
            else
            {
                TempData["successMessage"] = "Payment successful!";
                return RedirectToAction("GetUserCart");
            }

        }


    }
}
