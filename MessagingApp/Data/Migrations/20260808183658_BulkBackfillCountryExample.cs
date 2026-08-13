using System.Reflection;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessagingApp.Data.Migrations
{
    /// <inheritdoc />
    // Example of a bulk, set-based backfill: derives UserProfiles.Country
    // from the PhoneNumber area code instead of hardcoding per-row values
    // (see AddCountryColumn's UpdateData calls for the small-scale version
    // of the same problem). Safe to re-run - the script only touches rows
    // where Country is still unset.
    public partial class BulkBackfillCountryExample : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var assembly = Assembly.GetExecutingAssembly();
            using var stream = assembly.GetManifestResourceStream(
                "MessagingApp.Data.Migrations.Scripts.BackfillCountry.sql");
            using var reader = new StreamReader(stream!);
            var sql = reader.ReadToEnd();

            // suppressTransaction: true so each WHILE-loop batch in the
            // script commits as it goes, rather than being held open under
            // one long migration transaction spanning the whole backfill.
            migrationBuilder.Sql(sql, suppressTransaction: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Data-only change - nothing to structurally revert. Rolling
            // back would mean re-nulling Country, which isn't meaningful
            // here, so Down is intentionally a no-op.
        }
    }
}
