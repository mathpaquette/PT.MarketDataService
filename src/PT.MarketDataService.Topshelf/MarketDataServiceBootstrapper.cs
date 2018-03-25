using System;
using Microsoft.Extensions.Configuration;
using PT.MarketDataService.Core.Models;
using PT.MarketDataService.Core.Repositories;
using PT.MarketDataService.Infrastructure.Repositories;
using PT.MarketDataService.Repository.EF.Repositories;
using SharpRepository.Ioc.SimpleInjector;
using SharpRepository.Repository;
using SharpRepository.Repository.Configuration;
using SimpleInjector;

namespace PT.MarketDataService.Topshelf
{
    public class MarketDataServiceBootstrapper
    {
        private readonly Container _container;

        public MarketDataServiceBootstrapper()
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("repository.ef.json")
                .Build();

            var section = config.GetSection("sharpRepository");
            ISharpRepositoryConfiguration sharpConfig = RepositoryFactory.BuildSharpRepositoryConfiguation(section);

            _container = new Container();
            _container.Register<ScannerController>();
            _container.Register<IScannerRepository>(() => new ScannerRepository(sharpConfig));
            _container.Register<IScannerRepositoryFactory, ScannerRepositoryFactory>();

            _container.RegisterSharpRepository(sharpConfig);
            
           //_container.Verify();
        }

        public void Start()
        {
            var scannerController = _container.GetInstance<ScannerController>();
            scannerController.Start();
        }

        public void Stop()
        {
            
        }
    }
}