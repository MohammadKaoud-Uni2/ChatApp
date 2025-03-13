using ChatApp.Api.Data;
using ChatApp.Api.Data.Helper;
using ChatApp.Api.Model;
using ChatApp.Api.Repos.Abstract;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Identity.Client;
using System.Runtime.InteropServices;

namespace ChatApp.Api.Repos.Implementation
{
    public class PhotoService :GenericRepo<Photo>, IPhotoService
    {
        private readonly  IConfiguration _configuration;
       
        public Cloudinary _cloudinary { get; set; }
        private readonly ApplicationDbContext _context;
        public PhotoService(IConfiguration configuration,ApplicationDbContext context):base(context)
        {
            _context = context;
            _configuration = configuration;
            var  _cloudinarySettings=new CloudinarySettings();
            _configuration.GetSection("CloudinarySettings").Bind(_cloudinarySettings);
            Account account = new Account(_cloudinarySettings.CloudName, _cloudinarySettings.ApiKey, _cloudinarySettings.ApiSecret);

            _cloudinary=new Cloudinary(account);
        
           
        }

        public  async Task<ImageUploadResult> UploadImage(IFormFile file)
        {
            var stream=file.OpenReadStream();
            if (file.Length > 0) {
                var uploadParams = new ImageUploadParams()
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                
                var uploadResult = await _cloudinary.UploadAsync(uploadParams);
                if (uploadResult != null)
                {
                    return uploadResult;
                }
                else
                {
                    throw new Exception("there Was Problem While return Resultof Uploading ");
                }
                

            }
            else
            {
                throw new Exception("Not Correcct Uploading ");
            }
           
        }

        public async Task<DeletionResult> DeletePhoto(string  PublicId)
        {
            var deleteParams = new DeletionParams(PublicId);
            var resultofDelete=await _cloudinary.DestroyAsync(deleteParams);
            return resultofDelete;
        }

    }
}
