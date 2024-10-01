using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;
using Inbazar.Domain.ValueObjects.Product;

namespace Inbazar.Application.Products.Commands;

public sealed class ProductCreateCommandHandler : ICommandHandler<ProductCreateCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductCreateCommandHandler(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(ProductCreateCommand request, CancellationToken cancellationToken)
    {
        if ((await _productRepository.SelectAll()).Any(p => p.Name!.Value.ToLower() == request.Name.ToLower()))
        {
            return Result.Failure(new Error(
                code: "Product.Exist", message: $"This product with name '{request.Name}' is already exist'"));
        }

        var category = await _categoryRepository.SelectById(request.CategoryId);
        if (category is null)
        {
            return Result.Failure(new Error(
                code: "Category.NotFound", message: $"This category with id={request.CategoryId} was not found"));
        }

        Result<ProductName> name = ProductName.Create(request.Name);
        Result<Description> description = Description.Create(request.Description);
        Result<Price> price = Price.Create(request.Price);

        var result = Product.Create(
            Guid.NewGuid(), name.Value, description.Value, price.Value);

        result.ProductCategories = new List<ProductCategory>()
        {
            new ProductCategory { ProductId = result.Id, CategoryId = category.Id, Product = result, Category = category }
        };

        await _productRepository.Insert(result);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
