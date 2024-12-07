using BE_102024.DataAces.NetCore.DBContext;
using BE102024.DataAces.NetCore.DAL_Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BE102024.DataAces.NetCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        BE_102024Context _context;
        public IProductRepository _productRepository {  get; set; }
        public IOrderRepository _orderRepository {  get; set; }
        public IOrderDetailRepository _orderDetailRepository {  get; set; }
        public UnitOfWork(BE_102024Context context, IProductRepository productRepository,
            IOrderRepository orderRepository, IOrderDetailRepository orderDetailRepository)
        {
            _context = context;
            _productRepository = productRepository;
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
        }

        public async Task<int> SaveChange()
        {
            return  await _context.SaveChangesAsync();
        }
    }
}
