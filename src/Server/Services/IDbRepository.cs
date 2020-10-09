using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CaptchaApp.Server.Models;

namespace CaptchaApp.Server.Services
{
    /// <summary>
    /// Интерфейс репозитории БД.
    /// </summary>
    public interface IDbRepository
    {
        /// <summary>
        /// Получить все наборы данных.
        /// </summary>
        Task<IEnumerable<CaptchaDataSet>> GetAll();

        /// <summary>
        /// Получить набор данных по идентификатору.
        /// </summary>
        /// <param name="id">Идентификатор.</param>
        Task<CaptchaDataSet> GetById(Guid id);

        /// <summary>
        /// Добавить набор данных распознавания.
        /// </summary>
        /// <param name="value">Новый набор данных.</param>
        Task Add(CaptchaDataSet value);
    }
}
