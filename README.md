# Logger

```
  ______                       __                                 __     
 /      \                     |  \                               |  \    
|  $$$$$$\  ______    ______   \$$  ______    ______    ______  _| $$_   
| $$___\$$ /      \  /      \ |  \ /      \  /      \  /      \|   $$ \  
 \$$    \ |  $$$$$$\|  $$$$$$\| $$|  $$$$$$\|  $$$$$$\|  $$$$$$\\$$$$$$  
 _\$$$$$$\| $$    $$| $$   \$$| $$| $$  | $$| $$  | $$| $$   \$$ | $$ __ 
|  \__| $$| $$$$$$$$| $$      | $$| $$__/ $$| $$__/ $$| $$       | $$|  \
 \$$    $$ \$$     \| $$      | $$| $$    $$ \$$    $$| $$        \$$  $$
  \$$$$$$   \$$$$$$$ \$$       \$$| $$$$$$$   \$$$$$$  \$$         \$$$$ 
                                  | $$                                   
                                  | $$                                   
                                   \$$                                   
```


## Description

This package ensure use seriport

## Getting Started

### Dependencies

* .NET Dependency Injection >=9.0.3

### Installing

* Install Library with Package Manager script or on Nuget GUI.

### Executing program
#### Usage
```
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
        seriportService.ErrorReceivedEvent += ((sender, e) => {
            Console.WriteLine(e.ToString());
            Console.ReadKey();
            Environment.Exit(0);
        });
        seriportService.DataReceivedEvent += ((sender, e) => {
            string readedData = string.Empty;
            int readedBytes = 0;
            TimeSpan readedTime = TimeSpan.Zero;
            if (!seriportService.TryReadSeriport(out readedData, out readedBytes, out readedTime, out exception)) {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine($"Readed Data  : {readedData}\r\n");
            Console.WriteLine($"Readed Bytes : {readedBytes}\r\n");
            Console.WriteLine($"Readed Time  : {readedTime}\r\n");

            int sendedBytes = 0;
            TimeSpan sendedTime = TimeSpan.Zero;
            if (!seriportService.TryWriteSeriport(readedData, out sendedBytes, out sendedTime, out exception)) {
                Console.WriteLine(exception.Message);
                Console.ReadKey();
                Environment.Exit(0);
            }
            Console.WriteLine($"Sended Data  : {readedData}\r\n");
            Console.WriteLine($"Sended Bytes : {sendedBytes}\r\n");
            Console.WriteLine($"Sended Time  : {sendedTime}\r\n");

        });



        if (!seriportService.TryOpenSeriport(out exception)) {
            Console.WriteLine(exception.Message);
            Console.ReadKey();
            Environment.Exit(0);
        }
        bool connectionState = false;
        if (!seriportService.TryGetSeriPortState(out connectionState, out exception)) {
            Console.WriteLine(exception.Message);
            Console.ReadKey();
            Environment.Exit(0);
        }
        Console.WriteLine($"Connected To : {seriportService.GetSeriportConfiguration.PortName}");
        while (true) { Thread.Sleep(1000); }
```
## Authors

Ferdi Kurnaz
[Contact](https://github.com/ferdikurnazdm/OpenBuffet.Seriport)

## Version History

* 0.0
    * initial nuget package release

## License

This project is licensed under the [MIT] License


## Notes:



