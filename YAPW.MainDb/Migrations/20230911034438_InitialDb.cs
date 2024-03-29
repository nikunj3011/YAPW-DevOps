﻿//using System;
//using Microsoft.EntityFrameworkCore.Migrations;

//#nullable disable

//namespace YAPW.MainDb.Migrations
//{
//    /// <inheritdoc />
//    public partial class InitialDb : Migration
//    {
//        /// <inheritdoc />
//        protected override void Up(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.CreateTable(
//                name: "Links",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    LinkId = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Links", x => x.Id);
//                });

//            migrationBuilder.CreateTable(
//                name: "Actors",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    TotalVideos = table.Column<int>(type: "int", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Actors", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_Actors_Links_LinkId",
//                        column: x => x.LinkId,
//                        principalTable: "Links",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "Brands",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Count = table.Column<int>(type: "int", nullable: false),
//                    LogoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    WebsiteId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Brands", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_Brands_Links_LogoId",
//                        column: x => x.LogoId,
//                        principalTable: "Links",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_Brands_Links_WebsiteId",
//                        column: x => x.WebsiteId,
//                        principalTable: "Links",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                });

//            migrationBuilder.CreateTable(
//                name: "Categories",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Count = table.Column<int>(type: "int", nullable: false),
//                    PhotoUrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Categories", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_Categories_Links_PhotoUrlId",
//                        column: x => x.PhotoUrlId,
//                        principalTable: "Links",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "Photos",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    LinkId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Photos", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_Photos_Brands_BrandId",
//                        column: x => x.BrandId,
//                        principalTable: "Brands",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_Photos_Links_LinkId",
//                        column: x => x.LinkId,
//                        principalTable: "Links",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                });

//            migrationBuilder.CreateTable(
//                name: "Videos",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    BrandId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_Videos", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_Videos_Brands_BrandId",
//                        column: x => x.BrandId,
//                        principalTable: "Brands",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "ActorPhotos",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_ActorPhotos", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_ActorPhotos_Actors_ActorId",
//                        column: x => x.ActorId,
//                        principalTable: "Actors",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_ActorPhotos_Photos_PhotoId",
//                        column: x => x.PhotoId,
//                        principalTable: "Photos",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                });

//            migrationBuilder.CreateTable(
//                name: "PhotoCategories",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_PhotoCategories", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_PhotoCategories_Categories_CategoryId",
//                        column: x => x.CategoryId,
//                        principalTable: "Categories",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_PhotoCategories_Photos_PhotoId",
//                        column: x => x.PhotoId,
//                        principalTable: "Photos",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                });

//            migrationBuilder.CreateTable(
//                name: "PhotoInfos",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Likes = table.Column<int>(type: "int", nullable: false),
//                    Dislikes = table.Column<int>(type: "int", nullable: false),
//                    Views = table.Column<int>(type: "int", nullable: false),
//                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    BestQuality = table.Column<string>(type: "nvarchar(max)", nullable: false),
//                    VideoLength = table.Column<double>(type: "float", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_PhotoInfos", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_PhotoInfos_Photos_PhotoId",
//                        column: x => x.PhotoId,
//                        principalTable: "Photos",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateTable(
//                name: "ActorVideos",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    VideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    ActorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_ActorVideos", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_ActorVideos_Actors_ActorId",
//                        column: x => x.ActorId,
//                        principalTable: "Actors",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_ActorVideos_Videos_VideoId",
//                        column: x => x.VideoId,
//                        principalTable: "Videos",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                });

//            migrationBuilder.CreateTable(
//                name: "VideoCategories",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    VideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_VideoCategories", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_VideoCategories_Categories_CategoryId",
//                        column: x => x.CategoryId,
//                        principalTable: "Categories",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_VideoCategories_Videos_VideoId",
//                        column: x => x.VideoId,
//                        principalTable: "Videos",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                });

//            migrationBuilder.CreateTable(
//                name: "VideoInfos",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    VideoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Likes = table.Column<int>(type: "int", nullable: false),
//                    Dislikes = table.Column<int>(type: "int", nullable: false),
//                    Views = table.Column<int>(type: "int", nullable: false),
//                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    BestQuality = table.Column<int>(type: "int", nullable: false),
//                    VideoLength = table.Column<double>(type: "float", nullable: false),
//                    IsCensored = table.Column<bool>(type: "bit", nullable: false),
//                    VideoUrlId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    CoverId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    PosterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_VideoInfos", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_VideoInfos_Links_CoverId",
//                        column: x => x.CoverId,
//                        principalTable: "Links",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_VideoInfos_Links_PosterId",
//                        column: x => x.PosterId,
//                        principalTable: "Links",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                    table.ForeignKey(
//                        name: "FK_VideoInfos_Links_VideoUrlId",
//                        column: x => x.VideoUrlId,
//                        principalTable: "Links",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                    table.ForeignKey(
//                        name: "FK_VideoInfos_Videos_VideoId",
//                        column: x => x.VideoId,
//                        principalTable: "Videos",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.NoAction);
//                });

//            migrationBuilder.CreateTable(
//                name: "VideoTitles",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    VideoInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
//                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_VideoTitles", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_VideoTitles_VideoInfos_VideoInfoId",
//                        column: x => x.VideoInfoId,
//                        principalTable: "VideoInfos",
//                        principalColumn: "Id");
//                });

//            migrationBuilder.CreateTable(
//                name: "VideoInfoVideoTitles",
//                columns: table => new
//                {
//                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    VideoInfoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    VideoTitleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
//                    Active = table.Column<bool>(type: "bit", nullable: false),
//                    LastModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
//                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
//                },
//                constraints: table =>
//                {
//                    table.PrimaryKey("PK_VideoInfoVideoTitles", x => x.Id);
//                    table.ForeignKey(
//                        name: "FK_VideoInfoVideoTitles_VideoInfos_VideoInfoId",
//                        column: x => x.VideoInfoId,
//                        principalTable: "VideoInfos",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                    table.ForeignKey(
//                        name: "FK_VideoInfoVideoTitles_VideoTitles_VideoTitleId",
//                        column: x => x.VideoTitleId,
//                        principalTable: "VideoTitles",
//                        principalColumn: "Id",
//                        onDelete: ReferentialAction.Cascade);
//                });

//            migrationBuilder.CreateIndex(
//                name: "IX_ActorPhotos_ActorId",
//                table: "ActorPhotos",
//                column: "ActorId");

//            migrationBuilder.CreateIndex(
//                name: "IX_ActorPhotos_PhotoId",
//                table: "ActorPhotos",
//                column: "PhotoId");

//            migrationBuilder.CreateIndex(
//                name: "IX_Actors_LinkId",
//                table: "Actors",
//                column: "LinkId");

//            migrationBuilder.CreateIndex(
//                name: "IX_ActorVideos_ActorId",
//                table: "ActorVideos",
//                column: "ActorId");

//            migrationBuilder.CreateIndex(
//                name: "IX_ActorVideos_VideoId",
//                table: "ActorVideos",
//                column: "VideoId");

//            migrationBuilder.CreateIndex(
//                name: "IX_Brands_LogoId",
//                table: "Brands",
//                column: "LogoId");

//            migrationBuilder.CreateIndex(
//                name: "IX_Brands_WebsiteId",
//                table: "Brands",
//                column: "WebsiteId");

//            migrationBuilder.CreateIndex(
//                name: "IX_Categories_PhotoUrlId",
//                table: "Categories",
//                column: "PhotoUrlId");

//            migrationBuilder.CreateIndex(
//                name: "IX_PhotoCategories_CategoryId",
//                table: "PhotoCategories",
//                column: "CategoryId");

//            migrationBuilder.CreateIndex(
//                name: "IX_PhotoCategories_PhotoId",
//                table: "PhotoCategories",
//                column: "PhotoId");

//            migrationBuilder.CreateIndex(
//                name: "IX_PhotoInfos_PhotoId",
//                table: "PhotoInfos",
//                column: "PhotoId",
//                unique: true);

//            migrationBuilder.CreateIndex(
//                name: "IX_Photos_BrandId",
//                table: "Photos",
//                column: "BrandId");

//            migrationBuilder.CreateIndex(
//                name: "IX_Photos_LinkId",
//                table: "Photos",
//                column: "LinkId");

//            migrationBuilder.CreateIndex(
//                name: "ActiveAndUnique",
//                table: "VideoCategories",
//                columns: new[] { "Active", "CategoryId", "VideoId" },
//                unique: true,
//                filter: "[Active] != 0");

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoCategories_CategoryId",
//                table: "VideoCategories",
//                column: "CategoryId");

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoCategories_VideoId",
//                table: "VideoCategories",
//                column: "VideoId");

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoInfos_CoverId",
//                table: "VideoInfos",
//                column: "CoverId");

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoInfos_PosterId",
//                table: "VideoInfos",
//                column: "PosterId");

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoInfos_VideoId",
//                table: "VideoInfos",
//                column: "VideoId",
//                unique: true);

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoInfos_VideoUrlId",
//                table: "VideoInfos",
//                column: "VideoUrlId");

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoInfoVideoTitles_VideoInfoId",
//                table: "VideoInfoVideoTitles",
//                column: "VideoInfoId");

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoInfoVideoTitles_VideoTitleId",
//                table: "VideoInfoVideoTitles",
//                column: "VideoTitleId");

//            migrationBuilder.CreateIndex(
//                name: "IX_Videos_BrandId",
//                table: "Videos",
//                column: "BrandId");

//            migrationBuilder.CreateIndex(
//                name: "IX_VideoTitles_VideoInfoId",
//                table: "VideoTitles",
//                column: "VideoInfoId");
//        }

//        /// <inheritdoc />
//        protected override void Down(MigrationBuilder migrationBuilder)
//        {
//            migrationBuilder.DropTable(
//                name: "ActorPhotos");

//            migrationBuilder.DropTable(
//                name: "ActorVideos");

//            migrationBuilder.DropTable(
//                name: "PhotoCategories");

//            migrationBuilder.DropTable(
//                name: "PhotoInfos");

//            migrationBuilder.DropTable(
//                name: "VideoCategories");

//            migrationBuilder.DropTable(
//                name: "VideoInfoVideoTitles");

//            migrationBuilder.DropTable(
//                name: "Actors");

//            migrationBuilder.DropTable(
//                name: "Photos");

//            migrationBuilder.DropTable(
//                name: "Categories");

//            migrationBuilder.DropTable(
//                name: "VideoTitles");

//            migrationBuilder.DropTable(
//                name: "VideoInfos");

//            migrationBuilder.DropTable(
//                name: "Videos");

//            migrationBuilder.DropTable(
//                name: "Brands");

//            migrationBuilder.DropTable(
//                name: "Links");
//        }
//    }
//}
