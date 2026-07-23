using HotelReservationAPI.Data;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HotelReservationAPI.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20260723183000_PreventOverlappingReservations")]
    public partial class PreventOverlappingReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("CREATE EXTENSION IF NOT EXISTS btree_gist;");
            migrationBuilder.Sql(
                "ALTER TABLE \"Reservations\" ADD CONSTRAINT \"EX_Reservations_Room_DateRange\" " +
                "EXCLUDE USING gist (\"RoomId\" WITH =, " +
                "tstzrange(\"CheckInDate\", \"CheckOutDate\", '[)') WITH &&);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                "ALTER TABLE \"Reservations\" DROP CONSTRAINT \"EX_Reservations_Room_DateRange\";");
        }
    }
}
