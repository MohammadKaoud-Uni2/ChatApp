using ChatApp.Api.Data.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ChatApp.Api.Model
{
    public class Photo
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string ? PublicId { get; set; }
        public bool IsMain { get; set; }
        #region nav
        public virtual CustomUser CustomUser { get; set; }
        [ForeignKey(nameof(CustomUser.Id))]
        public string  CustomUserId { get; set; }
        #endregion
    }
}
