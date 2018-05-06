namespace PT.MarketDataService.Repository.EfRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TableIndexes : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Level1MarketDatas");
            AlterColumn("dbo.Level1MarketDatas", "Symbol", c => c.String(nullable: false, maxLength: 8));
            AlterColumn("dbo.ScannerRows", "Symbol", c => c.String(maxLength: 8));
            AddPrimaryKey("dbo.Level1MarketDatas", new[] { "Timestamp", "Symbol" });
            CreateIndex("dbo.Scanners", "Timestamp");
            CreateIndex("dbo.ScannerRows", "Symbol");
            DropColumn("dbo.Level1MarketDatas", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Level1MarketDatas", "Id", c => c.Int(nullable: false, identity: true));
            DropIndex("dbo.ScannerRows", new[] { "Symbol" });
            DropIndex("dbo.Scanners", new[] { "Timestamp" });
            DropPrimaryKey("dbo.Level1MarketDatas");
            AlterColumn("dbo.ScannerRows", "Symbol", c => c.String());
            AlterColumn("dbo.Level1MarketDatas", "Symbol", c => c.String());
            AddPrimaryKey("dbo.Level1MarketDatas", "Id");
        }
    }
}
