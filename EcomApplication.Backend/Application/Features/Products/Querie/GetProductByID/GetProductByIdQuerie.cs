using Application.Interface.Repository;
using Application.Wrappper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Querie.GetProductByID
{
    public class GetProductByIdQuerie:IRequest<Response<Product>>
    {
        public int Id { get; set; }
    }
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuerie, Response<Product>>
    {
        private readonly IProductRepository _repository;
        public GetProductByIdHandler(IProductRepository productRepository)
        {
            _repository = productRepository;
        }
        public async Task<Response<Product>> Handle(GetProductByIdQuerie request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null) { throw new Exception($"{product} is not availble in database"); }
            return new Response<Product>(product);
        }
    }
}
