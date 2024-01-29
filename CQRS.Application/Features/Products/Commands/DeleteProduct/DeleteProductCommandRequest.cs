﻿using MediatR;

namespace CQRS.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandRequest : IRequest
    {
        public int Id { get; set; }
    }
}
