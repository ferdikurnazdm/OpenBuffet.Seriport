using Microsoft.Extensions.DependencyInjection;
using OpenBuffet.Seriport.Core.Configurations;
using OpenBuffet.Seriport.Core.Interfaces;
using OpenBuffet.Seriport.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenBuffet.Seriport.Core.Extentions {
    /// <summary>
    /// this class is a extentions to 
    /// service collection for flashtech seriport
    /// </summary>
    public static class SeriportExtention {

        /// <summary>
        /// this extention method add the open buffet
        /// seriport to given service collection
        /// </summary>
        /// <param name="services">given service collection</param>
        /// <param name="builder">open buffet seriport builder</param>
        /// <returns>return added service collection</returns>
        public static IServiceCollection AddOpenBuffetSeriport(this IServiceCollection services, Action<SeriportConfiguration> configuration) {
            SeriportConfiguration seriportConfiguration = new SeriportConfiguration();
            configuration?.Invoke(seriportConfiguration);
            services.AddSingleton<ISeriportService>(new SeriportService(seriportConfiguration));
            return services;
        }


    }
}
