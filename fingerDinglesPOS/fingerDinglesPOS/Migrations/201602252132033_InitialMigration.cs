namespace fingerDinglesPOS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Carts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        checkoutTime = c.DateTime(nullable: false),
                        TransactionTotal = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SalesPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        UnitPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ProductName = c.String(),
                        Description = c.String(),
                        Sku = c.String(),
                        Quantity = c.Int(nullable: false),
                        ProductImageName = c.String(),
                        Cart_ID = c.Int(),
                        CustomerManagement_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carts", t => t.Cart_ID)
                .ForeignKey("dbo.CustomerManagements", t => t.CustomerManagement_ID)
                .Index(t => t.Cart_ID)
                .Index(t => t.CustomerManagement_ID);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ServiceName = c.String(),
                        Description = c.String(),
                        SalePrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                        ServiceImageName = c.String(),
                        Cart_ID = c.Int(),
                        CustomerManagement_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Carts", t => t.Cart_ID)
                .ForeignKey("dbo.CustomerManagements", t => t.CustomerManagement_ID)
                .Index(t => t.Cart_ID)
                .Index(t => t.CustomerManagement_ID);
            
            CreateTable(
                "dbo.CustomerManagements",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Email = c.String(),
                        Allergies = c.String(),
                        SpeicalNotes = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Services", "CustomerManagement_ID", "dbo.CustomerManagements");
            DropForeignKey("dbo.Products", "CustomerManagement_ID", "dbo.CustomerManagements");
            DropForeignKey("dbo.Services", "Cart_ID", "dbo.Carts");
            DropForeignKey("dbo.Products", "Cart_ID", "dbo.Carts");
            DropIndex("dbo.Services", new[] { "CustomerManagement_ID" });
            DropIndex("dbo.Services", new[] { "Cart_ID" });
            DropIndex("dbo.Products", new[] { "CustomerManagement_ID" });
            DropIndex("dbo.Products", new[] { "Cart_ID" });
            DropTable("dbo.CustomerManagements");
            DropTable("dbo.Services");
            DropTable("dbo.Products");
            DropTable("dbo.Carts");
        }
    }
}
