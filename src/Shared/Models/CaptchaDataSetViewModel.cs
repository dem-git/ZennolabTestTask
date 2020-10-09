using System;

namespace CaptchaApp.Shared.Models
{
    /// <summary>
    /// Модель для представления набора данных распознавания.
    /// </summary>
    public class CaptchaDataSetViewModel
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
        /// Дата-время создания.
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
        public AnswerPlacementModel AnswerPlacement { get; set; }
    }
}
