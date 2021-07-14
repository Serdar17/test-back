# Тестовое задание на вакансию back-end developer junior/middle

Обязательно использовать фрэймворк Net Core последней версии, язык программирования C#.

Тестовое задание делится на две части:
1. Задание для junior 
2. Задание для middle 

Нормальный срок выполнения всего тестового задания, с учётом занятости кандидата 7 дней, если срок превышен, то на собеседовании нужно рассказать почему.
В задание могут встречаться ссылки на домен приложения http://localhost:5000, это домен, на котором локально запускается ваше приложение, вы можете использовать любой другой порт. 

## # Для junior

1. Создать приложение net core. Общие пожелания:  

    - Модель User (пользователь, который регистрируется и потом авторизуется) сожержит поля:  

      | Поле | Назначение | Тип данных | Уникально |
      | :---: | :---: | :---: | :---: |
      | Id | Идентификатор | int | Да |
      | FIO | ФИО | String | Нет |
      | Phone | Номер телефона | String | Да |
      | Email | Email | String | Да |
      | Password | Пароль | String | Да |
      | LastLogin | Дата последней авторизации | DateTime | Нет |
  
    - Должна быть возможность хранить пользователей в базе данных или в памяти приложения (например, в коллекции List). Доступ к хранилищу данных, должен осуществляться через интерфейсы (например IUserRepository). Какое именно использовать хранилище определяется при старте приложения. Необходимо вынести это в настройки, для задания junior достаточно реализовать только вариант с хранением в памяти приложения.
    - Доступ к хранилищу данных, должен осуществляться через интерфейсы (например IUserRepository).
    - Доступ к функционалу приложения должен быть представлен в двух вариантах (бизнес логика должна быть вынесена в общие классы).
      + Через REST API (ApiController-ы которые находят ся в отдельном Areas).
      + Через стандартные MVC html-  странички razor-шаблонизатора (обычные Controller-ы).
    - Контроллеры должны находиться в Api.csproj, бизнес логика в Logic.csproj.

2. Реализовать функционал регистрации  

2.1. Реализация через HTML-страницы
    - Шаблон cshtml для razor.
    - Методы для генерации html-страниц регистрации должны находиться в AccountController, который унаследован от Controller.
    - Контроллер AccountController необходимо унаследовать от Controller
    - Должна быть возможность перейти на страницу по ссылке http://localhost:5000/account/register
    - Бизнес логику вынести в класс (AccountManager), который внедряется через Dependency Injection.
    - Шаблон содержит форму с полями (валидация на стороне сервера):  
    
        | Поле | Название | Тип данных | Обязательно | Уникально | Длина | Валидатор |
        | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
        | FIO | ФИО | строка | Да | Нет | 250 | - |
        | Phone | Номер телефона | строка | Да | Да | 11 | Только цифры, начинается с "7" |
        | Email | Email | строка | Да | Да | 150 | Валидация для email |
        | Password | Пароль | строка | Да | Нет | 20 | - |
        | PasswordConfirm | Подтверждение пароля | строка | Да | Нет | 20 | должно совпадать с полем Password | 
        
     - Если какие-то из полей не прошли валидацию, то необходимо подсветить их красным и написать ошибку под соответствующим полем.
     - Если пользователь с указанным email или номером телефона уже есть в системе, то не разрешать регистрацию.
     - После успешной регистрации, показывать страницу с текстом "Поздравляем %FIO%, вы стали пользователем системы !" и с одной ссылкой, которая ведёт на страницу авторизации.
     
2.2. Реализация через REST API
    - Создать контроллер AccountApiController и разместить его в Areas Api.
    - End-point Должен быть http://localhost:5000/api/account/register
    - Контроллер необходимо унаследовать от ControllerBase. 
    - Контроллер AccountApiController должен использовать AccountManager из п. 2.1.
    - 
    
3. Создать контроллер AccountController, он должен выполнять две функции и формировать две страницы: автозиция и регистрация пользователя.
    - Страница регистрации нового пользователя: 
        + Шаблон cshtml для razor.
        + Должна быть возможность перейти на страницу по ссылке http://localhost:5000/account/register
        + Шаблон содержит форму с полями (валидация на стороне сервера):  
    
        | Поле | Название | Тип данных | Обязательно | Уникально | Длина | Валидатор |
        | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
        | FIO | ФИО | строка | Да | Нет | 250 | - |
        | Phone | Номер телефона | строка | Да | Да | 11 | Только цифры, начинается с "7" |
        | Email | Email | строка | Да | Да | 150 | Валидация для email |
        | Password | Пароль | строка | Да | Нет | 20 | - |
        | PasswordConfirm | Подтверждение пароля | строка | Да | Нет | 20 | должно совпадать с полем Password |  
        
        + Если какие-то из полей не прошли валидацию, то необходимо подсветить их красным и написать ошибку под соответствующим полем.
        + Должна быть ссылка на страницу авторизации.
        + Если пользователь с указанным email или номером телефона уже есть в системе, то не разрешать регистрацию.
        + После успешной регистрации, показывать страницу с текстом "Поздравляем %FIO%, вы стали пользователем системы !" и с одной ссылкой, которая ведёт на страницу авторизации.
    
    - Страница авторизации пользователя:
        + Шаблон cshtml для razor.
        + Должна быть возможность перейти на страницу по ссылке http://localhost:5000/account/login .
        + Шаблон содержит форму с полями (валидация на стороне сервера):  

        | Поле | Название | Тип данных | Обязательно | Длина | Валидатор |
        | :---: | :---: | :---: | :---: | :---: | :---: |
        | Phone | Номер телефона | строка | Да | 11 | Только цифры, начинается с "7" |
        | Password | Пароль | строка | Да | 20 | - |
    
        + Если авторизация не выполняется: не найден номер телефона, не верный пароль или что-то другое - то поле "номер телефона" необходимо подсветить красным и добавить ошибку валидации с текстом "Ошибка авторизации".
        + Должна быть ссылка на страницу регистрации.

4. Создать контроллер CabinetController, он должен содержать один метод (GetInfo), который возвращает шаблон Razor
  - Страница с информацией об авторизованном пользователе. Выводится (ФИО, Email, Номер телефона)
  - Есть кнопка Logout, которая заканчивает сессию и редиректит на страницу авторизации.
  - 
