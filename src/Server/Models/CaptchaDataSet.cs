using System;

namespace CaptchaApp.Server.Models
{
    /// <summary>
    /// Набор данных распознавания.
    /// </summary>
    public class CaptchaDataSet
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Наименование.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Дата создания.
        /// </summary>
        public DateTimeOffset CreatedAt { get; set; }

        /// <summary>
        /// Содержит кириллицу.
        /// </summary>
        public bool ContainsCyrillic { get; set; }

        /// <summary>
        /// Содержит латиницу.
        /// </summary>
        public bool ContainsLatin { get; set; }

        /// <summary>
        /// Содержит цифры.
        /// </summary>
        public bool ContainsDigits { get; set; }

        /// <summary>
        /// Содержит специальные символы.
        /// </summary>
        public bool ContainsSpecialSymbols { get; set; }

        /// <summary>
        /// Чувствительность к регистру.
        /// </summary>
        public bool CaseSentence { get; set; }

        /// <summary>
        /// Расположение разметки.
        /// </summary>
        public AnswerPlacementEnum AnswerPlacement { get; set; }

        /// <summary>
        /// Архив картинок.
        /// </summary>
        public string ImagesZipFilename { get; set; }
    }
}
