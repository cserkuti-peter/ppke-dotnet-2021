// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using RecipeBookApi.Models;

namespace RecipeBookApi.Migrations
{
    [DbContext(typeof(RecipeBookContext))]
    [Migration("20211125143151_seed")]
    partial class seed
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("RecipeBookApi.Models.Recipe", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CookTimeMinutes")
                        .HasColumnType("int");

                    b.Property<string>("Ingredients")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Method")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<double>("RatingsAvg")
                        .HasColumnType("float");

                    b.Property<int>("Servers")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Recipes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CookTimeMinutes = 120,
                            Ingredients = "Eggs, ...",
                            Method = "...",
                            Name = "Apple pie",
                            RatingsAvg = 0.0,
                            Servers = 10
                        },
                        new
                        {
                            Id = 2,
                            CookTimeMinutes = 20,
                            Ingredients = "Rice, ...",
                            Method = "...",
                            Name = "Sushi",
                            RatingsAvg = 0.0,
                            Servers = 10
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
