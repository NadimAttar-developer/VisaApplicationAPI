
using VisaApplication.Model.Applicant;
using VisaApplication.Seeder.VisaTypeSeed.IData;
using VisaApplication.SqlServer.DataBase;
using VisaApplicationBase.DbContext;

namespace VisaApplication.Seeder.VisaTypeSeed.Data;
public class VisaTypeCreationService : VisaApplicationRepository,
    IVisaTypeCreationService
{
    #region Constructor
    public VisaTypeCreationService(
        VisaApplicationDbContext context) : base(context)
    {}
    #endregion
    public async Task CreateVisaTypesAsync()
    {
        var visaTypesEntities = new List<VisaTypeSet>();

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "Employment visa"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "Golden Visa"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "Tourist visa"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "Transit visa"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "UAE family visa"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "Student visa"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "UAE student visa"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "Retirement visa"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "GCC visa for UAE residents"
        });

        visaTypesEntities.Add(new VisaTypeSet
        {
            Id = Guid.NewGuid(),
            Name = "Medical visa"
        });

        await _context.AddRangeAsync(visaTypesEntities);
        await _context.SaveChangesAsync();
    }
}
