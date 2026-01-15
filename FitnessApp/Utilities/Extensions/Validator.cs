namespace FitnessApp.Utilities.Extensions
{
    public static class Validator
    {
        public async static Task<string> CreateFileAsync(this IFormFile formFile, params string[] roots)
        {
            string fileName = string.Concat(Guid.NewGuid().ToString(), formFile.FileName);
            string path = string.Empty;
            for(int i  = 0; i < roots.Length; i++)
            {
                path = Path.Combine(path, roots[i]);

            }
            path = Path.Combine(path, fileName);
            using (FileStream fileStream = new(path, FileMode.Create))
            {
                await formFile.CopyToAsync(fileStream);
            }
            return fileName;
        }
    }
}
