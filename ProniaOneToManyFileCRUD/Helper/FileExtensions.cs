namespace ProniaOneToManyFileCRUD.Helper
{
    public static class FileExtensions
    {
        public static string CreateFile(this IFormFile file, string webRoot, string folderName)
        {
            string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName); // faylin adina guide elave edirki eyni adli fayllar qarismasin

            string fullPath = Path.Combine(webRoot, folderName, filename);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create)) // Fayli foldere yazir
            {
                file.CopyTo(stream);
            }

            return filename; 
        }
    }
}
