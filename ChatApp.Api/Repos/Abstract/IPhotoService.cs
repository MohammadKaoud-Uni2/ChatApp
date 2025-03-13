using ChatApp.Api.Model;
using CloudinaryDotNet.Actions;
    namespace ChatApp.Api.Repos.Abstract
{
    public interface IPhotoService:IGenericRepo<Photo>
    {
        public Task<ImageUploadResult> UploadImage(IFormFile file);
        public Task<DeletionResult>DeletePhoto(string  PublicId);  
        
        
    }
}
