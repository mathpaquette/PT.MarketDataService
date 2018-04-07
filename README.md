# Description
PT.MarketDataService is a windows service built with C# to collect different kind of market data from *Interactive Brokers TWS Api* and store them for further use.

The overall architecture as been design respecting DDD practices.

# Features
- Collect and store market scanners based on time schedule
- Collect and store Level 1 data based on retrieved scanner contracts

# Usage
- Define your Scanner Parameters in the database (Check the DefaultInitializer)
- Configure app.config
  - Define DefaultConnection (Connection String)
  - Configure your TWS Api (IbHost, IbPort, IbClientId)
  - Adjust the frequency (in seconds) you wish to retrieve Level 1 data (Level1RequestFrequencySec)
- (Optional) [Install the service on Windows](http://docs.topshelf-project.com/en/latest/overview/commandline.html)

*Using [IB Gateway](https://www.interactivebrokers.com/en/index.php?f=16457) with your Paper Trading account is suggested.*

### Tech stack
- [IB.CSharpApiClient](https://github.com/mathpaquette/IB.CSharpApiClient)
- [TPL Dataflow](https://docs.microsoft.com/en-us/dotnet/standard/parallel-programming/dataflow-task-parallel-library)
- [SimpleInjector](https://github.com/simpleinjector/SimpleInjector)
- [SharpRepository](https://github.com/SharpRepository/SharpRepository) with EF6 (Code First)
- [Topshelf](https://github.com/Topshelf/Topshelf)
- [NLog](https://github.com/NLog/NLog)
<!-- - WebApi (OWIN) + SignalR -->