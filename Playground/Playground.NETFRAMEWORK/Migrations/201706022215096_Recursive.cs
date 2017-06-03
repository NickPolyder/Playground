namespace Playground.NETFRAMEWORK.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Recursive : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Locations",
                c => new
                    {
                        LocationID = c.Int(nullable: false, identity: true),
                        ParentLocationID = c.Int(),
                        LocationTypeID = c.Int(nullable: false),
                        InstallDate = c.DateTime(),
                        AltPhone = c.String(),
                        OfficePhone = c.String(),
                        PrimaryAddressID = c.Int(),
                        LocationName = c.String(),
                        LocationName2 = c.String(),
                    })
                .PrimaryKey(t => t.LocationID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Locations");
        }
    }
}
