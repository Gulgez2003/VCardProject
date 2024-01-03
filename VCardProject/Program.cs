class Program
{
    static async Task Main()
    {
        // VCardService'dən instance yaradırıq
        IVCardService vCardService = new VCardService();

        // DeserializeJsonToVCardAsync metodunu çağırırıq
        vCardService.ConvertObjectToVCardAsync().Wait();
    }
}
