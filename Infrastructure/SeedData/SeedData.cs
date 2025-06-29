using Domain.Entities;
using Infrastructure.Implementions;

namespace Infrastructure.Seeding;

public static class SeedData
{
    public static async Task SeedPlaceCategoriesAsync(Context dbContext)
    {
        if (!dbContext.PlaceCategories.Any())
        {
            var categories = new List<PlaceCategory>
            {
                new PlaceCategory { Name = "تاریخی" },
                new PlaceCategory { Name = "تفریحی" },
                new PlaceCategory { Name = "فرهنگی" },
                new PlaceCategory { Name = "مذهبی" },
            };

            dbContext.PlaceCategories.AddRange(categories);
            await dbContext.SaveChangesAsync();
        }
    }
}
