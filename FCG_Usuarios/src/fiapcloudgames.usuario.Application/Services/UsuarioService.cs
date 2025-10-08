using fiapcloudgames.usuario.Application.DTOs;
using fiapcloudgames.usuario.Application.Mappers;
using fiapcloudgames.usuario.Application.Services.Interfaces;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuario;
using fiapcloudgames.usuario.Domain.Interfaces;

namespace fiapcloudgames.usuario.Application.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public IEnumerable<UsuarioDto> ObterTodos()
        {
            var entidade = _usuarioRepository.ObterTodos();

            return entidade.Select(UsuarioMapper.ToDto);
        }

        public UsuarioDto? ObterPorId(Guid id)
        {
            var entidade = _usuarioRepository.ObterPorId(id);
            if (entidade == null)
                return null;

            return UsuarioMapper.ToDto(entidade);
        }

        public UsuarioDto Cadastrar(CreateUsuarioCommand dto)
        {
            var entidade = UsuarioMapper.ToEntity(dto);

            _usuarioRepository.Cadastrar(entidade);

            return UsuarioMapper.ToDto(entidade);
        }

        public UsuarioDto? Alterar(UpdateUsuarioCommand dto, string atualizadoPor)
        {
            var entidade = _usuarioRepository.ObterPorId(dto.UsuarioId);
            if (entidade == null)
                return null;

            return UsuarioMapper.ToDto(_usuarioRepository.Alterar(UsuarioMapper.ToEntity(dto, entidade, atualizadoPor)));
        }

        public bool Deletar(Guid id)
        {
            var entidade = _usuarioRepository.ObterPorId(id);
            if (entidade == null)
                return false;

            _usuarioRepository.Deletar(id);
            return true;
        }
    }
}
