using Application.Interface.Repository;
using Application.Wrappper;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Command.Create
{
    public class CreateProductCommand:IRequest<Response<int>>
    {
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductImage { get; set; } // Image path or URL
        public string Category { get; set; }
        public string Brand { get; set; }
        public decimal SellingPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public DateTime SellingDate { get; set; }
        public int Stock { get; set; }
    }
    public class CreateProductHandler:IRequestHandler<CreateProductCommand,Response<int>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper mapper;
        public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _repository = productRepository;
            this.mapper = mapper;
        }
       

        public async Task<Response<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            await _repository.AddAsync(product);
            return new Response<int>(product.Id);
        }
    }
}
