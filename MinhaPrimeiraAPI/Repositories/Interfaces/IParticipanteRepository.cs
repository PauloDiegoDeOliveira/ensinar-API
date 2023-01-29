using MinhaPrimeiraAPI.Models;

namespace MinhaPrimeiraAPI.Repositories.Interfaces
{
    // "Interface" contém apenas as assinaturas de métodos.
    public interface IParticipanteRepository
    {
        IEnumerable<Participante> GetAllAsync();

        Task<Participante> PostAsync(Participante participante);

        Task<Participante> PutAsync(Participante participante);

        Task<Participante> DeleteAsync(Guid id);

        Task<Participante> GetByIdAsync(Guid id);

        Task<IList<Participante>> GetByNomeAsync(string nome);
    }
}