using Inbazar.Application.Abstractions.Messaging;
using Inbazar.Domain.Entities;
using Inbazar.Domain.Repositories;
using Inbazar.Domain.Shared;
using Inbazar.Domain.ValueObjects.Product;

namespace Inbazar.Application.Products.Commands;

public sealed class ProductUpdateCommandHandler : ICommandHandler<ProductUpdateCommand>
{
    private readonly IProductRepository _productRepository;
    private readonly ICategoryRepository _categoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ProductUpdateCommandHandler(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        ICategoryRepository categoryRepository)
    {
        _unitOfWork = unitOfWork;
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<Result> Handle(ProductUpdateCommand request, CancellationToken cancellationToken)
    {
        var product = await _productRepository.SelectById(request.Id);
        if (product is null)
        {
            return Result.Failure<Product>(new Error(
                "Product.NotFound", $"This Product with id={request.Id} was not found"));
        }

        if (request.Price <= 0)
        {
            return Result.Failure<Product>(new Error(
                "Product.InvalidArgument", $"Please enter correctly price"));
        }

        if (request.Amount < 0)
        {
            return Result.Failure<Product>(new Error(
                "Product.InvalidArgument", $"Please enter correctly amount"));
        }

        if (!string.IsNullOrEmpty(request.Name))
        {
            product.ChangeName((ProductName.Create(request.Name)).Value);
        }

        if (!string.IsNullOrEmpty(request.Description))
        {
            product.ChangeDescription((Description.Create(request.Description)).Value);
        }

        if (request.CategoryId != Guid.Empty)
        {
            var category = await _categoryRepository.SelectById((Guid)request.CategoryId!);
            if (category is null)
            {
                return Result.Failure(new Error(
                    "Category.NotFound", $"Category with id={request.CategoryId} was not found"));
            }

            //product.ProductCategories.Add(new ProductCategory
            //{
            //    ProductId = product.Id,
            //    CategoryId = category.Id
            //});
        }

        await _unitOfWork.SaveChangesAsync();

        return Result.Success(product);
    }
}
