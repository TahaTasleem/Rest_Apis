﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestApis.Data;
using RestApis.Exception_Handler;
using RestApis.Models;
using RestApis.Repository;
using RestApis.UnitofWork;

namespace RestApis.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    [ApiExceptionHandler]
    public class OrderController : Controller
    {
        private readonly IUnitOfwork _unitOfWork;
        IRepository<Order> orderRepository;

        public OrderController(IUnitOfwork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            orderRepository = new OrderRepository(_unitOfWork,"crud2");
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrder()
        {
            var orders = await orderRepository.Get();
            return orders;
        }

        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(Order order)
        {
            var orders = await orderRepository.Create(order);
            return orders;
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var orders = await orderRepository.Delete(id);
            return orders;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOrder(int id, Order order)
        {
            var orders = await orderRepository.Update(id, order);
            return orders;
        }
    }
   
}
