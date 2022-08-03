using Application.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public interface IPessoasService
    {
        void Add<Pessoas>(Pessoas entity);
        void AddRange(List<Pessoas> entity);
        void Update<Pessoas>(Pessoas entity);
        void Delete<Pessoas>(Pessoas entity);


        Task<bool> SaveChangeAsync();

        Task<Pessoas[]> GetAll(string filter);

        Task<Pessoas> GetPessoaById(long id);

        string PessoaValidate(Pessoas model);
    }
}
