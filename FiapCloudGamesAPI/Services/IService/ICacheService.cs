namespace AutenticacaoEAutorizacaoCorreto.Services.IService
{
    public interface ICacheService
    {
        object get(string key);
        void set(string key, object content);
    }
}
