using System;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using src.Core.MappingProfiles;

namespace src.Infrastructure.Extensions
{
    public static class AutoMapperExtension
    {
        public static void AddAutoMapperSetup(this IServiceCollection services)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddAutoMapper(typeof(MappingProfile));
        }
    }
}