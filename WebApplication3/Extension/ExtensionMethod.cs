namespace WebApplication3.Extension
{
    public static class ExtensionMethod
    {
        public static bool CheckSize(this IFormFile file, int mb)
        {
            if(file.Length > mb * 1024 * 1024)
            {
                return false;
            }

            return true;
        }

        public static bool Checktype(this IFormFile file, string type="image")
        {
            return file.ContentType.Contains(type);
        }


        public static async Task<string> SaveFileAsync(this IFormFile file, string folderPath)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + file.FileName;

            string path = Path.Combine(folderPath, uniqueFileName);

            using FileStream fileStream = new(path, FileMode.Create);

            await file.CopyToAsync(fileStream);

            return uniqueFileName;

        } 

    }
}
