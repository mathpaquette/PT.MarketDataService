using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;
using PT.MarketDataService.Core.Entities;

namespace PT.MarketDataService.Repository.EF.Mappings
{
    public class ContractMap : EntityTypeConfiguration<Contract>
    {
        public ContractMap()
        {
            ToTable("Contracts");

            // key
            HasKey(t => t.Id);

            // ConId unique
            Property(t => t.ConId).HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("IX_ConId") {IsUnique = true}));
        }
    }
}