namespace WinFastLoseFaster.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Wager = c.Int(nullable: false),
                        game_Id = c.Int(nullable: false),
                        user_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.user_Id, cascadeDelete: true)
                .Index(t => t.game_Id)
                .Index(t => t.user_Id);
            
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Timestamp = c.DateTime(nullable: false),
                        GameActive = c.Boolean(nullable: false),
                        Gametype = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        Deposit = c.Int(nullable: false),
                        Withdrawal = c.Int(nullable: false),
                        Credits = c.Int(nullable: false),
                        Mail = c.String(nullable: false),
                        Picture = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Winners",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TotalAmount = c.Int(nullable: false),
                        game_Id = c.Int(nullable: false),
                        WinningUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Games", t => t.game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.WinningUser_Id)
                .Index(t => t.game_Id)
                .Index(t => t.WinningUser_Id);
            
            CreateTable(
                "dbo.GameUsers",
                c => new
                    {
                        Game_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Game_Id, t.User_Id })
                .ForeignKey("dbo.Games", t => t.Game_Id, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Game_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Bets", "user_Id", "dbo.Users");
            DropForeignKey("dbo.Bets", "game_Id", "dbo.Games");
            DropForeignKey("dbo.Winners", "WinningUser_Id", "dbo.Users");
            DropForeignKey("dbo.Winners", "game_Id", "dbo.Games");
            DropForeignKey("dbo.GameUsers", "User_Id", "dbo.Users");
            DropForeignKey("dbo.GameUsers", "Game_Id", "dbo.Games");
            DropIndex("dbo.GameUsers", new[] { "User_Id" });
            DropIndex("dbo.GameUsers", new[] { "Game_Id" });
            DropIndex("dbo.Winners", new[] { "WinningUser_Id" });
            DropIndex("dbo.Winners", new[] { "game_Id" });
            DropIndex("dbo.Bets", new[] { "user_Id" });
            DropIndex("dbo.Bets", new[] { "game_Id" });
            DropTable("dbo.GameUsers");
            DropTable("dbo.Winners");
            DropTable("dbo.Users");
            DropTable("dbo.Games");
            DropTable("dbo.Bets");
        }
    }
}
