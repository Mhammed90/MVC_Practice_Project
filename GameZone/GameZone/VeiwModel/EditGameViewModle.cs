using GameZone.Attributes;

namespace GameZone.VeiwModel
{
    public class EditGameViewModle:BaseViewModle
    {
        public int Id { get; set; }

        public string? CurrentCover { get; set; }

        [AllowedExtentions(FileSettings.AllowedExtentions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile? Cover { get; set; } = default!;
    }
}
