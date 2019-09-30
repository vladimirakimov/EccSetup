using ITG.Brix.EccSetup.Infrastructure.Exceptions;
using ITG.Brix.EccSetup.Infrastructure.Storage.Config;
using ITG.Brix.EccSetup.Infrastructure.Storage.Dtos;
using Microsoft.Extensions.Options;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Blob.Protocol;
using System.IO;
using System.Threading.Tasks;

namespace ITG.Brix.EccSetup.Infrastructure.Storage.Impl
{
    public class BlobStorage : IBlobStorage
    {
        private readonly StorageConfiguration _storageConfiguration;

        public BlobStorage(IOptions<StorageConfiguration> storageConfiguration)
        {
            _storageConfiguration = storageConfiguration.Value;
        }

        public async Task<FileDownloadDto> DownloadFileAsync(string fileName)
        {
            var file = new FileDownloadDto();

            try
            {
                var container = await GetContainer();
                var blob = container.GetBlockBlobReference(fileName);

                using (var stream = new MemoryStream())
                {
                    await blob.DownloadToStreamAsync(stream);
                    file.FileStream = stream.ToArray();
                }

                await blob.FetchAttributesAsync();
                var contentType = blob.Properties.ContentType;
                file.ContentType = contentType;
                file.Name = fileName;

                return file;
            }
            catch (StorageException ex)
            {
                if (ex.RequestInformation.ErrorCode == BlobErrorCodeStrings.BlobNotFound)
                {
                    throw new BlobNotFoundException(ex);
                }
                throw new GenericBlobStorageException(ex);
            }
        }

        public async Task<string> UploadFileToStorage(Stream fileStream, string fileName)
        {
            try
            {
                var container = await GetContainer();
                var blob = container.GetBlockBlobReference(fileName);
                await blob.UploadFromStreamAsync(fileStream);

                return blob.Name;
            }
            catch (StorageException ex)
            {
                if (ex.RequestInformation.ErrorCode == BlobErrorCodeStrings.BlobAlreadyExists)
                {
                    throw new UniqueFileNameException(ex);
                }
                throw new GenericBlobStorageException(ex);
            }
        }

        private async Task<CloudBlobContainer> GetContainer()
        {
            CloudStorageAccount storageAccount;
            var storageConnectionString = _storageConfiguration.StorageConnectionString;

            if (CloudStorageAccount.TryParse(storageConnectionString, out storageAccount))
            {
                var cloudBlobClient = storageAccount.CreateCloudBlobClient();
                var container = cloudBlobClient.GetContainerReference("eorder");
                await container.CreateIfNotExistsAsync();
                return container;
            }
            else
            {
                return null;
            }
        }
    }
}
