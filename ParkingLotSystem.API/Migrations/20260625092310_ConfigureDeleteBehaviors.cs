using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ParkingLotSystem.API.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureDeleteBehaviors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTickets_ParkingSpots_ParkingSpotId",
                table: "ParkingTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTickets_Vehicles_VehicleId",
                table: "ParkingTickets");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTickets_ParkingSpots_ParkingSpotId",
                table: "ParkingTickets",
                column: "ParkingSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTickets_Vehicles_VehicleId",
                table: "ParkingTickets",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTickets_ParkingSpots_ParkingSpotId",
                table: "ParkingTickets");

            migrationBuilder.DropForeignKey(
                name: "FK_ParkingTickets_Vehicles_VehicleId",
                table: "ParkingTickets");

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTickets_ParkingSpots_ParkingSpotId",
                table: "ParkingTickets",
                column: "ParkingSpotId",
                principalTable: "ParkingSpots",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ParkingTickets_Vehicles_VehicleId",
                table: "ParkingTickets",
                column: "VehicleId",
                principalTable: "Vehicles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
