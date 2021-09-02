using Microsoft.EntityFrameworkCore;
using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using Ordering.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {

        private readonly OrderContext _orderContext;

        public OrderRepository(OrderContext orderContext)
            : base(orderContext)
        {
            _orderContext = orderContext ?? throw new ArgumentNullException(nameof(orderContext));
        }

        public async Task<IEnumerable<Order>> GetOrderByUserName(string userName)
        {
            return await _orderContext.Orders
                            .Where(o => o.UserName == userName)
                            .ToListAsync();
        }
    }
}
