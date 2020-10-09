using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CaptchaApp.Shared.Models
{
    /// <summary>
    /// Post-модель набора данных распознавания.
    /// </summary>
    public class CaptchaDataSetPostModel : IValidatableObject
    {
        /// <summary>
        /// Идентификатор.
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Наименование.
        /// </summary>
        [Required(ErrorMessage = "Заполните наименование.")]
        [StringLength(maximumLength: 8, MinimumLength = 4, ErrorMessage = "Длина наименования должна быть от 4 до 8 символов.")]
        [RegularExpression("(?=^[a-zA-Z]*$)(^(?!.*captcha).*$)", ErrorMessage = "Наименование должно содержать только латинские буквы и не содержать 'captcha'.")]
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

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!ContainsCyrillic && !ContainsLatin && !ContainsLatin )
                yield return new ValidationResult(
                    $"Заполните одно из полей: “Содержит кириллицу”, “Содержит латиницу”, “Содержит цифры”.",
                    new[] { nameof(ContainsCyrillic), nameof(ContainsLatin), nameof(ContainsLatin) });
        }
    }
}
