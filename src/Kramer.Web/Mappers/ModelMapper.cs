using AutoMapper;
using Kramer.Infra;
using Kramer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Kramer.Mappers
{
    public static class ModelMapper
    {
        public static void Map()
        {
            Mapper.CreateMap<UserRequestFormViewModel, UserRequest>().ReverseMap();
        }
    }
}