using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using CaptchaApp.Server.Configuration;
using CaptchaApp.Server.Extentions;
using CaptchaApp.Server.Services.Implementation;
using CaptchaApp.Shared.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CaptchaApp.Server.Services
{
    /// <summary>
    /// Сервис для работы файлом-архивом картинок.
    /// </summary>
    public class DataSetFileService : IDataSetFileService
    {
        readonly IOptions<AppOptions> _appOptions;

        public DataSetFileService(IOptions<AppOptions> appOptions)
        {
            _appOptions = appOptions;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<string>> AddFileAndValidate(IFormFile file, CaptchaDataSetPostModel postModel)
        {
            var errors = new List<string>();

            // Сохранить файл в хранилище.
            var modelDir = Path.Combine(_appOptions.Value.ImageFilesPath, postModel.Id.ToString());
            Directory.CreateDirectory(modelDir);
            var filePath = Path.Combine(modelDir, file.FileName);
            await using (var stream = File.Create(filePath))
                await file.CopyToAsync(stream);

            try
            {
                using var zipFile = ZipFile.OpenRead(filePath);
                var totalFilesCount = zipFile.Entries.Count;
                var imagesCount = zipFile.Entries
                    .Count(x => x.Name.ToLower().EndsWith(".jpg"));

                if (totalFilesCount - imagesCount > 1)
                    errors.Add("В архиве могут находиться только картинки и файл ответов.");

                // Файл с ответами.
                var answersFile = zipFile.Entries.FirstOrDefault(x => x.Name == "answers.txt");

                if (postModel.AnswerPlacement == AnswerPlacementModel.InSeparateFile)
                {
                    if (answersFile == null)
                        errors.Add(
                            $"Расплоложение разметки указано '{postModel.AnswerPlacement.GetEnumDescription()}, но файл с ответами отсутствует'.");
                    else
                    {
                        // Проверить количество ответов.
                        using var sr = new StreamReader(answersFile.Open());
                        var answersText = await sr.ReadToEndAsync();
                        string[] lines = answersText.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);

                        if (lines.Length != imagesCount)
                            errors.Add($"Количество ответов ({lines.Length}) не совпадает с количеством картинок ({imagesCount}).");
                    }
                }
                else
                {
                    if (answersFile != null)
                        errors.Add(
                            $"Найден файл с ответами, но расплоложение разметки указано '{postModel.AnswerPlacement.GetEnumDescription()}'.");
                }

                //Количество картинок должно попадать в диапазон ограничений.
                // Диапазон количества картинок по-умолчанию.
                var imgCountRange = new Range(2000, 3000);

                // Условия увеличения диапазона.
                var increaseConditions = new[]
                {
                    postModel.ContainsCyrillic,
                    postModel.ContainsLatin,
                    postModel.ContainsDigits,
                    postModel.ContainsSpecialSymbols,
                    postModel.CaseSentence
                };

                var condCount = increaseConditions.Count(x => x);
                // Увеличить границы диапазона кратно условиям.
                if (condCount > 0)
                    imgCountRange = new Range(imgCountRange.Start.Value + 3000 * condCount,
                        imgCountRange.End.Value + 3000 * condCount);

                if (imgCountRange.Start.Value > imagesCount)
                    errors.Add(
                        $"Количество картинок ({imagesCount}) должно быть не менее {imgCountRange.Start.Value}.");

                if (imgCountRange.End.Value < imagesCount)
                    errors.Add(
                        $"Количество картинок ({imagesCount}) должно быть не более {imgCountRange.End.Value}.");
            }
            catch (InvalidDataException)
            {
                errors.Add("Неверный формат файла-архива.");
            }
            finally
            {
                if (errors.Any())
                {
                    File.Delete(filePath);
                    Directory.Delete(modelDir);
                }
            }

            return errors;
        }
    }
}
