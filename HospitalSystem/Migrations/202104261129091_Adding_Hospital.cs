namespace HospitalSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Adding_Hospital : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Hospitals",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Location = c.String(),
                        Coordinates = c.String(),
                        Phone = c.String(),
                        NumberOfBeds = c.Int(nullable: false),
                        NumberOfBedsAvailable = c.Int(nullable: false),
                        NumberOfBloodBags = c.Int(nullable: false),
                        A_Plus = c.Int(nullable: false),
                        A_Minus = c.Int(nullable: false),
                        B_Plus = c.Int(nullable: false),
                        B_Minus = c.Int(nullable: false),
                        AB_Plus = c.Int(nullable: false),
                        AB_Minus = c.Int(nullable: false),
                        O_Plus = c.Int(nullable: false),
                        O_Minus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Hospitals");
        }
    }
}
