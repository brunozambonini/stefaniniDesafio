using Application.Context;
using Application.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Interfaces
{
    public class CidadesService : ICidadesService
    {
        private readonly StefaniniContext _context;

        public CidadesService(StefaniniContext context)
        {
            _context = context;
        }

        public void Add<Cidades>(Cidades entity)
        {
            _context.Add(entity);
        }

        public void AddRange(List<Cidades> entity)
        {
            _context.AddRange(entity);
        }

        public void Delete<Cidades>(Cidades entity)
        {
            _context.Remove(entity);
        }
        public void Update<Cidades>(Cidades entity)
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Cidades[]> GetAllPaginated(string term, int page, int pageSize)
        {
            IQueryable<Cidades> query = _context.Cidades;

            query = query.AsNoTracking()
                    .Where(x => x.Nome.Contains(term));

            int qtd = query.Count();
            int qtdPaginated = Convert.ToInt32(Math.Ceiling(qtd * 1M / pageSize));

            if (page > qtdPaginated)
            {
                return null;
            }

            // Pula uma qunatidade de registros de acordo com a pagina
            // Se for a primeira página, pega os primeiros registros, se for a segunda, pula as primeiras que foi pego na primeira página...
            query = query.Skip(pageSize * (page - 1)).Take(pageSize);
            return await query.ToArrayAsync();
        }

        public async Task<Cidades[]> GetAll(string filter)
        {
            IQueryable<Cidades> query = _context.Cidades;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.AsNoTracking()
                    .Where(x => x.Nome.ToLower().Contains(filter.ToLower()));
            }

            return await query.ToArrayAsync();
        }

        public async Task<Cidades> GetCidadeById(long id)
        {
            IQueryable<Cidades> query = _context.Cidades;

            query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public string CidadeValidate(Cidades model)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(model.Nome))
                errors += "Insira um nome.";
            if (string.IsNullOrWhiteSpace(model.UF))
                errors += "Insira uma UF.";
            if (!string.IsNullOrWhiteSpace(model.UF) && model.UF.Length > 2)
                errors += "Insira uma UF válida.";
            return errors;
        }
    }
}
