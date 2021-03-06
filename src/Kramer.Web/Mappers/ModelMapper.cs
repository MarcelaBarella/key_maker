﻿using AutoMapper;
using Kramer.Domain;
using Kramer.Models;

namespace Kramer.Mappers
{
    public static class ModelMapper
    {
        public static void Map()
        {
            Mapper.CreateMap<UserRequestFormViewModel, UserRequest>()
                .ForMember(model => model.SaleType, opt => opt.Ignore()) //Não faz o mapeamento de SaleType de um para o SaleType do outro
                .ForMember(model => model.Status, opt => opt.Ignore())
                .ForMember(model => model.SaleTypeId, opt => opt.MapFrom(viewModel => viewModel.SaleType.Id)) //Aqui diz para salvar na propriedade SaletypeId (do db) o valor que estiver no SaleType.Id (do viewModel)
                .ForMember(model => model.SaleTypeId, opt => opt.MapFrom(viewModel => viewModel.SaleType.Id));
            Mapper.CreateMap<UserRequest, UserRequestFormViewModel>();

            Mapper.CreateMap<UserRequest, UserRequestChangeStatusViewModel>();
            Mapper.CreateMap<UserRequestChangeStatusViewModel, UserRequest>()
                .ForMember(model => model.Status, opt => opt.Ignore())
                .ForMember(model => model.StatusId, opt => opt.MapFrom(viewModel => viewModel.Status.Id));


            Mapper.CreateMap<RegisterViewModel, ApplicationUser>().ReverseMap();
            Mapper.CreateMap<SaleType, SaleTypeViewModel>();
            Mapper.CreateMap<Status, StatusViewModel>();

        }
    }
}