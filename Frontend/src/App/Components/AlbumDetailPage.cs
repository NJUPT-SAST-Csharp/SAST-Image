using App.Framework;
using App.Models;
using Client.SignalR;
using static App.Framework.WebApp;
using static App.Framework.Hooks;
using static App.Framework.Tags;

namespace App.Components;

public partial class AlbumDetailPage(long albumId, Action<string> setPage, Action<long> setUserId) : IComponent
{
    public VNode Render()
    {
        var sastClient = UseContext(App.ClientCtx);
        var signalR = sastClient.SignalR();
        var baseUrl = sastClient.Options.BaseUrl.TrimEnd('/');
        var (images, setImages) = UseState<ImageModel[]>([]);
        var (loading, setLoading) = UseState(true);

        UseEffect(() => { _ = Load(); return; }, [albumId]);

        async Task Load()
        {
            setLoading(true);
            try
            {
                var list = await signalR.GetImagesAsync(albumId: albumId);
                // TODO: Implement pagination / infinite scroll for large datasets
                setImages(list.Select(i => (ImageModel)i).ToArray());
            }
            catch { /* TODO: Show error feedback to user */ }
            finally { setLoading(false); }
        }

        return div(
            div("page-header",
                button("md-btn text", () => setPage("albums"), txt("← Albums")),
                h2(txt($"{images.Length} images"))),
            loading
                ? div("loading", span(txt("Loading...")))
                // TODO: Add image upload UI
                : div(Css.image_grid,
                    images.Select(x =>
                        ImageCard(x, baseUrl, setUserId, setPage, signalR)).ToArray()));
    }

    private static VNode ImageCard(
        ImageModel image, string baseUrl, Action<long> setUserId,
        Action<string> setPage, SignalRClient signalR)
    {
        var src = image.ThumbnailUrl is not null ? $"{baseUrl}{image.ThumbnailUrl}" : null;
        return div(new Props("md-card").Cls(Css.image_card),
            src is not null
                ? Tags.img(src, image.Title)
                : div(Css.img_fallback, txt("🖼")),
            div("md-card-body",
                h4(txt(image.Title)),
                div("md-card-meta",
                    button("md-btn text sm", () =>
                    {
                        setUserId(image.UploaderId);
                        setPage("profile");
                    }, txt("Uploader")))));
    }
}
