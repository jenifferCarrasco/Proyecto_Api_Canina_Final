﻿using APLICATION.Exceptions;
using Application.Interface;
using APLICATION.Wrappers;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DOMAIN.Canina.Entities;

namespace APLICATION.Feauters.Clientes.Commands.DeleteClientCommand
{
   
    public class DeleteCaninoCommand : IRequest<Response<Guid>>
    {
        public Guid Id { get; set; }
    }
    public class DeleteCaninoCommandHandler : IRequestHandler<DeleteCaninoCommand, Response<Guid>>
    {

        private readonly IRepositoryAsync<DOMAIN.Canina.Entities.Canino> _repositoryAsync;
        

        public DeleteCaninoCommandHandler(IRepositoryAsync<DOMAIN.Canina.Entities.Canino> repositoryAsync, IMapper mapper = null)
        {
            _repositoryAsync = repositoryAsync;
          
        }


        public async Task<Response<Guid>> Handle(DeleteCaninoCommand request, CancellationToken cancellationToken)
        {
            var client = await _repositoryAsync.GetByIdAsync(request.Id);
            if (client == null)
            {
                throw new KeyNotFoundException($"Registro no encontrado con el id {request.Id}");
            }
            else
            {
                
                await _repositoryAsync.DeleteAsync(client);

                return new Response<Guid>(client.Id);
            }
        }
    }
}
