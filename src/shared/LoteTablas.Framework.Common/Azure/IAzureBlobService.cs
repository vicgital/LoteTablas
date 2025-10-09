namespace LoteTablas.Framework.Common.Azure
{
    public interface IAzureBlobService
    {
        Task<Stream> GetBlob(string container, string blobName);
        Task DeleteBlob(string container, string blobName);
        Task<bool> ExistsBlob(string container, string blobName);
        Task<bool> MoveBlob(string containerFrom, string blobName, string containerTo);
        Task UploadBlob(string container, string blobName, Stream blob);

    }
}
