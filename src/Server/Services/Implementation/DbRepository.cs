using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaptchaApp.Server.Configuration;
using CaptchaApp.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace CaptchaApp.Server.Services.Implementation
{
    /// <summary>
    /// Репозитория БД.
    /// </summary>
    public class DbRepository : IDbRepository
    {
        readonly CaptchaContext _context;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="context">Контекст БД.</param>
        public DbRepository(CaptchaContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<CaptchaDataSet>> GetAll()
        {
            return await _context.DataSets.ToListAsync();
        }

        /// <inheritdoc />
        public async Task<CaptchaDataSet> GetById(Guid id)
        {
            return await _context.DataSets.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task Add(CaptchaDataSet value)
        {
            await _context.DataSets.AddAsync(value);
            await _context.SaveChangesAsync();
        }
    }
}
