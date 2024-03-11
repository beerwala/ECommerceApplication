using Application.Interface.Repository;
using Application.Wrappper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Command.Update
{
    public class UpdateProductCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductImage { get; set; } 
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime SellingDate { get; set; }
        public int Stock { get; set; }
    }
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Response<int>>
    {
        private readonly IProductRepository _repository;
        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Response<int>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null) { throw new ArgumentException($"Product not Found"); }
            else
            {
                product.ProductName = request.ProductName;
                product.ProductCode = request.ProductCode;
                product.ProductImage = request.ProductImage;
                product.Category = request.Category;
                product.Brand = request.Brand;
                product.PurchasePrice = request.PurchasePrice;
                product.Stock = request.Stock;
                product.PurchasePrice = request.PurchasePrice;  
                product.SellingPrice= request.SellingPrice;
                await _repository.UpdateAsync(product);
                return new Response<int>(product.Id);
            }
        }
    }
}
