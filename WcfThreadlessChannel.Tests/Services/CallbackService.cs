using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfThreadlessChannel.Tests.Services
{
    public sealed class CallbackService : ICallbackService
    {
        public string Text { get; private set; }

        public void Callback(string text)
        {
            Text = text;
        }
    }
}
