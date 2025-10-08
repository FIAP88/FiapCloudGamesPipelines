namespace fiapcloudgames.usuario.API.Attributes
{
    /// <summary>
    /// Atributo para marcar endpoints que n�o requerem autentica��o por API Key
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AllowAnonymousApiKeyAttribute : Attribute
    {
    }
}