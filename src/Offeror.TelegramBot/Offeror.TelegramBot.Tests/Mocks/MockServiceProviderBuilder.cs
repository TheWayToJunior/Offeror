﻿using Microsoft.Extensions.DependencyInjection;
using Moq;
using System;

namespace Offeror.TelegramBot.Tests.Mocks
{
    internal class MockServiceProviderBuilder
    {
        private readonly Mock<IServiceProvider> _serviceProvider;

        public MockServiceProviderBuilder()
        {
            _serviceProvider = new();
        }

        public MockServiceProviderBuilder Builde()
        {
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(_serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            _serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            return this;
        }

        public MockServiceProviderBuilder AddService<T>(T result)
        {
            _serviceProvider
                .Setup(x => x.GetService(typeof(T)))
                .Returns(result);

            return this;
        }

        public IServiceProvider GetResult()
        {
            return _serviceProvider.Object;
        }
    }
}
