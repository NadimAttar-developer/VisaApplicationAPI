
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VisaApplication.Model.Applicant;
using VisaApplication.Model.Content;
using VisaApplication.Model.Security;

namespace VisaApplication.SqlServer.DataBase;
public class VisaApplicationDbContext :
    IdentityDbContext<VisaApplicationUser,
        VisaApplicationRole,
        Guid,
        VisaApplicationUserClaim,
        VisaApplicationUserRole,
        VisaApplicationUserLogin,
        VisaApplicationRoleClaim,
        VisaApplicationUserToken>
{
    #region Constructor
    public VisaApplicationDbContext(
        DbContextOptions<VisaApplicationDbContext> Options) : base(Options)
    { }
    #endregion

    #region Content
    public DbSet<FileSet> Files { get; set; }
    public DbSet<DocumentFileSet> Documents { get; set; }
    #endregion

    #region VisaApplication
    public DbSet<ApplicantSet> Applicants { get; set; }
    public DbSet<VisaTypeSet> VisaTypes { get; set; }
    #endregion

    #region Methods
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        foreach (var mutableForeignKey in builder.Model
            .GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            mutableForeignKey.DeleteBehavior = DeleteBehavior.Restrict;
        }
        builder.Entity<ApplicantSet>().Property(e => e.Id).HasDefaultValueSql("NEWID()");
        builder.Entity<VisaTypeSet>().Property(e => e.Id).HasDefaultValueSql("NEWID()");
        builder.Entity<FileSet>().Property(e => e.Id).HasDefaultValueSql("NEWID()");
        builder.Entity<VisaApplicationUser>().ToTable("VisaApplicationUsers", "Security");
        builder.Entity<VisaApplicationRole>().ToTable("VisaApplicationRoles", "Security");
        builder.Entity<VisaApplicationUserClaim>().ToTable("VisaApplicationUserClaims", "Security");
        builder.Entity<VisaApplicationUserRole>().ToTable("VisaApplicationUserRoles", "Security");
        builder.Entity<VisaApplicationUserLogin>().ToTable("VisaApplicationUserLogins", "Security");
        builder.Entity<VisaApplicationRoleClaim>().ToTable("VisaApplicationRoleClaims", "Security");
        builder.Entity<VisaApplicationUserToken>().ToTable("VisaApplicationUserTokens", "Security");
    }
    #endregion

}
