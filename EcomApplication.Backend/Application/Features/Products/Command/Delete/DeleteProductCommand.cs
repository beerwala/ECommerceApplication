using Application.Interface.Repository;
using Application.Wrappper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Command.Delete
{
    public class DeleteProductCommand:IRequest<Response<int>>
    {
        public int Id { get; set; }
    }
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Response<int>>
    {
        private readonly IProductRepository _repository;
        public DeleteProductCommandHandler(IProductRepository productRepository)
        {
            _repository = productRepository;
        }
        public async Task<Response<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _repository.GetByIdAsync(request.Id);
            if (product == null) { throw new ArgumentException($"{product} is not available in database"); }
            await _repository.DeleteAsync(product);
            return new Response<int>(product.Id);
        }
    }
}
