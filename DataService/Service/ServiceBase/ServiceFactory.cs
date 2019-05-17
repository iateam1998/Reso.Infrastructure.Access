using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataService.Service
{
    public class ServiceFactory
    {
        public static TService CreateService<TService>(IServiceProvider serviceProvider)
        {
            var scope = serviceProvider.CreateScope();
            var service = scope.ServiceProvider.GetRequiredService<TService>();
            return service;
        }
    }
}
