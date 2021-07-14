# Тестовое задание на вакансию back-end developer junior/middle

Обязательно использовать фрэймворк Net Core последней версии, язык программирования C#.

Тестовое задание делится на две части:
1. Задание для junior 
2. Задание для middle 

Нормальный срок выполнения всего тестового задания, с учётом занятости кандидата 7 дней, если срок превышен, то на собеседовании нужно рассказать почему.
В задание могут встречаться ссылки на домен приложения http://localhost:5000, это домен, на котором локально запускается ваше приложение, вы можете использовать любой другой порт. 

## # Для junior

###### 1. Создать приложение net core. Общие пожелания:
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
    + Через стандартные MVC html-странички razor-шаблонизатора (обычные Controller-ы).
  - Контроллеры должны находиться в Api.csproj, бизнес логика в Logic.csproj.
###### 2. Реализовать функционал регистрации
###### 2.1. Реализация через HTML-страницы
  - Методы для генерации html-страниц регистрации должны находиться в AccountController, который унаследован от Controller.
  - Должна быть возможность перейти на страницу по ссылке http://localhost:5000/account/register
  - Бизнес логику вынести в класс (AccountManager), который внедряется через Dependency Injection.
  - Шаблон cshtml для razor.
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
###### 2.2. Реализация через REST API
  - Создать контроллер AccountApiController, который унаследован от ControllerBase и разместить его в Areas Api.csproj.
  - End-point должен быть http://localhost:5000/api/account/register
  - REST на регистрацию пользователя ожидает на вход модель (RegisterRequest) и имеет тип POST:
  
    | Поле | Название | Тип данных | Обязательно | Уникально | Длина | Валидатор |
    | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
    | FIO | ФИО | строка | Да | Нет | 250 | - |
    | Phone | Номер телефона | строка | Да | Да | 11 | Только цифры, начинается с "7" |
    | Email | Email | строка | Да | Да | 150 | Валидация для email |
    | Password | Пароль | строка | Да | Нет | 20 | - |
    | PasswordConfirm | Подтверждение пароля | строка | Да | Нет | 20 | должно совпадать с полем Password | 
    
  - Контроллер AccountApiController должен использовать AccountManager из п. 2.1.
  - Если регистрация прошла успешно, то с сервера должен приходить 200-ый статус ответа, с пустым телом ответа.
  - Если в процессе регистрации пользователя возникли ошибки (пользователь уже зарегистрирован), то необхоидмо возвращать 400-ый статус ответа и модель (ErrorResponse) состоящую из полей:
 
    | Поле | Название | Тип данных | Уникально |
    | :---: | :---: | :---: | :---: |
    | Code | Код ошибки | строка | да |
    | Message | Расшифровка кода ошибки | строка | да |
  
  - Ошибку валидации входной модели (ModelState) можно оставить в том виде, в котором возвращает её net core.  
###### 3. Реализовать функционал авторизации
###### 3.1. Реализация через HTML-страницы
  - Методы для генерации html-страниц авторизации должны находиться в AccountController, который унаследован от Controller.
  - Должна быть возможность перейти на страницу по ссылке http://localhost:5000/account/login
  - Бизнес логику вынести в класс (AccountManager), который внедряется через Dependency Injection.
  - Шаблон cshtml для razor.
  - Шаблон содержит форму с полями (валидация на стороне сервера):

    | Поле | Название | Тип данных | Обязательно | Длина | Валидатор |
    | :---: | :---: | :---: | :---: | :---: | :---: |
    | Phone | Номер телефона | строка | Да | 11 | Только цифры, начинается с "7" |
    | Password | Пароль | строка | Да | 20 | - |

  - Если авторизация не выполняется: не найден номер телефона, не верный пароль или что-то другое - то поле "номер телефона" необходимо подсветить красным и добавить ошибку валидации с текстом "Ошибка авторизации".
  - Должна быть ссылка на страницу регистрации.
  - Если авторизация выполнилась корректно пользователь должен смочь перейти на http://localhost:5000/cabinet. Т.е. этот адрес должен быть закрыт авторизационным атрибутом Authorize.
  - Без авторизованной сессии пользователь не может попасть в http://localhost:5000/cabinet 
###### 3.2. Реализация через REST API
  - Создать контроллер AccountApiController, который унаследован от ControllerBase и разместить его в Areas Api.csproj.
  - End-point должен быть http://localhost:5000/api/account/login
  - REST на авторизацию пользователя ожидает на вход модель (LoginRequest) и имеет тип POST:

    | Поле | Название | Тип данных | Обязательно | Длина | Валидатор |
    | :---: | :---: | :---: | :---: | :---: | :---: |
    | Phone | Номер телефона | строка | Да | 11 | Только цифры, начинается с "7" |
    | Password | Пароль | строка | Да | 20 | - |
    
  - Методы для авторизации должны использовать AccountManager из п. 2.1.
  - Если авторизация прошла успешно, то с сервера должен приходить 200-ый статус ответа. Возможны 2 варианта выдачи авторизованной сессии: заголовок Set-Cookies или выдача токена. Реализовать любой из вариантов. 
  - Без авторизованной сессии пользователь не может попасть в http://localhost:5000/cabinet
  - Если в процессе авторизации пользователя возникли ошибки (пользователь не найен или неверный пароль), то необхоидмо возвращать 400-ый статус ответа и модель (ErrorResponse) состоящую из полей:
 
    | Поле | Название | Тип данных | Уникально |
    | :---: | :---: | :---: | :---: |
    | Code | Код ошибки | строка | да |
    | Message | Расшифровка кода ошибки | строка | да |
  
  - Ошибку валидации входной модели (ModelState) можно оставить в том виде в котором возвращает её net core. 

###### 4. Функционал авторизованной зоны

    
