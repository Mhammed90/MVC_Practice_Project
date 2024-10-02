


namespace GameZone.VeiwModel
{
    public class CreateGameVeiwModle:BaseViewModle
    {
        [AllowedExtentions(FileSettings.AllowedExtentions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;

    }
}
