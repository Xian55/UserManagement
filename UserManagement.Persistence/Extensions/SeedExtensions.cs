namespace UserManagement.Persistence.Extensions
{
    /// <summary>
    /// Contains the extension method for seeding the database with initial data.
    /// </summary>
    public static class SeedExtensions
    {
        public static void SeedDatabase(this UserMangementDbContext dbContext)
        {
            // TODO: 

            dbContext.SaveChanges();
        }
    }
}
