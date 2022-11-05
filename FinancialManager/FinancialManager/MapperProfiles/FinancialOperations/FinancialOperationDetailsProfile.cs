﻿using AutoMapper;
using FinancialManager.Models;
using Shared.DTOs.FinancialOperations;
using System.Globalization;

namespace FinancialManager.MapperProfiles.FinancialOperations
{
    public class FinancialOperationDetailsProfile : Profile
    {
        public FinancialOperationDetailsProfile()
        {
            CreateMap<FinancialOperation, FinancialOperationDetailsDto>()
                .ForMember(
                    dest => dest.Amount,
                    opt => opt.MapFrom(c => ((double)c.Amount / 100).ToString("0.00") + " UAH")
                )
                .ForMember(
                    dest => dest.DateTime,
                    opt => opt.MapFrom(c => c.DateTime.ToString("G"))
                );
        }
    }
}
