namespace GameZone.Attributes
{
    public class MaxFileSizeAttribute:ValidationAttribute
    { 
        private readonly int _maxFileSize;
        public MaxFileSizeAttribute(int maxFileSize)
        {

            _maxFileSize = maxFileSize;
        }
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var file = value as IFormFile;
            if (file != null)
            { 
                if(file.Length <= _maxFileSize)
                {
                    return ValidationResult.Success;
                }
            }
            return new ValidationResult($"File Size must be less than {FileSettings.MaxFileSizeInMB}MB");
        } 
       
    }  

}
