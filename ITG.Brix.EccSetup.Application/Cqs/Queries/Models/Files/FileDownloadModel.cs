namespace ITG.Brix.EccSetup.Application.Cqs.Queries.Models.Files
{
    public class FileDownloadModel
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] FileStream { get; set; }
    }
}
