using System.ComponentModel.DataAnnotations;
using NetVips;

namespace WebAPI.Utilities.Attributes;

[AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false)]
public sealed class FileValidator(long minSize, long maxSize) : ValidationAttribute
{
    public FileValidator(long maxSize)
        : this(0, maxSize) { }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value is null)
            return ValidationResult.Success;
        if (value is not IFormFile file)
            return new ValidationResult("No file is provided.");
        if (file.Length < minSize)
            return new ValidationResult("Too small file. Min " + minSize + " Bytes.");
        if (file.Length > maxSize)
            return new ValidationResult("Too large file. Max " + maxSize + " Bytes.");
        if (file.ContentType.Contains("image") == false)
            return new ValidationResult("Not supported file type.");
        using var stream = file.OpenReadStream();
        if (Image.FindLoadStream(stream) is null)
            return new ValidationResult("Not supported file type.");

        return ValidationResult.Success;
    }
}
