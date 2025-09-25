using Amazon.S3;

public class S3ClientFactory
{

    public S3ClientFactory()
    {

    }

    public IAmazonS3 CreateClient()
    {
        var config = new AmazonS3Config
        {
            ServiceURL = "http://minio:9000", // MinIO endpoint
            ForcePathStyle = true,                // must be true for MinIO
            AuthenticationRegion = "eu-central-1",

        };

        return new AmazonS3Client(
            "root",          // MINIO_ROOT_USER
            "password123",   // MINIO_ROOT_PASSWORD
            config
        );
    }
    
    public IAmazonS3 CreateExternalClient()
    {
        var config = new AmazonS3Config
        {
            ServiceURL = "http://localhost:9000", // reachable from outside
            ForcePathStyle = true,                // must be true for MinIO
            AuthenticationRegion = "eu-central-1",
            
        };

        return new AmazonS3Client(
            "root",          // MINIO_ROOT_USER
            "password123",   // MINIO_ROOT_PASSWORD
            config
        );
    }
}