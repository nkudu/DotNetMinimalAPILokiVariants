using Microsoft.EntityFrameworkCore;

namespace LokiVariants
{
    public class VariantService : IVariantService
    {
        public readonly ApiContext _context;

        public VariantService(ApiContext context)
        {
            _context = context;
        }

        public async Task<IResult> GetVariants()
        {
            return Results.Ok(await _context.Variants.ToListAsync());
        }

        public async Task<IResult> GetVariantById(int id)
        {
            var Variant = await _context.Variants.FindAsync(id);

            return Variant != null ? Results.Ok(Variant) : Results.NotFound();
        }

        public async Task<IResult> CreateVariant(VariantRequest Variant)
        {
            var createdVariant = _context.Variants.Add(new Variant
            {
                Name = Variant.Name ?? string.Empty,
                Type = Variant.Type ?? string.Empty,
                CreatedAt = DateTime.Now,
            });

            await _context.SaveChangesAsync();

            return Results.Created($"/Variants/{createdVariant.Entity.Id}", createdVariant.Entity);
        }

        public async Task<IResult> UpdateVariant(int id, VariantRequest Variant)
        {
            var VariantToUpdate = await _context.Variants.FindAsync(id);

            if (VariantToUpdate == null)
            {
                return Results.NotFound();
            }

            if (Variant.Name != null)
            {
                VariantToUpdate.Name = Variant.Name;
            }

            if (Variant.Type != null)
            {
                VariantToUpdate.Type = Variant.Type;
            }

            await _context.SaveChangesAsync();

            return Results.Ok(VariantToUpdate);
        }

        public async Task<IResult> DeleteVariant(int id)
        {
            var Variant = await _context.Variants.FindAsync(id);

            if (Variant == null)
            {
                return Results.NotFound();
            }

            _context.Variants.Remove(Variant);

            await _context.SaveChangesAsync();

            return Results.NoContent();
        }
    }
}
