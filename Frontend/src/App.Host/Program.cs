using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Net.Http.Headers;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = AppContext.BaseDirectory,
    WebRootPath = Path.Combine(AppContext.BaseDirectory, "wwwroot")
});

var app = builder.Build();

var contentTypeProvider = new FileExtensionContentTypeProvider();
contentTypeProvider.Mappings[".dll"] = MediaTypeNames.Application.Octet;
contentTypeProvider.Mappings[".webcil"] = MediaTypeNames.Application.Octet;
contentTypeProvider.Mappings[".pdb"] = MediaTypeNames.Application.Octet;
contentTypeProvider.Mappings[".dat"] = MediaTypeNames.Application.Octet;
contentTypeProvider.Mappings[".blat"] = MediaTypeNames.Application.Octet;
contentTypeProvider.Mappings[".wasm"] = "application/wasm";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = contentTypeProvider,
    OnPrepareResponse = ctx =>
    {
        var path = ctx.Context.Request.Path.Value ?? "";

        if (path.Contains("/_framework/"))
        {
            ctx.Context.Response.Headers[HeaderNames.CacheControl] = "public,max-age=31536000,immutable";
        }

        var extension = Path.GetExtension(path);
        if (extension == ".gz" || extension == ".br")
        {
            ctx.Context.Response.Headers[HeaderNames.ContentEncoding] = extension == ".br" ? "br" : "gzip";

            var fileNameWithoutCompression = Path.GetFileNameWithoutExtension(path);
            if (contentTypeProvider.TryGetContentType(fileNameWithoutCompression, out var originalMime))
            {
                ctx.Context.Response.ContentType = originalMime;
            }
        }
    }
});

app.MapFallbackToFile("index.html");
app.Run();
