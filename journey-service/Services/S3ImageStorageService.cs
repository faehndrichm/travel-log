using Amazon.S3;
using Amazon.S3.Model;

namespace journey_service.Services;

public class S3ImageStorageService
{
    private readonly IAmazonS3 _s3Client;
    private readonly IAmazonS3 _externalS3Client;
    private readonly string _bucketName = "journey-stop-images";

    public S3ImageStorageService(IAmazonS3 s3Client, IAmazonS3 s3ExternalClient)
    {
        _s3Client = s3Client;
        _externalS3Client = s3ExternalClient;
    }

    public async Task<string> UploadImageAsync(string journeyId, IFormFile file)
    {
        var key = $"journey-{journeyId}/{Guid.NewGuid()}.jpg";
        using var stream = file.OpenReadStream();

        await _s3Client.PutObjectAsync(new PutObjectRequest
        {
            BucketName = _bucketName,
            Key = key,
            InputStream = stream
        });

        var urlRequest = new GetPreSignedUrlRequest
        {
            BucketName = _bucketName,
            Key = key,
            Expires = DateTime.UtcNow.AddMinutes(10),
            Protocol = Protocol.HTTP, // TODO: Change to HTTPS in production
        };

        return _externalS3Client.GetPreSignedURL(urlRequest);
    }
}