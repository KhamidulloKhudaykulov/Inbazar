using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inbazar.Application.Baskets.Commands;

public sealed record BasketAddItemCommand(
    Guid BasketId,
    List<OrderItem> OrderItems) : ICommand;
