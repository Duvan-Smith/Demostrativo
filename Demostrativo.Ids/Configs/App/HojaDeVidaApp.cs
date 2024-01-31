namespace Demostrativo.Ids.Configs.App;

public sealed class HojaDeVidaApp : ConfigBase
{
    protected override string AppIdentifier => "HojaDeVida";

    public override string AppName => "Demostrativo, HojaDeVida";

    public override string Description => "HojaDeVida services for: Demostrativo";

    protected override List<string> AppExternalIdentifier => new();

    protected override List<string> ListScopes => new();

    public override string DisplayName => "HojaDeVida Api";

    public override string FrontendPort => "3000";

    public override string BackendPort => "5063";

    public override string GatewayPort => "7251";
}