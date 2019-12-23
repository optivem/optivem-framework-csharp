﻿using Microsoft.EntityFrameworkCore;
using Optivem.Framework.Core.Domain;
using Optivem.Template.Core.Domain.Orders;
using Optivem.Template.Infrastructure.Persistence.Records;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Optivem.Template.Infrastructure.Persistence.Repositories
{
    public class OrderRepository : OrderReadRepository, IOrderRepository
    {
        public OrderRepository(DatabaseContext context) : base(context)
        {
        }

        public void Add(Order order)
        {
            var orderRecord = GetOrderRecord(order);
            Context.Orders.Add(orderRecord);
        }

        public void Remove(OrderIdentity orderId)
        {
            var orderRecord = GetOrderRecord(orderId);
            Context.Remove(orderRecord);
        }

        public async Task UpdateAsync(Order order)
        {
            var orderRecord = await Context.Orders
                .Include(e => e.OrderItems)
                .FirstOrDefaultAsync(e => e.Id == order.Id);

            UpdateOrderRecord(orderRecord, order);

            try
            {
                Context.Orders.Update(orderRecord);
                await Context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                throw new ConcurrentUpdateException(ex.Message, ex);
            }
        }

        private OrderRecord GetOrderRecord(Order order)
        {
            var orderItemRecords = order.OrderItems
                .Select(e => GetOrderItemRecord(e, order.Id))
                .ToList();

            return new OrderRecord
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                OrderStatusId = order.Status,
                OrderItems = orderItemRecords,
            };
        }

        private OrderRecord GetOrderRecord(OrderIdentity orderId)
        {
            return new OrderRecord
            {
                Id = orderId,
            };
        }

        private OrderItemRecord GetOrderItemRecord(OrderItem orderItem, Guid orderRecordId)
        {
            return new OrderItemRecord
            {
                Id = orderItem.Id,
                OrderId = orderRecordId,
                ProductId = orderItem.ProductId,
                StatusId = orderItem.Status,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
            };
        }

        private void UpdateOrderRecord(OrderRecord record, Order order)
        {
            record.CustomerId = order.CustomerId;
            record.OrderStatusId = order.Status;

            var addedOrderDetails = order.OrderItems
                .Where(e => !record.OrderItems.Any(f => f.Id == e.Id))
                .ToList();

            var addedOrderDetailRecords = addedOrderDetails
                .Select(e => CreateOrderItemRecord(e))
                .ToList();

            var removedOrderDetailRecords = record.OrderItems
                .Where(e => !order.OrderItems.Any(f => f.Id == e.Id))
                .ToList();

            var updatedOrderDetailRecords = record.OrderItems
                .Where(e => order.OrderItems.Any(f => f.Id == e.Id))
                .ToList();

            foreach (var addedOrderDetailRecord in addedOrderDetailRecords)
            {
                record.OrderItems.Add(addedOrderDetailRecord);
            }

            foreach (var removedOrderDetailRecord in removedOrderDetailRecords)
            {
                record.OrderItems.Remove(removedOrderDetailRecord);
            }

            foreach (var updatedOrderDetailRecord in updatedOrderDetailRecords)
            {
                var orderDetail = order.OrderItems.Single(e => e.Id == updatedOrderDetailRecord.Id);

                UpdateOrderItemRecord(updatedOrderDetailRecord, orderDetail);
            }
        }

        private OrderItemRecord CreateOrderItemRecord(OrderItem orderItem)
        {
            return new OrderItemRecord
            {
                ProductId = orderItem.ProductId,
                StatusId = orderItem.Status,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
            };
        }

        private void UpdateOrderItemRecord(OrderItemRecord orderItemRecord, OrderItem orderItem)
        {
            orderItemRecord.ProductId = orderItem.ProductId;
            orderItemRecord.StatusId = orderItem.Status;
            orderItemRecord.Quantity = orderItem.Quantity;
            orderItemRecord.UnitPrice = orderItem.UnitPrice;
        }
    }
}