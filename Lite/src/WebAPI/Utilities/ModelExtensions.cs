using Domain.Entity;
using Domain.Shared;
using Storage;

namespace WebAPI.Utilities;

internal static class ImageFileExtension
{
    extension(ImageFile file)
    {
        public string DownloadName
        {
            get
            {
                string filename = Path.GetFileName(file.Value);
                return Path.ChangeExtension(filename, file.Extension);
            }
        }
    }
}

public static class IFormFileExtensions
{
    extension(IFormFile file)
    {
        public async Task<ImageFile> GetAsync(CancellationToken cancellationToken = default)
        {
            string temp = Path.GetTempFileName();
            await using var stream = new FileStream(temp, FileMode.OpenOrCreate);
            await file.CopyToAsync(stream, cancellationToken);
            return new ImageFile() { Value = temp };
        }
    }
}

public static class ModelBindExtensions
{
    extension<TObject, TValue>(TObject)
        where TObject : IValueObject<TObject, TValue>, IFactoryConstructor<TObject, TValue>
    {
        public static TObject Bind(TValue value)
        {
            if (TObject.TryCreateNew(value, out var model) == false)
            {
                throw new DomainModelInvalidException(value?.ToString());
            }
            return model;
        }
    }
}
