using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using DataService.MappingProfileModel;
using Microsoft.EntityFrameworkCore;
using DataService.Service;
using DataService.DBEntity;
using DataService.Service.ServiceAPI;

namespace DataService
{
    public static class RootConfig
    {
        public static void Entry(IServiceCollection services, IConfiguration configuration)
        {
            #region DB Config
            services.AddDbContext<UseCaseDBDevContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("UniAccessDB")));
            services.AddScoped(typeof(DbContext), typeof(UseCaseDBDevContext));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
            #endregion

            #region Service Scope
            services.AddScoped(typeof(IActorService), typeof(ActorService));
            services.AddScoped(typeof(IApplicationService), typeof(ApplicationService));
            services.AddScoped(typeof(IApplicationCharacteristicService), typeof(ApplicationCharacteristicService));
            services.AddScoped(typeof(IEcfService), typeof(EcfService));
            services.AddScoped(typeof(IEntityService), typeof(EntityService));
            services.AddScoped(typeof(ITcfService), typeof(TcfService));
            services.AddScoped(typeof(IUawService), typeof(UawService));
            services.AddScoped(typeof(IUseCaseService), typeof(UseCaseService));
            services.AddScoped(typeof(IUucwService), typeof(UucwService));
            #endregion

            #region Mapper Config
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
        }
        private static void ConfigAutoMapper(IMapperConfigurationExpression config)
        {
            config.CreateMissingTypeMaps = true;
            config.AllowNullDestinationValues = false;
        }
    }
}