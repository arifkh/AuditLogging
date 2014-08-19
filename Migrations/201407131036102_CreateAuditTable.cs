namespace AuditDemo.ConsoleApplication.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateAuditTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Audits",
                c => new
                    {
                        AuditId = c.Int(nullable: false, identity: true),
                        EntityName = c.String(),
                        LogData = c.String(),
                        ModifiedDate = c.DateTime(nullable: false),
                        ModifiedBy = c.String(),
                    })
                .PrimaryKey(t => t.AuditId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Audits");
        }
    }
}
