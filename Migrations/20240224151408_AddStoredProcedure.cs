using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace srsProject.Migrations
{
    /// <inheritdoc />
    public partial class AddStoredProcedure : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
migrationBuilder.Sql(
         @"
        -- ================================================
        -- Template generated from Template Explorer using:
        -- Create Procedure (New Menu).SQL
        --
        -- Use the Specify Values for Template Parameters 
        -- command (Ctrl-Shift-M) to fill in the parameter 
        -- values below.
        --
        -- This block of comments will not be included in
        -- the definition of the procedure.
        -- ================================================
        SET ANSI_NULLS ON
        GO
        SET QUOTED_IDENTIFIER ON
        GO
        -- =============================================
        -- Author:        Tamás Olasz
        -- Create date:   2024.02.24
        -- Description:   Calls all owned cars by Owner ID
        -- =============================================
        CREATE PROCEDURE GetCarsByUserId 
            -- Add the parameters for the stored procedure here
            @OwnerId INT
        AS
        BEGIN
            -- SET NOCOUNT ON added to prevent extra result sets from
            -- interfering with SELECT statements.
            SET NOCOUNT ON;

            -- Insert statements for procedure here
            SELECT Cars.* 
            FROM Cars
            INNER JOIN Ownerships ON Cars.Id = Ownerships.CarId
            WHERE Ownerships.OwnerId = @OwnerId;
        END
        GO
        ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
