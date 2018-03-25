# Description
PT.MarketDataService is a windows service built with C# to collect different kind of market data and store them for further use.
The overall architecture as been design respecting DDD practices.

#### Tech stack
- [IB.CSharpApiClient](https://github.com/mathpaquette/IB.CSharpApiClient)
- [SimpleInjector](https://github.com/simpleinjector/SimpleInjector)
- [SharpRepository](https://github.com/SharpRepository/SharpRepository) with Entity Framework (Code First)
- [Topshelf](https://github.com/Topshelf/Topshelf)
- [NLog](https://github.com/NLog/NLog)
<!-- - WebApi (OWIN) + SignalR -->