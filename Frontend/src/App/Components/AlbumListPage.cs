using App.Framework;
using App.Models;
using Domain.Api;
using static App.Framework.WebApp;
using static App.Framework.Hooks;
using static App.Framework.Tags;

namespace App.Components;

public partial class AlbumListPage(Action<string> setPage, Action<long> setAlbumId) : IComponent
{
    public VNode Render()
    {
        var client = UseContext(App.ClientCtx).SignalR();
        var (albums, setAlbums) = UseState<AlbumModel[]>([]);
        var (categories, setCategories) = UseState<CategoryModel[]>([]);
        var (loading, setLoading) = UseState(true);
        var (activeCat, setActiveCat) = UseState(0L);

        UseEffect(() => { _ = Load(); return; }, [activeCat]);

        async Task Load()
        {
            setLoading(true);
            try
            {
                var cats = await client.GetCategoriesAsync();
                setCategories(cats.Select(c => (CategoryModel)c).ToArray());
                var list = await client.GetAlbumsAsync(
                    categoryId: activeCat > 0 ? activeCat : null);
                // TODO: Implement pagination / infinite scroll for large datasets
                setAlbums(list.Select(a => (AlbumModel)a).ToArray());
            }
            catch { /* TODO: Show error feedback to user */ }
            finally { setLoading(false); }
        }

        var chips = new List<VNode> {
            button(Css.chip + (activeCat == 0 ? " " + Css.active : ""),
                () => setActiveCat(0), txt("All"))
        };
        foreach (var c in categories)
        {
            var id = c.Id;
            chips.Add(button(Css.chip + (activeCat == id ? " " + Css.active : ""),
                () => setActiveCat(id), txt(c.Name)));
        }

        return div(
            div(Css.chip_row, chips.ToArray()),
            loading
                ? div("loading", span(txt("Loading albums...")))
                // TODO: Add album creation UI (create, edit, delete)
                : div(Css.card_grid,
                    albums.Length == 0
                        ? [p("empty", txt("No albums yet."))]
                        : albums.Select(a => Card(a)).ToArray()));
    }

    private VNode Card(AlbumModel a)
        => div(new Props("md-card").OnClick(() => { setAlbumId(a.Id); setPage("album"); }),
            div("md-card-body",
                h3(txt(a.Title)),
                p(txt(a.Description.Length > 120
                    ? a.Description[..120] + "..." : a.Description)),
                div("md-card-meta",
                    span(txt($"By {a.AuthorName}")),
                    span(txt($"♡ {a.SubscribeCount}")),
                    span(txt(AccessIcon(a.AccessLevel))))));

    private static string AccessIcon(AccessLevel l) => l switch
    {
        AccessLevel.Private => "🔒",
        AccessLevel.AuthReadOnly => "👁",
        AccessLevel.AuthReadWrite => "✏️",
        AccessLevel.PublicReadOnly => "🌐",
        AccessLevel.PublicReadWrite => "🌐",
        _ => "",
    };
}
