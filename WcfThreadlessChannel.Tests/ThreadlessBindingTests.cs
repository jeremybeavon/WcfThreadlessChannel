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
            using (ServiceHost host = new ServiceHost(typeof(RequestReplyService)))
            {
                EndpointAddress address = new EndpointAddress(new Uri("net.threadless://test/RequestReplyService.svc"));
                ThreadlessBinding binding = new ThreadlessBinding();
                host.AddServiceEndpoint(typeof(IRequestReplyService), binding, address.Uri);
                host.Open();
                using (var factory = new ChannelFactory<IRequestReplyService>(binding, address))
                {
                    IRequestReplyService client = factory.CreateChannel();
                    client.Ping("Hello").Should().Be("Ping: Hello");
                }

                host.Close();
            }
        }

        [TestMethod]
        public void TestDuplexService()
        {
            using (ServiceHost host = new ServiceHost(typeof(DuplexService)))
            {
                EndpointAddress address = new EndpointAddress(new Uri("net.threadless://test/DuplexService.svc"));
                ThreadlessBinding binding = new ThreadlessBinding();
                host.AddServiceEndpoint(typeof(IDuplexService), binding, address.Uri);
                host.Open();
                CallbackService callbackService = new CallbackService();
                InstanceContext instance = new InstanceContext(callbackService);
                using (var factory = new DuplexChannelFactory<IDuplexService>(instance, binding, address))
                {
                    IDuplexService client = factory.CreateChannel();
                    client.Ping("Hello");
                    callbackService.Should().Be("Ping: Hello");
                }

                host.Close();
            }
        }
    }
}
