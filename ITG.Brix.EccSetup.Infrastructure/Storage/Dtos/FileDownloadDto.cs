namespace ITG.Brix.EccSetup.Infrastructure.Storage.Dtos
{
    public class FileDownloadDto
    {
        public string Name { get; set; }
        public string ContentType { get; set; }
        public byte[] FileStream { get; set; }
    }
}
