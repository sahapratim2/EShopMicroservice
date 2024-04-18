
namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<String> Category, string Description, string ImageFile, decimal Price )
        : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(command => command.Name).NotEmpty().WithMessage("Name is Reqired")
                                            .Length(2, 150).WithMessage("Name must be between 2 and 150 characters");
            RuleFor(command => command.Category).NotEmpty().WithMessage("Category is Reqired");
            RuleFor(command => command.ImageFile).NotEmpty().WithMessage("ImageFile is Reqired");
            RuleFor(command => command.Price).GreaterThan(0).WithMessage("Price must be  Greater Than 0");
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session
                                       // ,IValidator<CreateProductCommand> validator,
                                        //ILogger<CreateProductCommandHandler> logger
                                        ) 
            : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

           // logger.LogInformation("CreateProductCommandHandler.Handle called with {@command}", command);
/*
            var result= await validator.ValidateAsync(command);
            var errors= result.Errors.Select(x=> x.ErrorMessage).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors.FirstOrDefault());
            }
*/
            var product = new Product
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.ImageFile,
                Price = command.Price
            };

            session.Store(product);

            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}
