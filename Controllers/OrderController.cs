using DHSOnlineStore.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DHSOnlineStore.Controllers
{
    [Authorize(Roles = "User")]
    public class OrderController : Controller
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _orderRepository.UserOrders();
            return View(orders);
        }
    }
}
