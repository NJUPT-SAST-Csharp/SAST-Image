using App.Framework;
using App.Models;
using static App.Framework.WebApp;
using static App.Framework.Hooks;
using static App.Framework.Tags;

namespace App.Components;

public partial class ProfilePage(long userId, Action<string> setPage) : IComponent
{
    public VNode Render()
    {
        var signalR = UseContext(App.ClientCtx).SignalR();
        var (profile, setProfile) = UseState<UserProfileModel>(null!);
        var (loading, setLoading) = UseState(true);
        var (error, setError) = UseState("");

        UseEffect(() => { _ = Load(); return; }, [userId]);

        async Task Load()
        {
            setLoading(true);
            setError("");
            try
            {
                var p = await signalR.GetProfileAsync(userId > 0 ? userId : null);
                if (p is not null)
                    setProfile((UserProfileModel)p);
                else
                    setError("Profile not found.");
            }
            catch (Exception ex) { setError(ex.Message); }
            finally { setLoading(false); }
        }

        return div(
            div("page-header",
                button("md-btn text", () => setPage("albums"), txt("← Back")),
                h2(txt("Profile"))),
            loading
                ? div("loading", span(txt("Loading...")))
                : error.Length > 0
                    ? p("error", txt(error))
                    : profile is not null
                        ? div(new Props("md-card").Cls(Css.profile_card),
                            div(Css.avatar,
                                txt(profile.Nickname.Length > 0
                                    ? profile.Nickname[..1].ToUpper() : "?")),
                            div("md-card-body",
                                h3(txt(profile.Nickname)),
                                p(txt($"@{profile.Username}")),
                                p(txt(profile.Biography))))
                        : p("empty", txt("Profile not found.")));
    }
}
