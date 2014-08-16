using System;
using System.ServiceModel;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WcfThreadlessChannel.Tests.Services;

namespace WcfThreadlessChannel.Tests
{
    [TestClass]
    public class ThreadlessBindingTests
    {
        [TestMethod]
        public void TestRequestReplyService()
        {
            EndpointAddress address = new EndpointAddress(new Uri("net.threadless://test/RequestReplyService.svc"));
            using (var client = new ThreadlessClient<RequestReplyService, IRequestReplyService>(address))
            {
                client.Client.Ping("Hello").Should().Be("Ping: Hello");
            }
        }

        [TestMethod]
        public void TestDuplexService()
        {
            using (ServiceHost host = new ServiceHost(typeof(DuplexService)))
            {
                EndpointAddress address = new EndpointAddress(new Uri("net.threadless://test/DuplexService.svc"));
                CallbackService callbackService = new CallbackService();
                using (var client = new ThreadlessDuplexClient<DuplexService, IDuplexService>(address, callbackService))
                {
                    client.Client.Ping("Hello");
                    callbackService.Text.Should().Be("Ping: Hello");
                }
            }
        }
    }
}
