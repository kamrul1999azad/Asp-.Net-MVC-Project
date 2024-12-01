namespace Project_Shoe_Stock.Migrations.ContextA
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Brands",
                c => new
                    {
                        BrandId = c.Int(nullable: false, identity: true),
                        BrandName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.BrandId);
            
            CreateTable(
                "dbo.Shoes",
                c => new
                    {
                        ShoeId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                        FirstIntroducedOn = c.DateTime(nullable: false, storeType: "date"),
                        OnSale = c.Boolean(nullable: false),
                        Picture = c.String(nullable: false, maxLength: 50),
                        ShoeModelId = c.Int(nullable: false),
                        BrandId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ShoeId)
                .ForeignKey("dbo.Brands", t => t.BrandId, cascadeDelete: true)
                .ForeignKey("dbo.ShoeModels", t => t.ShoeModelId, cascadeDelete: true)
                .Index(t => t.ShoeModelId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.ShoeModels",
                c => new
                    {
                        ShoeModelId = c.Int(nullable: false, identity: true),
                        ModelName = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.ShoeModelId);
            
            CreateTable(
                "dbo.Stocks",
                c => new
                    {
                        StockId = c.Int(nullable: false, identity: true),
                        Size = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ShoeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.StockId)
                .ForeignKey("dbo.Shoes", t => t.ShoeId, cascadeDelete: true)
                .Index(t => t.ShoeId);
            //////////////////////////////////
            CreateStoredProcedure("InsertShoe", c => new {
                Name = c.String(maxLength: 50),
                FirstIntroducedOn = c.DateTime(),
                OnSale = c.Boolean(),
                Picture = c.String(maxLength: 30),
                ShoeModelId = c.Int(),
                BrandId = c.Int()
            }, @"INSERT INTO Shoes ([Name], FirstIntroducedOn, OnSale, Picture,ShoeModelId, BrandId)
                    VALUES (@Name, @FirstIntroducedOn, @OnSale, @Picture, @ShoeModelId, @BrandId)
                    SELECT SCOPE_IDENTITY() AS ShoeId
                RETURN ");
            CreateStoredProcedure("UpdateShoe", c => new
            {
                ShoeId = c.Int(),
                Name = c.String(maxLength: 50),
                FirstIntroducedOn = c.DateTime(),
                OnSale = c.Boolean(),
                Picture = c.String(maxLength: 30),
                ShoeModelId = c.Int(),
                BrandId = c.Int()
            }, @"UPDATE Shoes SET [Name] = @Name, FirstIntroducedOn=@FirstIntroducedOn, OnSale=@OnSale, Picture=@Picture,ShoeModelId=@ShoeModelId, BrandId=@BrandId
                WHERE ShoeId = @ShoeId
            RETURN");
            CreateStoredProcedure("DeleteProduct", c => new
            {
                ShoeId = c.Int()

            }, @"DELETE FROM Shoes
                WHERE ShoeId = @ShoeId
            RETURN");
            CreateStoredProcedure("DeleteShoe", c => new
            {
                ShoeId = c.Int()

            }, @"DELETE FROM Shoes
                WHERE ShoeId = @ShoeId
            RETURN");
            CreateStoredProcedure("DeleteStockByShoeId", c => new
            {
                ShoeId = c.Int()

            }, @"DELETE FROM Stocks
                WHERE ShoeId = @ShoeId
            RETURN");
            CreateStoredProcedure("InsertStock", c => new
            {
                Size = c.Int(),
                Price = c.Decimal(),
                Quantity = c.Int(),
                ShoeId = c.Int()

            }, @"INSERT INTO Stocks (Size, Price, Quantity, ShoeId)
                VALUES (@Size, @Price, @Quantity, @ShoeId)
                SELECT SCOPE_IDENTITY() as StockId
            RETURN");

        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Stocks", "ShoeId", "dbo.Shoes");
            DropForeignKey("dbo.Shoes", "ShoeModelId", "dbo.ShoeModels");
            DropForeignKey("dbo.Shoes", "BrandId", "dbo.Brands");
            DropIndex("dbo.Stocks", new[] { "ShoeId" });
            DropIndex("dbo.Shoes", new[] { "BrandId" });
            DropIndex("dbo.Shoes", new[] { "ShoeModelId" });
            DropTable("dbo.Stocks");
            DropTable("dbo.ShoeModels");
            DropTable("dbo.Shoes");
            DropTable("dbo.Brands");
        }
    }
}
