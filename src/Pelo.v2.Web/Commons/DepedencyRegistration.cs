using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Autofac.Extras.DynamicProxy;
using AutoMapper;
using Castle.DynamicProxy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pelo.Common.Log.AutoWriteLog;
using Pelo.v2.Web.Factories;
using Pelo.v2.Web.Services.AppConfig;

namespace Pelo.v2.Web.Commons
{
    public static class DepedencyRegistration
    {
        public static IContainer RegisterAutofac(this IServiceCollection services,
                                                 IConfiguration configuration)
        {
            var builder = new ContainerBuilder();

            RegisterMaps(builder);

            builder.RegisterType<BaseModelFactory>()
                   .As<IBaseModelFactory>();

            builder.RegisterAssemblyTypes(typeof(AppConfigService).Assembly)
                   .Where(c => c.Name.EndsWith("Service"))
                   .AsImplementedInterfaces()
                   .EnableInterfaceInterceptors()
                   .InterceptedBy("log-calls")
                   .InstancePerLifetimeScope();

            builder.Register(c => new DynamicProxyLog(new DynamicProxyAsyncLog(configuration)))
                   .Named<IInterceptor>("log-calls");

            builder.Populate(services);
            var container = builder.Build();

            return container;
        }

        private static void RegisterMaps(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                   .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                   .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg =>
                                                          {
                                                              foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                                                              {
                                                                  cfg.AddProfile(profile);
                                                              }
                                                          }))
                   .AsSelf()
                   .SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                                   .CreateMapper(c.Resolve))
                   .As<IMapper>()
                   .InstancePerLifetimeScope();
        }
    }
}
