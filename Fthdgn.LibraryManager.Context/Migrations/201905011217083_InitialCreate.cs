namespace Fthdgn.LibraryManager.Context.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Authors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "lower(convert(nvarchar(255),newid()))")
                                },
                            }),
                        Name = c.String(),
                        Surname = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ChangedAt = c.DateTimeOffset(precision: 7),
                        RemovedAt = c.DateTimeOffset(precision: 7),
                        IsEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        Properties_Serialized = c.String(),
                        Library_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Libraries", t => t.Library_Id)
                .Index(t => t.Library_Id);
            
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "lower(convert(nvarchar(255),newid()))")
                                },
                            }),
                        Name = c.String(),
                        Description = c.String(),
                        PublishedAt = c.DateTimeOffset(precision: 7),
                        Pages = c.Int(),
                        LCC = c.String(),
                        LCCN = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ChangedAt = c.DateTimeOffset(precision: 7),
                        RemovedAt = c.DateTimeOffset(precision: 7),
                        IsEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        Properties_Serialized = c.String(),
                        Author_Id = c.String(maxLength: 128),
                        Library_Id = c.String(maxLength: 128),
                        Image_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Authors", t => t.Author_Id)
                .ForeignKey("dbo.Libraries", t => t.Library_Id)
                .ForeignKey("dbo.Media", t => t.Image_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.Library_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.Media",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "lower(convert(nvarchar(255),newid()))")
                                },
                            }),
                        Name = c.String(),
                        Uri = c.String(),
                        Content = c.Binary(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ChangedAt = c.DateTimeOffset(precision: 7),
                        RemovedAt = c.DateTimeOffset(precision: 7),
                        IsEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        Properties_Serialized = c.String(),
                        Library_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Libraries", t => t.Library_Id)
                .Index(t => t.Library_Id);
            
            CreateTable(
                "dbo.Libraries",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "lower(convert(nvarchar(255),newid()))")
                                },
                            }),
                        Name = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        MailAddress = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ChangedAt = c.DateTimeOffset(precision: 7),
                        RemovedAt = c.DateTimeOffset(precision: 7),
                        IsEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        Properties_Serialized = c.String(),
                        Image_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.Loans",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "lower(convert(nvarchar(255),newid()))")
                                },
                            }),
                        LoanedAt = c.DateTimeOffset(precision: 7),
                        ReturnsAt = c.DateTimeOffset(precision: 7),
                        ReturnedAt = c.DateTimeOffset(precision: 7),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ChangedAt = c.DateTimeOffset(precision: 7),
                        RemovedAt = c.DateTimeOffset(precision: 7),
                        IsEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        Properties_Serialized = c.String(),
                        Book_Id = c.String(maxLength: 128),
                        Library_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.Book_Id)
                .ForeignKey("dbo.Libraries", t => t.Library_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Book_Id)
                .Index(t => t.Library_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "lower(convert(nvarchar(255),newid()))")
                                },
                            }),
                        Name = c.String(),
                        Surname = c.String(),
                        Username = c.String(),
                        MailAddress = c.String(),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        PasswordHash = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ChangedAt = c.DateTimeOffset(precision: 7),
                        RemovedAt = c.DateTimeOffset(precision: 7),
                        IsEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        Properties_Serialized = c.String(),
                        Image_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Media", t => t.Image_Id)
                .Index(t => t.Image_Id);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "lower(convert(nvarchar(255),newid()))")
                                },
                            }),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ChangedAt = c.DateTimeOffset(precision: 7),
                        RemovedAt = c.DateTimeOffset(precision: 7),
                        IsEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        Properties_Serialized = c.String(),
                        Library_Id = c.String(maxLength: 128),
                        Role_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Libraries", t => t.Library_Id)
                .ForeignKey("dbo.Roles", t => t.Role_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Library_Id)
                .Index(t => t.Role_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "lower(convert(nvarchar(255),newid()))")
                                },
                            }),
                        Name = c.String(),
                        Scopes_Serialized = c.String(),
                        CreatedAt = c.DateTimeOffset(nullable: false, precision: 7,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "getutcdate()")
                                },
                            }),
                        ChangedAt = c.DateTimeOffset(precision: 7),
                        RemovedAt = c.DateTimeOffset(precision: 7),
                        IsEnabled = c.Boolean(nullable: false,
                            annotations: new Dictionary<string, AnnotationValues>
                            {
                                { 
                                    "SqlDefaultValue",
                                    new AnnotationValues(oldValue: null, newValue: "1")
                                },
                            }),
                        Properties_Serialized = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Books", "Image_Id", "dbo.Media");
            DropForeignKey("dbo.Media", "Library_Id", "dbo.Libraries");
            DropForeignKey("dbo.UserRoles", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UserRoles", "Role_Id", "dbo.Roles");
            DropForeignKey("dbo.UserRoles", "Library_Id", "dbo.Libraries");
            DropForeignKey("dbo.Loans", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Users", "Image_Id", "dbo.Media");
            DropForeignKey("dbo.Loans", "Library_Id", "dbo.Libraries");
            DropForeignKey("dbo.Loans", "Book_Id", "dbo.Books");
            DropForeignKey("dbo.Libraries", "Image_Id", "dbo.Media");
            DropForeignKey("dbo.Books", "Library_Id", "dbo.Libraries");
            DropForeignKey("dbo.Authors", "Library_Id", "dbo.Libraries");
            DropForeignKey("dbo.Books", "Author_Id", "dbo.Authors");
            DropIndex("dbo.UserRoles", new[] { "User_Id" });
            DropIndex("dbo.UserRoles", new[] { "Role_Id" });
            DropIndex("dbo.UserRoles", new[] { "Library_Id" });
            DropIndex("dbo.Users", new[] { "Image_Id" });
            DropIndex("dbo.Loans", new[] { "User_Id" });
            DropIndex("dbo.Loans", new[] { "Library_Id" });
            DropIndex("dbo.Loans", new[] { "Book_Id" });
            DropIndex("dbo.Libraries", new[] { "Image_Id" });
            DropIndex("dbo.Media", new[] { "Library_Id" });
            DropIndex("dbo.Books", new[] { "Image_Id" });
            DropIndex("dbo.Books", new[] { "Library_Id" });
            DropIndex("dbo.Books", new[] { "Author_Id" });
            DropIndex("dbo.Authors", new[] { "Library_Id" });
            DropTable("dbo.Roles",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "lower(convert(nvarchar(255),newid()))" },
                        }
                    },
                    {
                        "IsEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                });
            DropTable("dbo.UserRoles",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "lower(convert(nvarchar(255),newid()))" },
                        }
                    },
                    {
                        "IsEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                });
            DropTable("dbo.Users",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "lower(convert(nvarchar(255),newid()))" },
                        }
                    },
                    {
                        "IsEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                });
            DropTable("dbo.Loans",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "lower(convert(nvarchar(255),newid()))" },
                        }
                    },
                    {
                        "IsEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                });
            DropTable("dbo.Libraries",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "lower(convert(nvarchar(255),newid()))" },
                        }
                    },
                    {
                        "IsEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                });
            DropTable("dbo.Media",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "lower(convert(nvarchar(255),newid()))" },
                        }
                    },
                    {
                        "IsEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                });
            DropTable("dbo.Books",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "lower(convert(nvarchar(255),newid()))" },
                        }
                    },
                    {
                        "IsEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                });
            DropTable("dbo.Authors",
                removedColumnAnnotations: new Dictionary<string, IDictionary<string, object>>
                {
                    {
                        "CreatedAt",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "getutcdate()" },
                        }
                    },
                    {
                        "Id",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "lower(convert(nvarchar(255),newid()))" },
                        }
                    },
                    {
                        "IsEnabled",
                        new Dictionary<string, object>
                        {
                            { "SqlDefaultValue", "1" },
                        }
                    },
                });
        }
    }
}
