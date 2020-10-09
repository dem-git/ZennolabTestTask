namespace CaptchaApp.Server.Configuration
{
    /// <summary>
    /// Константы приложения.
    /// </summary>
    public static class AppConstants
    {
        /// <summary>
        /// Секция в настройках для строки соединения с БД.
        /// </summary>
        public const string CaptchaDbConnectionSection = "Database";

        /// <summary>
        /// Секция в настройках для пути к папке с файлами-архивами.
        /// </summary>
        public const string ImageFilesPathSection = "ImageFilesPath";
    }
}
