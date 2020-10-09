using System.Collections.Generic;
using System.Threading.Tasks;
using CaptchaApp.Shared.Models;
using Microsoft.AspNetCore.Http;

namespace CaptchaApp.Server.Services.Implementation
{
    /// <summary>
    /// Интерфейс сервиса для работы файлом-архивом картинок.
    /// </summary>
    public interface IDataSetFileService
    {
        /// <summary>
        /// Добавить файл и проверить. Если есть ошибки - удалить файл.
        /// </summary>
        /// <returns>Список ошибок.</returns>
        Task<IEnumerable<string>> AddFileAndValidate(IFormFile file, CaptchaDataSetPostModel postModel);
    }
}
