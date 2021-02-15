using FloodFinder.Core.Entities;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FloodFinder.Infrastructure.Persistence
{
  public static class SeedData
  {
    public static void SeedCounties(this MigrationBuilder migrationBuilder)
    {
      migrationBuilder.InsertData(
        table: nameof(County),
        columns: new[] { nameof(County.Name) },
        values: new object[,]
        {
          {"Buckinghamshire"},{"Cambridgeshire"},{"Cheshire"},{"Cleveland"},{"Cornwall"},{"Cumbria"},{"Derbyshire"},{"Devon"},{"Dorset"},{"Durham"},{"East Sussex"},{"Essex"},{"Gloucestershire"},
          {"Greater London"},{"Greater Manchester"},{"Hampshire"},{"Hertfordshire"},{"Kent"},{"Lancashire"},{"Leicestershire"},{"Lincolnshire"},{"Merseyside"},{"Norfolk"},{"North Yorkshire"},
          {"Northamptonshire"},{"Northumberland"},{"Nottinghamshire"},{"Oxfordshire"},{"Shropshire"},{"Somerset"},{"South Yorkshire"},{"Staffordshire"},{"Suffolk"},{"Surrey"},{"Tyne and Wear"},
          {"Warwickshire"},{"West Berkshire"},{"West Midlands"},{"West Sussex"},{"West Yorkshire"},{"Wiltshire"},{"Worcestershire"}
        });
    }
  }
}