# Overview
Provides a WCF channel binding that has no transport and uses no threads. The server and client must use the same Binding object in the same app domain.

# Example

```csharp
using System.ServiceModel;

[ServiceContract]
public interface IPingService
{
    [OperationContract]
    string Ping(string text);
}

public sealed class PingService : IPingService
{
  public string Ping(string text)
  {
    return "Ping: " + text;
  }
}

public static class WcfThreadlessChannel
{
  public static void Main(string[] args)
  {
    EndpointAddress address = new EndpointAddress(new Uri("net.threadless://example/PingService.svc"));
    using (var client = new ThreadlessClient<PingService, IPingService>(address))
    {
        Console.WriteLine(client.Client.Ping("Hello"));
    }
  }
}
```

This example should write the following to the console:
```
Ping: Hello
```
