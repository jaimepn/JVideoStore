namespace JVideoStore.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"
            INSERT INTO [dbo].[AspNetUsers] ([Id], [InactiveDate], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'74d52e26-2f7e-4b0d-8195-69f4d6b506b1', NULL, N'guest@jvideostore.com', 0, N'AGIRCjwPOPLgmLmZ+gDSsuqT5N3SKzy9YHJC8SV39CH0xS+0gh5+izlOQenpqlmxDg==', N'f92b4f85-f6c0-4abe-91ee-54525eba9bc9', NULL, 0, 0, NULL, 1, 0, N'guest@jvideostore.com')
            INSERT INTO [dbo].[AspNetUsers] ([Id], [InactiveDate], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a101beb6-6c2f-42a5-ae04-eb652b7d1c13', NULL, N'admin@jvideostore.com', 0, N'AL+OsfswN0yidsR2bpTi7Dy88rZdJTzZ/srToZul/De63rl/CNeHSWAJtMlc1qNx3w==', N'f85ef14a-6ed2-48b4-b63d-75166483f646', NULL, 0, 0, NULL, 1, 0, N'admin@jvideostore.com')

            INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'41adb391-0ba0-42ba-8148-1c93bb875b4b', N'canManageMovies')

            INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'a101beb6-6c2f-42a5-ae04-eb652b7d1c13', N'41adb391-0ba0-42ba-8148-1c93bb875b4b')
        "); 
        }
        
        public override void Down()
        {
        }
    }
}
