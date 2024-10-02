namespace GameZone.Attributes
{
    public class AllowedExtentionsAttribute:ValidationAttribute
    {
        private readonly string _allowedExtensions;
        public AllowedExtentionsAttribute(string allowedExtensions)
        {
            _allowedExtensions = allowedExtensions;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
           var file =value as IFormFile;
            if (file != null)
            {
                var exten = Path.GetExtension(file.FileName);
                if (_allowedExtensions.Split(',').Contains(exten, StringComparer.OrdinalIgnoreCase))
                {
                    return ValidationResult.Success;
                } 
               
            }
            return new ValidationResult($"only these extension {{ {_allowedExtensions} }} are allowed.");
        } 
    } 

}
