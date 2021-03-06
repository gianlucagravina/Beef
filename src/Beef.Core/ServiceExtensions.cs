﻿// Copyright (c) Avanade. Licensed under the MIT License. See https://github.com/Avanade/Beef

using Beef.Business;
using Beef.Caching;
using Beef.Caching.Policy;
using Beef.Events;
using Beef.WebApi;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Beef
{
    /// <summary>
    /// Represents the standard <i>Beef</i> <see cref="IServiceCollection"/> extensions.
    /// </summary>
    public static class ServiceExtensions
    {
        /// <summary>
        /// Adds a scoped service to instantiate a new <see cref="ExecutionContext"/> instance.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="createExecutionContext">The function to override the creation of the <see cref="ExecutionContext"/> instance to a custom <see cref="Type"/>; defaults to <see cref="ExecutionContext"/> where not specified.</param>
        /// <returns>The <see cref="IServiceCollection"/> for fluent-style method-chaining.</returns>
        /// <remarks>Use the <paramref name="createExecutionContext"/> function to instantiate a custom <see cref="ExecutionContext"/> (inherited) <see cref="Type"/> where required; otherwise, by default the <i>Beef</i>
        /// <see cref="ExecutionContext"/> will be used.
        /// </remarks>
        public static IServiceCollection AddBeefExecutionContext(this IServiceCollection services, Func<IServiceProvider, ExecutionContext>? createExecutionContext = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services.AddScoped(sp => createExecutionContext?.Invoke(sp) ?? new ExecutionContext());
        }

        /// <summary>
        /// Adds a scoped service to instantiate a new <see cref="IRequestCache"/> <see cref="RequestCache"/> instance.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="createRequestCache">The function to override the creation of the <see cref="IRequestCache"/> instance; defaults to <see cref="RequestCache"/> where not specified.</param>
        /// <returns>The <see cref="IServiceCollection"/> for fluent-style method-chaining.</returns>
        /// <remarks>The <see cref="IRequestCache"/> enables the short-lived request caching; intended to reduce data chattiness within the context of a request scope.</remarks>
        public static IServiceCollection AddBeefRequestCache(this IServiceCollection services, Func<IServiceProvider, IRequestCache>? createRequestCache = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services.AddScoped<IRequestCache>(sp => createRequestCache?.Invoke(sp) ?? new RequestCache());
        }

        /// <summary>
        /// Adds a singleton service to instantiate a new <see cref="CachePolicyManager"/> instance with the specified <paramref name="config"/>, <paramref name="flushDueTime"/> and <paramref name="flushPeriod"/>.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="config">The optional <see cref="CachePolicyConfig"/>.</param>
        /// <param name="flushDueTime">The optional amount of time to delay before <see cref="CachePolicyManager.Flush"/> is invoked for the first time (defaults to <see cref="CachePolicyManager.TenMinutes"/>).</param>
        /// <param name="flushPeriod">The optional time interval between subsequent invocations of <see cref="CachePolicyManager.Flush"/> (defaults to <see cref="CachePolicyManager.FiveMinutes"/>).</param>
        /// <returns>The <see cref="IServiceCollection"/> for fluent-style method-chaining.</returns>
        /// <remarks>The The <see cref="CachePolicyManager"/> enables the centralised management of <see cref="ICachePolicy"/> caches.</remarks>
        public static IServiceCollection AddBeefCachePolicyManager(this IServiceCollection services, CachePolicyConfig? config = null, TimeSpan? flushDueTime = null, TimeSpan? flushPeriod = null)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services.AddSingleton(_ =>
            {
                var cpm = new CachePolicyManager();
                if (config != null)
                    cpm.SetFromCachePolicyConfig(config);

                cpm.StartFlushTimer(flushDueTime ?? CachePolicyManager.TenMinutes, flushPeriod ?? CachePolicyManager.FiveMinutes);
                return cpm;
            });
        }

        /// <summary>
        /// Adds a singleton service to instantiate a new <see cref="CachePolicyManager"/> instance.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <param name="createCachePolicyManager">The function to create the <see cref="IRequestCache"/> instance.</param>
        /// <returns>The <see cref="IServiceCollection"/> for fluent-style method-chaining.</returns>
        /// <remarks>The <see cref="IRequestCache"/> enables the short-lived request caching; intended to reduce data chattiness within the context of a request scope.</remarks>
        public static IServiceCollection AddBeefCachePolicyManager(this IServiceCollection services, Func<IServiceProvider, CachePolicyManager> createCachePolicyManager)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (createCachePolicyManager == null)
                throw new ArgumentNullException(nameof(createCachePolicyManager));

            return services.AddScoped(sp => createCachePolicyManager(sp));
        }

        /// <summary>
        /// Adds a singleton service to instantiate a new <see cref="IEventPublisher"/> <see cref="NullEventPublisher"/> instance.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for fluent-style method-chaining.</returns>
        public static IServiceCollection AddBeefNullEventPublisher(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services.AddSingleton<IEventPublisher>(_ => new NullEventPublisher());
        }

        /// <summary>
        /// Adds the required <i>Business</i> singleton services (being the <see cref="ManagerInvoker"/>, <see cref="DataSvcInvoker"/> and <see cref="DataInvoker"/>).
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for fluent-style method-chaining.</returns>
        public static IServiceCollection AddBeefBusinessServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services.AddSingleton(_ => new ManagerInvoker())
                           .AddSingleton(_ => new DataSvcInvoker())
                           .AddSingleton(_ => new DataInvoker());
        }

        /// <summary>
        /// Adds the required <i>Agent</i> (client-side) services (being the <see cref="WebApiAgentInvoker"/>).
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        /// <returns>The <see cref="IServiceCollection"/> for fluent-style method-chaining.</returns>
        public static IServiceCollection AddBeefAgentServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            return services.AddSingleton(_ => new WebApiAgentInvoker());
        }
    }
}