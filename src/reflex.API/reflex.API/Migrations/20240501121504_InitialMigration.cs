using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace reflex.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customerPhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    emailAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    userName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    firstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    lastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    customerSex = table.Column<string>(type: "nvarchar(1)", nullable: false),
                    isEmailVerified = table.Column<bool>(type: "bit", nullable: false),
                    emailVerifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isPhoneNumVerified = table.Column<bool>(type: "bit", nullable: false),
                    phoneNumVerifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isPasswordSet = table.Column<bool>(type: "bit", nullable: false),
                    userPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    passwordCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    passwordUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastLogin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    lastLoginType = table.Column<int>(type: "int", nullable: false),
                    isLoggedIn = table.Column<bool>(type: "bit", nullable: false),
                    failedLoginAttempt = table.Column<int>(type: "int", nullable: false),
                    isAccountLocked = table.Column<bool>(type: "bit", nullable: false),
                    isTPinSet = table.Column<bool>(type: "bit", nullable: false),
                    transactionPin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    transactionPinCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    transactionPinUpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    wrongTPinCount = table.Column<int>(type: "int", nullable: false),
                    transactionPinBlocked = table.Column<bool>(type: "bit", nullable: false),
                    transactionPinBlockedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isLPinSet = table.Column<bool>(type: "bit", nullable: false),
                    loginPin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    loginPinCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    loginPinUpdateAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    wrongLPinCount = table.Column<int>(type: "int", nullable: false),
                    loginPinBlocked = table.Column<bool>(type: "bit", nullable: false),
                    loginPinBlockedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customerAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    addressCreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    addressUpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    addressUpdateCount = table.Column<int>(type: "int", nullable: false),
                    customerCountryId = table.Column<int>(type: "int", nullable: false),
                    customerStateId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Otps",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    otp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    createdAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    otpType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Otps", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_customerPhoneNumber",
                table: "Customers",
                column: "customerPhoneNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_id",
                table: "Customers",
                column: "id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Otps");
        }
    }
}
