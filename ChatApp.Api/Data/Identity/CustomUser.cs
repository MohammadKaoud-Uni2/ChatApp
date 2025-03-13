using ChatApp.Api.Data.Helper;
using ChatApp.Api.Model;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ChatApp.Api.Data.Identity
{
    public class CustomUser:IdentityUser
    {
        public DateTime DateBirth { get; set; }
        public DateTime ?Created {  get; set; }
        public DateTime ?LastActive { get; set; }
       
        public string ?KnownAs { get; set; }
       
        public string? Gender { get; set; }
     
        public string ?LookingFor { get; set; }
       
        public string  ?Interests { get; set; }
        public virtual ICollection<Photo>Photos { get; set; }
        public virtual ICollection<CustomUser>LikedUsers { get; set; }
        public virtual ICollection<CustomUser>LikedByUser { get; set; }
        public virtual ICollection<Message> MessageRecived { get; set; }
        public virtual ICollection<Message> MessagesSent  { get; set; }

        public string ?Introduction {  get; set; }
        
        public string ?City { get; set; }
        
        public string ?Country { get; set; }

        public int GetUserAge()
        {
            return this.DateBirth.CalculateAge();
        }
    }
}
