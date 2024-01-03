namespace VCardProject.Implementations
{
    public class VCardService : IVCardService
    {
        public async Task<List<string>> ConvertObjectToVCardAsync()
        {
            // VCard'ların sayının daxil edilməsi üçün 'count' dəyişəni təyin edirik
            Console.Write("Enter the count of VCards you want to create : ");
            int count = int.Parse(Console.ReadLine());

            // Yaradılmış faylların adlarını saxlamaq üçün kolleksiya
            List<string> createdFiles = new List<string>();

            // URL RandomUser.me API
            string apiUrl = "https://randomuser.me/api/";

            for (int i = 1; i <= count; i++)
            {
                // HttpClient nümunəsi yaradılması
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        // API'yə GET-sorğu göndərilməsi
                        HttpResponseMessage response = await client.GetAsync(apiUrl);

                        // Sorğununun uğurlu olub olmadığının yoxlanılması
                        if (response.IsSuccessStatusCode)
                        {
                            // Cavabın 'string' kimi alınması
                            string jsonResult = await response.Content.ReadAsStringAsync();

                            VCardData userData = JsonConvert.DeserializeObject<VCardData>(jsonResult);

                            // İstifadəçi məlumatlarının qəbul edildiyini və nəticələrin siyahısının boş olmadığının yoxlanılması
                            if (userData != null && userData.VCardResults != null && userData.VCardResults.Count > 0)
                            {
                                // Nəticələr siyahısından ilk vCardın götürülməsi və "user" obyektində saxlanması
                                VCard user = userData.VCardResults[0];
                                // İstifadəçi məlumatlarından istifadə edərək vCard formatında sətir yaraılması
                                string vCard = "BEGIN:VCARD\n" +
                                               "VERSION:3.0\n" +
                                               $"FN:{user.Name.Firstname} {user.Name.Surname}\n" +
                                               $"N:{user.Name.Surname};{user.Name.Firstname}\n" +
                                               $"EMAIL:{user.Email}\n" +
                                               $"TEL:{user.Phone}\n" +
                                               $"ADR:;;{user.Location.City};;{user.Location.Country}\n" +
                                               "END:VCARD";
                                // Unikal fayl adının alınması
                                string uniqueFileName = GetUniqueFileName();

                                // vCard sətirinin fayla yazılması
                                await WriteVCardToFileAsync(vCard, uniqueFileName);

                                // Добавить имя созданного файла в коллекцию
                                createdFiles.Add(uniqueFileName);
                            }
                            else
                            {
                                // Default vCard 'string'i qaytarılması
                                createdFiles.Add("BEGIN:VCARD\nEND:VCARD");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Data request error: {response.StatusCode}");
                            // Default vCard 'string'i qaytarılması
                            createdFiles.Add("BEGIN:VCARD\nEND:VCARD");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"There was an error: {ex.Message}");
                        // Default vCard 'string'i qaytarılması
                        createdFiles.Add("BEGIN:VCARD\nEND:VCARD");
                    }
                }
            }
            // Вернуть список созданных файлов
            return createdFiles;
        }

        // Göstərilən ada malik fayla vCard formatında sətri asinxron yazan metod
        private async Task WriteVCardToFileAsync(string vCard, string fileName)
        {
            try
            {
                // vCard sətirinin .vcf uzantılı fayla yazılması
                await File.WriteAllTextAsync(fileName, vCard);

                Console.WriteLine($"The {fileName} file has been successfully created.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error writing to file: {ex.Message}");
                throw; 
            }
        }

        // Unikal fayl adı yaradan metod
        private string GetUniqueFileName()
        {
            // Müvəqqəti qovluqda unikal fayl adının yaradılması
            return Path.Combine(Path.GetTempPath(), Guid.NewGuid().ToString("N") + ".vcf");
        }
    }
}
