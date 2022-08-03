using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface ICidadesService
    {
        void Add<Cidades>(Cidades entity);
        void AddRange(List<Cidades> entity);
        void Update<Cidades>(Cidades entity);
        void Delete<Cidades>(Cidades entity);


        Task<bool> SaveChangeAsync();

        Task<Cidades[]> GetAll(string filter);

        Task<Cidades> GetCidadeById(long id);

        string CidadeValidate(Cidades model);
    }
}
