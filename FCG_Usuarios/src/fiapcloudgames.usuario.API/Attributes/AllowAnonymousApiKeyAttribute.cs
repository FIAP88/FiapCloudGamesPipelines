namespace fiapcloudgames.usuario.API.Attributes
{
    /// <summary>
    /// Atributo para marcar endpoints que não requerem autenticação por API Key
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AllowAnonymousApiKeyAttribute : Attribute
    {
    }
}