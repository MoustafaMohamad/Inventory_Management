using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using static Inventory_Management.Common.Helpers.ICloudinaryService;

namespace Inventory_Management.Common.Helpers
{
    public interface ICloudinaryService
    {
        Task<ImageUploadResult> UploadImageAsync(IFormFile file);
    }
    public class CloudinaryService: ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        private readonly IConfiguration _config;

        public CloudinaryService(IConfiguration configuration)
        {
            _config= configuration;
            var account = new Account(
                _config.GetSection("CloudinarySettings:CLOUD_NAME").Value,//Environment.GetEnvironmentVariable("CLOUD_NAME"),
                _config.GetSection("CloudinarySettings:API_KEY").Value,//Environment.GetEnvironmentVariable("API_KEY"),
                _config.GetSection("CloudinarySettings:API_SECRET").Value//Environment.GetEnvironmentVariable("API_SECRET")
            );

            _cloudinary = new Cloudinary(account);
        }
        public async Task<ImageUploadResult> UploadImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("File cannot be null or empty.", nameof(file));
            }

            using (var stream = file.OpenReadStream())
            {
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    PublicId = Path.GetFileNameWithoutExtension(file.FileName)
                };

                var uploadResult = await _cloudinary.UploadAsync(uploadParams);

                if (uploadResult.Error != null)
                {
                    throw new Exception($"Upload failed: {uploadResult.Error.Message}");
                }

                // Check if URL is not null
                if (string.IsNullOrEmpty(uploadResult.Url?.ToString()))
                {
                    throw new Exception("Upload succeeded, but URL is null.");
                }

                return uploadResult;
            }
        }
     }
 }

