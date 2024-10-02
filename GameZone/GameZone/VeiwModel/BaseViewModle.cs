namespace GameZone.VeiwModel
{
    public class BaseViewModle
    {
        [Display(Name = "Game Name")]
        public string Name { get; set; } = string.Empty;
        [Display(Name = "Category")]
        [Required(ErrorMessage = "Gategory is Required.")]

        public int CategoryId { get; set; }

        public IEnumerable<SelectListItem> Categories { get; set; } = default!;

        [Display(Name = "Suported Device")]
        public List<int> SelectedDevices { get; set; } = default!;

        public IEnumerable<SelectListItem> Devices { get; set; } = default!;
        [MaxLength(2500)]
        [Required(ErrorMessage = "The Description Field is rquired.")]

        public string Description { get; set; } = string.Empty;

       
    }
}
