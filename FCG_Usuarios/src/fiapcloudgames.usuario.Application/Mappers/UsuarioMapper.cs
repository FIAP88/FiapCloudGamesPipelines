using fiapcloudgames.usuario.Application.DTOs;
using fiapcloudgames.usuario.Application.UseCases.Usuario.CreateUsuario;
using fiapcloudgames.usuario.Application.UseCases.Usuario.UpdateUsuario;
using fiapcloudgames.usuario.Domain.Entities;

namespace fiapcloudgames.usuario.Application.Mappers
{
    public class UsuarioMapper
    {
        public static Usuario ToEntity(CreateUsuarioCommand dto)
        {
            return new Usuario
            {
                PrimeiroNome = dto.PrimeiroNome,
                Sobrenome = dto.Sobrenome,
                Apelido = dto.Apelido,
                Email = dto.Email,
                Telefone = dto.Telefone,
                DataNascimento = dto.DataNascimento,
                HashSenha = dto.HashSenha
            };
        }

        public static Usuario ToEntity(UpdateUsuarioCommand dto, Usuario entidade, string atualizadoPor)
        {
            entidade.PrimeiroNome = dto.PrimeiroNome;
            entidade.Sobrenome = dto.Sobrenome;
            entidade.Apelido = dto.Apelido;
            entidade.Email = dto.Email;
            entidade.Telefone = dto.Telefone;
            entidade.DataNascimento = dto.DataNascimento;
            entidade.HashSenha = dto.HashSenha;
            entidade.AtualizadoPor = atualizadoPor;

            return entidade;
        }

        public static UsuarioDto ToDto(Usuario entidade)
        {
            return new UsuarioDto
            {
                UsuarioId = entidade.Id,
                PrimeiroNome = entidade.PrimeiroNome,
                Sobrenome = entidade.Sobrenome,
                Apelido = entidade.Apelido,
                Email = entidade.Email,
                Telefone = entidade.Telefone,
                DataNascimento = entidade.DataNascimento,
                HashSenha = entidade.HashSenha,
                DataCadastro = entidade.DataCadastro,
                DataAtualizacao = entidade.DataAtualizacao,
                AtualizadoPor = entidade.AtualizadoPor
            };
        }
    }
}
