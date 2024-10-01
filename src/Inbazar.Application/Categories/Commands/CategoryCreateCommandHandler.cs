using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;
using Inbazar.Domain.ValueObjects.Category;

namespace Inbazar.Application.Categories.Commands;

public sealed class CategoryCreateCommandHandler : ICommandHandler<CategoryCreateCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CategoryCreateCommandHandler(
        IUnitOfWork unitOfWork,
        ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(request.Name))
        {
            return Result.Failure(new Error(
                "CategoryName.NullOrEmpty", $"Category name cannot be null or empty"));
        }

        var name = CategoryName.Create(request.Name).Value;
        if (request.SubCategory != Guid.Empty)
        {
            var result = Category.Create(name, request.SubCategory);
            await _categoryRepository.Insert(result);
        }
        else
        {
            var result = Category.Create(name, Guid.Empty);
            await _categoryRepository.Insert(result);
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success();
    }
}
