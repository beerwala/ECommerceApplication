using Application.DTO;
using Application.Interface.Repository;
using AutoMapper;
using Domain.Entity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Products.Querie.GetAllProduct
{
    public class GetAllProductQuerie : IRequest<IEnumerable<ProductViewModel>>
    {

    }
    public class GetAllProductHandler : IRequestHandler<GetAllProductQuerie, IEnumerable<ProductViewModel>>
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;
        public GetAllProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _repository = productRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductViewModel>> Handle(GetAllProductQuerie request, CancellationToken cancellationToken)
        {
            var products = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(products);
        }
    }
}
