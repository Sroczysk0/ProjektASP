using Microsoft.EntityFrameworkCore;
using Projekt01.Models.Movies;
using WebApplication5.Models.Movies;

namespace WebApplication5.Data;

public partial class MoviesDbContext : DbContext
{
    public MoviesDbContext()
    {
    }

    public MoviesDbContext(DbContextOptions<MoviesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Country> Countries { get; set; }
    public virtual DbSet<Department> Departments { get; set; }
    public virtual DbSet<Gender> Genders { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Keyword> Keywords { get; set; }
    public virtual DbSet<Language> Languages { get; set; }
    public virtual DbSet<LanguageRole> LanguageRoles { get; set; }
    public virtual DbSet<Movie> Movies { get; set; }
    public virtual DbSet<MovieCast> MovieCasts { get; set; }
    public virtual DbSet<MovieCompany> MovieCompanies { get; set; }
    public virtual DbSet<MovieCrew> MovieCrews { get; set; }
    public virtual DbSet<MovieGenre> MovieGenres { get; set; }
    public virtual DbSet<MovieKeyword> MovieKeywords { get; set; }
    public virtual DbSet<MovieLanguage> MovieLanguages { get; set; }
    public virtual DbSet<Person> People { get; set; }
    public virtual DbSet<ProductionCompany> ProductionCompanies { get; set; }
    public virtual DbSet<ProductionCountry> ProductionCountries { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("country");

            entity.Property(e => e.CountryId).ValueGeneratedNever().HasColumnName("country_id");
            entity.Property(e => e.CountryIsoCode).HasColumnName("country_iso_code");
            entity.Property(e => e.CountryName).HasColumnName("country_name");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.ToTable("department");

            entity.Property(e => e.DepartmentId).ValueGeneratedNever().HasColumnName("department_id");
            entity.Property(e => e.DepartmentName).HasColumnName("department_name");
        });

        modelBuilder.Entity<Gender>(entity =>
        {
            entity.ToTable("gender");

            entity.Property(e => e.GenderId).ValueGeneratedNever().HasColumnName("gender_id");
            entity.Property(e => e.Gender1).HasColumnName("gender");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("genre");

            entity.Property(e => e.GenreId).ValueGeneratedNever().HasColumnName("genre_id");
            entity.Property(e => e.GenreName).HasColumnName("genre_name");
        });

        modelBuilder.Entity<Keyword>(entity =>
        {
            entity.ToTable("keyword");

            entity.Property(e => e.KeywordId).ValueGeneratedNever().HasColumnName("keyword_id");
            entity.Property(e => e.KeywordName).HasColumnName("keyword_name");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.ToTable("language");

            entity.Property(e => e.LanguageId).ValueGeneratedNever().HasColumnName("language_id");
            entity.Property(e => e.LanguageCode).HasColumnName("language_code");
            entity.Property(e => e.LanguageName).HasColumnName("language_name");
        });

        modelBuilder.Entity<LanguageRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("language_role");

            entity.Property(e => e.RoleId).ValueGeneratedNever().HasColumnName("role_id");
            entity.Property(e => e.LanguageRole1).HasColumnName("language_role");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.ToTable("movie");

            entity.HasKey(e => e.MovieId);

            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.Title).HasColumnName("title");
            entity.Property(e => e.Budget).HasColumnName("budget");
            entity.Property(e => e.Homepage).HasColumnName("homepage");
            entity.Property(e => e.Overview).HasColumnName("overview");
            entity.Property(e => e.Popularity).HasColumnName("popularity");
            entity.Property(e => e.ReleaseDate).HasColumnName("release_date");
            entity.Property(e => e.Revenue).HasColumnName("revenue");
            entity.Property(e => e.Runtime).HasColumnName("runtime");
            entity.Property(e => e.MovieStatus).HasColumnName("movie_status");
            entity.Property(e => e.Tagline).HasColumnName("tagline");
            entity.Property(e => e.VoteAverage).HasColumnName("vote_average"); 
            entity.Property(e => e.VoteCount).HasColumnName("vote_count");
        });

        modelBuilder.Entity<MovieCast>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("movie_cast");

            entity.Property(e => e.CastOrder).HasColumnName("cast_order");
            entity.Property(e => e.CharacterName).HasColumnName("character_name");
            entity.Property(e => e.GenderId).HasColumnName("gender_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");

            entity.HasOne(d => d.Gender).WithMany().HasForeignKey(d => d.GenderId);
            entity.HasOne(d => d.Movie).WithMany().HasForeignKey(d => d.MovieId);
            entity.HasOne(d => d.Person).WithMany().HasForeignKey(d => d.PersonId);
        });

        modelBuilder.Entity<MovieCompany>(entity =>
        {
            entity.HasKey(e => new { e.CompanyId, e.MovieId });
            entity.ToTable("movie_company");

            entity.Property(e => e.CompanyId).HasColumnName("company_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(d => d.Company).WithMany().HasForeignKey(d => d.CompanyId);
            entity.HasOne(d => d.Movie).WithMany().HasForeignKey(d => d.MovieId);
        });

        modelBuilder.Entity<MovieCrew>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("movie_crew");

            entity.Property(e => e.DepartmentId).HasColumnName("department_id");
            entity.Property(e => e.Job).HasColumnName("job");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");
            entity.Property(e => e.PersonId).HasColumnName("person_id");

            entity.HasOne(d => d.Department).WithMany().HasForeignKey(d => d.DepartmentId);
            entity.HasOne(d => d.Movie).WithMany().HasForeignKey(d => d.MovieId);
            entity.HasOne(d => d.Person).WithMany().HasForeignKey(d => d.PersonId);
        });

        modelBuilder.Entity<MovieGenre>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("movie_genres");

            entity.Property(e => e.GenreId).HasColumnName("genre_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(d => d.Genre).WithMany().HasForeignKey(d => d.GenreId);
            entity.HasOne(d => d.Movie).WithMany().HasForeignKey(d => d.MovieId);
        });

        modelBuilder.Entity<MovieKeyword>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("movie_keywords");

            entity.Property(e => e.KeywordId).HasColumnName("keyword_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(d => d.Keyword).WithMany().HasForeignKey(d => d.KeywordId);
            entity.HasOne(d => d.Movie).WithMany().HasForeignKey(d => d.MovieId);
        });

        modelBuilder.Entity<MovieLanguage>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("movie_languages");

            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.LanguageRoleId).HasColumnName("language_role_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(d => d.Language).WithMany().HasForeignKey(d => d.LanguageId);
            entity.HasOne(d => d.LanguageRole).WithMany().HasForeignKey(d => d.LanguageRoleId);
            entity.HasOne(d => d.Movie).WithMany().HasForeignKey(d => d.MovieId);
        });

        modelBuilder.Entity<Person>(entity =>
        {
            entity.ToTable("person");

            entity.Property(e => e.PersonId).ValueGeneratedNever().HasColumnName("person_id");
            entity.Property(e => e.PersonName).HasColumnName("person_name");
        });

        modelBuilder.Entity<ProductionCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId);
            entity.ToTable("production_company");

            entity.Property(e => e.CompanyId).ValueGeneratedNever().HasColumnName("company_id");
            entity.Property(e => e.CompanyName).HasColumnName("company_name");
        });

        modelBuilder.Entity<ProductionCountry>(entity =>
        {
            entity.HasNoKey();
            entity.ToTable("production_country");

            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.MovieId).HasColumnName("movie_id");

            entity.HasOne(d => d.Country).WithMany().HasForeignKey(d => d.CountryId);
            entity.HasOne(d => d.Movie).WithMany().HasForeignKey(d => d.MovieId);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
