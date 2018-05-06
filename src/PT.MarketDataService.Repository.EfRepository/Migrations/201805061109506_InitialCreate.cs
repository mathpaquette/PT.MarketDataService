namespace PT.MarketDataService.Repository.EfRepository.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Level1MarketDatas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        Symbol = c.String(),
                        BidSize = c.Int(),
                        Bid = c.Double(),
                        AskSize = c.Int(),
                        Ask = c.Double(),
                        LastSize = c.Int(),
                        Last = c.Double(),
                        Open = c.Double(),
                        High = c.Double(),
                        Low = c.Double(),
                        Close = c.Double(),
                        Volume = c.Int(),
                        PutVolume = c.Int(),
                        CallVolume = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScannerConfigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Enable = c.Boolean(nullable: false),
                        StartTime = c.Time(nullable: false, precision: 7),
                        EndTime = c.Time(nullable: false, precision: 7),
                        Frequency = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ScannerParameters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ExcludeConvertible = c.String(),
                        CouponRateBelow = c.Double(),
                        CouponRateAbove = c.Double(),
                        MaturityDateBelow = c.String(),
                        MaturityDateAbove = c.String(),
                        SpRatingBelow = c.String(),
                        SpRatingAbove = c.String(),
                        MoodyRatingBelow = c.String(),
                        MoodyRatingAbove = c.String(),
                        MarketCapBelow = c.Double(),
                        MarketCapAbove = c.Double(),
                        AverageOptionVolumeAbove = c.Int(),
                        AboveVolume = c.Int(),
                        BelowPrice = c.Double(),
                        AbovePrice = c.Double(),
                        ScanCode = c.String(),
                        LocationCode = c.String(),
                        Instrument = c.String(),
                        NumberOfRows = c.Int(),
                        ScannerSettingPairs = c.String(),
                        StockTypeFilter = c.String(),
                        ScannerConfigId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScannerConfigs", t => t.ScannerConfigId, cascadeDelete: true)
                .Index(t => t.ScannerConfigId);
            
            CreateTable(
                "dbo.Scanners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        ParameterId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ScannerParameters", t => t.ParameterId, cascadeDelete: true)
                .Index(t => t.ParameterId);
            
            CreateTable(
                "dbo.ScannerRows",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Rank = c.Int(nullable: false),
                        Symbol = c.String(),
                        ScannerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Scanners", t => t.ScannerId, cascadeDelete: true)
                .Index(t => t.ScannerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ScannerParameters", "ScannerConfigId", "dbo.ScannerConfigs");
            DropForeignKey("dbo.Scanners", "ParameterId", "dbo.ScannerParameters");
            DropForeignKey("dbo.ScannerRows", "ScannerId", "dbo.Scanners");
            DropIndex("dbo.ScannerRows", new[] { "ScannerId" });
            DropIndex("dbo.Scanners", new[] { "ParameterId" });
            DropIndex("dbo.ScannerParameters", new[] { "ScannerConfigId" });
            DropTable("dbo.ScannerRows");
            DropTable("dbo.Scanners");
            DropTable("dbo.ScannerParameters");
            DropTable("dbo.ScannerConfigs");
            DropTable("dbo.Level1MarketDatas");
        }
    }
}
