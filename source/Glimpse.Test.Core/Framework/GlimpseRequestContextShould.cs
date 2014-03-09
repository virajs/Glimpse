﻿using System;
using Glimpse.Core.Extensibility;
using Glimpse.Core.Framework;
using Glimpse.Test.Core.Tester;
using Xunit;

namespace Glimpse.Test.Core.Framework
{
    public class GlimpseRequestContextShould
    {
        [Fact]
        public void ReturnTheActiveRuntimePolicy()
        {
            const RuntimePolicy expectedRuntimePolicy = RuntimePolicy.DisplayGlimpseClient;
            var requestUri = new Uri("http://localhost/");

            var requestResponseAdapter = RequestResponseAdapterTester.Create(requestUri).RequestResponseAdapterMock.Object;

            var glimpseRequestContext = new GlimpseRequestContext(
                Guid.NewGuid(),
                requestResponseAdapter,
                expectedRuntimePolicy,
                ResourceEndpointConfigurationTester.Create(requestUri, false).ResourceEndpointConfigurationMock.Object,
                "/glimpse.axd");

            Assert.Equal(expectedRuntimePolicy, glimpseRequestContext.CurrentRuntimePolicy);
        }

        [Fact]
        public void SetTheRequestHandlingModeToRegularRequest()
        {
            var regularRequestUri = new Uri("http://localhost/test");

            var glimpseRequestContext = new GlimpseRequestContext(
                    Guid.NewGuid(),
                    RequestResponseAdapterTester.Create(regularRequestUri).RequestResponseAdapterMock.Object,
                    RuntimePolicy.On,
                    ResourceEndpointConfigurationTester.Create(regularRequestUri, false).ResourceEndpointConfigurationMock.Object,
                    "/glimpse.axd");

            Assert.Equal(RequestHandlingMode.RegularRequest, glimpseRequestContext.RequestHandlingMode);
        }

        [Fact]
        public void SetTheRequestHandlingModeToResourceRequest()
        {
            var resourceRequestUri = new Uri("http://localhost/glimpse.axd?n=something");

            var glimpseRequestContext = new GlimpseRequestContext(
                    Guid.NewGuid(),
                    RequestResponseAdapterTester.Create(resourceRequestUri).RequestResponseAdapterMock.Object,
                    RuntimePolicy.On,
                    ResourceEndpointConfigurationTester.Create(resourceRequestUri, true).ResourceEndpointConfigurationMock.Object,
                   "/glimpse.axd");

            Assert.Equal(RequestHandlingMode.ResourceRequest, glimpseRequestContext.RequestHandlingMode);
        }
    }
}