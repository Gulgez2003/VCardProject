namespace VCardProject.Interfaces
{
    public interface IVCardService
    {
        public Task<List<string>> ConvertObjectToVCardAsync();
    }
}
