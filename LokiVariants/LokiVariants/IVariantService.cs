namespace LokiVariants
{
    public interface IVariantService
    {
        Task<IResult> GetVariants();

        Task<IResult> GetVariantById(int id);

        Task<IResult> CreateVariant(VariantRequest Variant);

        Task<IResult> UpdateVariant(int id, VariantRequest Variant);

        Task<IResult> DeleteVariant(int id);
    }
}
