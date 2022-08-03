using Application.Context;
using Application.Models;
using Application.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class PessoasService : IPessoasService
    {
        private readonly StefaniniContext _context;

        public PessoasService(StefaniniContext context)
        {
            _context = context;
        }

        public void Add<Pessoas>(Pessoas entity)
        {
            _context.Add(entity);
        }

        public void AddRange(List<Pessoas> entity)
        {
            _context.AddRange(entity);
        }

        public void Delete<Pessoas>(Pessoas entity)
        {
            _context.Remove(entity);
        }
        public void Update<Pessoas>(Pessoas entity)
        {
            _context.Update(entity);
        }

        public async Task<bool> SaveChangeAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<Pessoas[]> GetAllPaginated(string term, int page, int pageSize)
        {
            IQueryable<Pessoas> query = _context.Pessoas;

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

        public async Task<Pessoas[]> GetAll(string filter)
        {
            IQueryable<Pessoas> query = _context.Pessoas
                .Include(h => h.Cidade);

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.AsNoTracking()
                    .Where(x => x.Nome.ToLower().Contains(filter.ToLower()));
            }

            return await query.ToArrayAsync();
        }

        public async Task<Pessoas> GetPessoaById(long id)
        {
            IQueryable<Pessoas> query = _context.Pessoas;

            query = query.AsNoTracking();

            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public string PessoaValidate(Pessoas model)
        {
            string errors = "";

            if (string.IsNullOrWhiteSpace(model.Cpf) || model.Cpf.Length != 11)
                errors = "Insira um CPF válido. ";
            if (model.Idade < 1)
                errors += "Insira uma idade maior que 0. ";
            if (string.IsNullOrWhiteSpace(model.Nome))
                errors += "Insira um nome.";
            if (model.Id_Cidade == 0)
                errors += "Insira uma cidade.";

            return errors;
        }
    }
}
