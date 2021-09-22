using Microsoft.EntityFrameworkCore.Migrations;

namespace FloodFinder.Infrastructure.Migrations
{
    public partial class UpdateEnquiryAddUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Enquiry",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Enquiry");
        }
    }
}
