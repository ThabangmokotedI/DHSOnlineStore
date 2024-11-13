using DHSOnlineStore.Data;
using DHSOnlineStore.DTOs;
using DHSOnlineStore.Models;
using DHSOnlineStore.Repositories.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DHSOnlineStore.Repositories.Class
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IHttpContextAccessor _contextAccessor;

        public OrderRepository(ApplicationDbContext context, UserManager<IdentityUser> userManager, IHttpContextAccessor contextAccessor)
        {
            _context = context;
            _userManager = userManager;
            _contextAccessor = contextAccessor;
        }
        public async Task ChangeOrderStatus(OrderStatusDTO orderStatus)
        {
            var order = await _context.Orders.FindAsync(orderStatus.OrderId);
            if (order == null)
            {
                throw new InvalidOperationException($"order with id: {orderStatus.OrderId} was not found");
            }
            order.OrderStatusId = orderStatus.OrderStatusId;
            await _context.SaveChangesAsync();
        }

        public async Task<Order> GetOrderById(int Id)
        {
            return await _context.Orders.FindAsync(Id) ?? throw new ArgumentNullException();
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatus()
        {
            return await _context.OrderStatus.ToListAsync();
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order == null)
            {
                throw new InvalidOperationException($"order with id: {orderId} was not found");
            }
            order.IsPaid = !order.IsPaid;
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
        {
            var orders = _context.Orders
                .Include(x => x.OrderStatus)
                .Include(x => x.OrderDetail)
                .ThenInclude(x => x.Product)
                .AsQueryable();

            if (!getAll)
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new Exception("User is not logged in");
                orders = orders.Where(a => a.UserId == userId);
                return await orders.ToListAsync();
            }

            return await orders.ToListAsync();
        }

        private string GetUserId()
        {
            var principal = _contextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);

            return userId;
        }
    }
}
