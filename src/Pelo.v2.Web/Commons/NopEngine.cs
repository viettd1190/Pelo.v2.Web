using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Pelo.Common.Exceptions;

namespace Pelo.v2.Web.Commons
{
    /// <summary>
    ///     Represents Nop engine
    /// </summary>
    public class NopEngine : IEngine
    {
        #region Properties

        /// <summary>
        ///     Gets or sets service provider
        /// </summary>
        private IServiceProvider _serviceProvider { get; set; }

        #endregion

        #region Properties

        /// <summary>
        ///     Service provider
        /// </summary>
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        #endregion

        #region Utilities

        /// <summary>
        ///     Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Resolve dependency
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public T Resolve<T>()
                where T : class
        {
            return (T) Resolve(typeof(T));
        }

        /// <summary>
        ///     Resolve dependency
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <returns>Resolved service</returns>
        public object Resolve(Type type)
        {
            return GetServiceProvider()
                    .GetService(type);
        }

        /// <summary>
        ///     Resolve dependencies
        /// </summary>
        /// <typeparam name="T">Type of resolved services</typeparam>
        /// <returns>Collection of resolved services</returns>
        public virtual IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>) GetServiceProvider()
                    .GetServices(typeof(T));
        }

        /// <summary>
        ///     Resolve unregistered service
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <returns>Resolved service</returns>
        public virtual object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters()
                                                .Select(parameter =>
                                                        {
                                                            var service = Resolve(parameter.ParameterType);
                                                            if(service == null)
                                                                throw new PeloException("Unknown dependency");
                                                            return service;
                                                        });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new PeloException("No constructor was found that had all the dependencies satisfied.", innerException);
        }

        #endregion
    }
}
