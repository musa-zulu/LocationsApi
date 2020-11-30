using Microsoft.EntityFrameworkCore.Migrations;

namespace LocationsApi.Persistence.Migrations
{
    public partial class AddInitialTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    FeatureId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryCode = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    DisplayName = table.Column<string>(nullable: true),
                    MatchedName = table.Column<string>(nullable: true),
                    HighlightedName = table.Column<string>(nullable: true),
                    WoeType = table.Column<int>(nullable: false),
                    Slug = table.Column<string>(nullable: true),
                    Id = table.Column<string>(nullable: true),
                    LongId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.FeatureId);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Latitude = table.Column<float>(nullable: false),
                    Longitude = table.Column<float>(nullable: false),
                    CountryCode = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    CrossStreet = table.Column<string>(nullable: true),
                    FormattedAddress = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    ReferralId = table.Column<string>(nullable: true),
                    HasPerk = table.Column<bool>(nullable: false),
                    VenueId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    PluralName = table.Column<string>(nullable: true),
                    ShortName = table.Column<string>(nullable: true),
                    IconPrefix = table.Column<string>(nullable: true),
                    IconSuffic = table.Column<string>(nullable: true),
                    Primary = table.Column<bool>(nullable: false),
                    LocationId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                    table.ForeignKey(
                        name: "FK_Categories_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LocationId",
                table: "Categories",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Features");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
