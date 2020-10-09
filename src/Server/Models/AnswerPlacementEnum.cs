using System.ComponentModel;

namespace CaptchaApp.Server.Models
{
    /// <summary>
    /// Вариант расположения ответов.
    /// </summary>
    public enum AnswerPlacementEnum
    {
        /// <summary>
        /// Отсутствует.
        /// </summary>
        [Description("отсутствует")]
        None = 0,

        /// <summary>
        /// В именах файлов.
        /// </summary>
        [Description("в именах файлов")]
        InFilename = 1,

        /// <summary>
        /// В отдельном файле.
        /// </summary>
        [Description("в отдельном файле")]
        InSeparateFile = 2
    }
}
