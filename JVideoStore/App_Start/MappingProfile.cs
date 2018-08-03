using AutoMapper;
using JVideoStore.Dtos;
using JVideoStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace JVideoStore.App_Start
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            Mapper.CreateMap<MembershipType, MembershipTypeDto>();
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>();            
        }
    }
}