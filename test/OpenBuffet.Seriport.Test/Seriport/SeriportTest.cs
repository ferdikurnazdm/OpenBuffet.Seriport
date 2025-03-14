using Microsoft.Extensions.DependencyInjection;
using OpenBuffet.Seriport.Core.Extentions;
using OpenBuffet.Seriport.Core.Interfaces;
using OpenBuffet.Seriport.Core.Services;
using System.IO.Ports;

namespace OpenBuffet.Seriport.Test;

//arr
//act
//ass



[TestClass]
public class SeriportTest {
    [TestMethod]
    public void Seriport_open_connection_test() {
        //arr
        Exception exception;
        IServiceProvider serviceProvider = new ServiceCollection()
            .AddOpenBuffetSeriport(configuration => {
                configuration.PortName = "COM1"; //set the your test port
                configuration.Baudrate = 9600;
                configuration.Databit = 8;
                configuration.Paritybit = Parity.None;
                configuration.Stopbit = StopBits.One;
                configuration.ReceiveBufferSize = 1024;
                configuration.TransferBufferSize = 1024;
                configuration.ReceiveTimeout = 1000;
                configuration.TransferTimeout = 1000;
            })
            .BuildServiceProvider();
        //act
        ISeriportService seriportService = serviceProvider.GetService<ISeriportService>();
        bool expected_result = seriportService.TryOpenSeriport(out exception);
        //ass
        Assert.IsTrue(expected_result, exception.Message);
    }
}
