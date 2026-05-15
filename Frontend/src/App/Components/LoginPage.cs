using System.Runtime.InteropServices.JavaScript;
using App.Framework;
using Client;
using Domain.Api;
using static App.Framework.WebApp;
using static App.Framework.Hooks;
using static App.Framework.Tags;

namespace App.Components;

public partial class LoginPage(SastClient client, Action onAuthenticated) : IComponent
{
    public VNode Render()
    {
        var (tab, setTab) = UseState("login");

        return div(Css.login_page,
            div(Css.login_card,
                div(Css.login_hero,
                    h1(txt("Sast Image")),
                    p(txt(tab == "login"
                        ? "Sign in to explore albums"
                        : "Create your account"))),

                TabSwitcher(tab, setTab),

                tab == "login" ? LoginForm() : RegisterForm(setTab),

                OAuthSection()));
    }

    private static VNode Field(string placeholder, string type, string value, Action<string> set) =>
        input(type, placeholder, value, (Action<JSObject>)(e =>
            set(e.GetPropertyAsJSObject("target")!.GetPropertyAsString("value") ?? "")));

    private static async Task AuthenticateAsync(
        SastClient c,
        Func<SastClient, Task> auth,
        Action onSuccess,
        Action<bool> setLoading,
        Action<string> setError)
    {
        setLoading(true);
        setError("");
        try
        {
            await auth(c);
            onSuccess();
        }
        catch (Exception ex) { setError(ex.Message); }
        finally { setLoading(false); }
    }

    private static VNode TabSwitcher(string tab, Action<string> setTab)
    {
        return div(Css.login_tabs,
            button(Css.login_tab + (tab == "login" ? " " + Css.active : ""),
                () => setTab("login"), txt("Login")),
            button(Css.login_tab + (tab == "register" ? " " + Css.active : ""),
                () => setTab("register"), txt("Register")));
    }

    private VNode LoginForm()
    {
        var (username, setUsername) = UseState("");
        var (password, setPassword) = UseState("");
        var (error, setError) = UseState("");
        var (loading, setLoading) = UseState(false);

        return div(Css.login_form,
            Field("Username", "text", username, setUsername),
            Field("Password", "password", password, setPassword),

            error.Length > 0
                ? p("error", txt(error))
                : txt(""),

            button("md-btn primary full", async () =>
            {
                if (loading) return;
                await AuthenticateAsync(client,
                    c => c.SignalR().LoginAsync(username, password),
                    onAuthenticated, setLoading, setError);
            }, txt(loading ? "Signing in..." : "Sign In")));
    }

    private VNode RegisterForm(Action<string> setTab)
    {
        var (username, setUsername) = UseState("");
        var (nickname, setNickname) = UseState("");
        var (biography, setBiography) = UseState("");
        var (password, setPassword) = UseState("");
        var (error, setError) = UseState("");
        var (loading, setLoading) = UseState(false);

        return div(Css.login_form,
            Field("Username", "text", username, setUsername),
            Field("Nickname", "text", nickname, setNickname),
            Field("Biography", "text", biography, setBiography),
            // TODO: Password field reserved for future server-side password support
            Field("Password (optional)", "password", password, setPassword),

            error.Length > 0
                ? p("error", txt(error))
                : txt(""),

            button("md-btn primary full", async () =>
            {
                if (loading) return;
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(nickname))
                {
                    setError("Username and nickname are required.");
                    return;
                }
                await AuthenticateAsync(client,
                    c => c.SignalR().RegisterAsync(new RegisterRequest(username, nickname, biography)),
                    onAuthenticated, setLoading, setError);
            }, txt(loading ? "Creating account..." : "Register")),

            p(Css.login_switch,
                span(txt("Already have an account? ")),
                button("md-btn text sm", () => setTab("login"), txt("Sign in"))));
    }

    private static VNode OAuthSection()
    {
        return div(Css.oauth_section,
            div(Css.oauth_divider,
                span(txt("or continue with"))),
            // TODO: Integrate third-party OAuth providers (GitHub, Google, Microsoft)
            div(Css.oauth_buttons,
                button(new Props(Css.oauth_btn).Disabled("true").TitleAttr("Coming soon"),
                    txt("GitHub")),
                button(new Props(Css.oauth_btn).Disabled("true").TitleAttr("Coming soon"),
                    txt("Google")),
                button(new Props(Css.oauth_btn).Disabled("true").TitleAttr("Coming soon"),
                    txt("Microsoft"))));
    }
}
