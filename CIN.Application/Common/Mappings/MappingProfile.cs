﻿using AutoMapper;
using CIN.Application.FinanceMgtDtos;
using CIN.Domain.FinanceMgt;
using System;
using System.Linq;
using System.Reflection;

namespace CIN.Application.Common.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<HRM_DEF_User, BaseLoginUserDTO>();
            //CreateMap<BankDetailDTO, HRM_DEF_BankDetail>();
           // CreateMap<TblFinSysFinanialSetupDto, TblFinSysFinanialSetup>();

            ApplyMappingsFromAssembly(Assembly.GetExecutingAssembly());
        }

        private void ApplyMappingsFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });

            }
        }
    }
}