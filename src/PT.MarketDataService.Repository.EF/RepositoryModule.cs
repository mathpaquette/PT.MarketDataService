using System;
using Microsoft.Extensions.Configuration;
using PT.MarketDataService.Core.Repositories;
using PT.MarketDataService.Repository.EF.Repositories;
using SharpRepository.Ioc.SimpleInjector;
using SharpRepository.Repository;
using SharpRepository.Repository.Configuration;
using SimpleInjector;
using SimpleInjector.Packaging;

namespace PT.MarketDataService.Repository.EF
{
    public class RepositoryModule : IPackage
    {
        public void RegisterServices(Container container)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("repository.ef.json")
                .Build();

            var section = config.GetSection("sharpRepository");
            ISharpRepositoryConfiguration sharpConfig = RepositoryFactory.BuildSharpRepositoryConfiguation(section);
            container.RegisterSharpRepository(sharpConfig);

            // repositories
            container.Register<ILevel1MarketDataRepository>(() => new Level1MarketDataRepository(new MarketDataServiceContext()));
            container.Register<IScannerRepository>(() => new ScannerRepository(new MarketDataServiceContext()));
            container.Register<IScannerConfigRepository>(() => new ScannerConfigRepository(sharpConfig));
            container.Register<IContractRepository>(() => new ContractRepository(sharpConfig), Lifestyle.Singleton);
        }
    }
}