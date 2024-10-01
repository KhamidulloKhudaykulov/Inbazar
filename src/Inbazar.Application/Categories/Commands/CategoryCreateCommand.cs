using Inbazar.Application.Abstractions.Messaging;

namespace Inbazar.Application.Categories.Commands;

public class CategoryCreateCommand : ICommand
{
    public string Name { get; set; } = string.Empty;
    public Guid SubCategory { get; set; } = Guid.Empty;
}