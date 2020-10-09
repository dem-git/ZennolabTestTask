namespace CaptchaApp.Client.Components
{
    /// <summary>
    /// Интерфейс компонета для отображения ошибок.
    /// </summary>
    public interface IErrorComponent
    {
        /// <summary>
        /// Показать сообщение с ошибкой.
        /// </summary>
        /// <param name="title">Заголовок.</param>
        /// <param name="message">Сообщение.</param>
        void ShowError(string title, string message);

        /// <summary>
        /// Очистить и спрятать сообщения с ошибкой.
        /// </summary>
        void ClearAndHide();
    }
}
