using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLotSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class UpdateParkingSpotRelationship : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTickets_ParkingSpots_ParkingSpotId",
                table: "ParkingTickets");

            migrationBuilder.DropIndex(
                name: "IX_ParkingTickets_ParkingSpotId",
                table: "ParkingTickets");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTickets_ParkingSpotId",
                table: "ParkingTickets",
                column: "ParkingSpotId");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTickets_ParkingSpots_ParkingSpotId",
                table: "ParkingTickets",
                column: "ParkingSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTickets_ParkingSpots_ParkingSpotId",
                table: "ParkingTickets");

            migrationBuilder.DropIndex(
                name: "IX_ParkingTickets_ParkingSpotId",
                table: "ParkingTickets");

            migrationBuilder.CreateIndex(
                name: "IX_ParkingTickets_ParkingSpotId",
                table: "ParkingTickets",
                column: "ParkingSpotId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTickets_ParkingSpots_ParkingSpotId",
                table: "ParkingTickets",
                column: "ParkingSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
