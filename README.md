# CaptchaApp
Приложение для хранения наборов данных распознавания.

Для начала работы нужно развернуть БД.
Путь к будущей БД в файле "appsettings.json"->"ConnectionStrings"->"Database"
В VisualStudio в окне "Package Manager Console" введите команду "Update-Database".

Приложение получилось не одностраничное. Честно, я не успел бы сделать.
На странице "Список модулей" отображается список загруженных наборов.
На странице "Добавить модуль" происходит добавление.

В коде оставил комментарии, что не успел сделать.

PS: Blazor может падать на старте при работе в Chrome (у меня повторялось на 1 машине).
С Firefox - like a charm.
