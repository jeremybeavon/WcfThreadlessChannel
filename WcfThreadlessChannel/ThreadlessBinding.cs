using System;
using System.ServiceModel.Channels;

namespace WcfThreadlessChannel
{
    public sealed class ThreadlessBinding : CustomBinding
    {
        public ThreadlessBinding()
            : this(new ThreadlessBindingElement())
        {
        }

        private ThreadlessBinding(ThreadlessBindingElement bindingElement)
            : base(bindingElement)
        {
            bindingElement.Binding = this;
        }

        public Func<IChannel, IOutputSession> OutputSessionProvider { get; set; }

        public Func<IChannel, IInputSession> InputSessionProvider { get; set; }
    }
}
