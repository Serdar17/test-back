# Тестовое задание на вакансию back-end developer junior/middle

Обязательно использовать фрэймворк Net Core последней версии, язык программирования C#.

Тестовое задание делится на две части:
1. Задание для junior 
2. В задание для middle 

Нормальный срок выполнения всего тестового задания, с учётом занятости кандидата 7 дней, если срок превышен, то на собеседовании нужно рассказать почему.
В задание могут встречаться ссылки на домен приложения http://localhost:5000, это домен, на котором локально запускается ваше приложение, вы можете использовать любой другой порт. 

## # Для junior

1. Создать контроллер AccountController, он должен выполнять две функции и формировать две страницы: автозиция и регистрация пользователя.
  + Страница регистрации нового пользователя: 
    - Шаблон cshtml для razor.
    - Должна быть возможность перейти на страницу по ссылке http://localhost:5000/account/register
    - Шаблон содержит форму с полями (валидация на стороне сервера):  
    
    | Поле | Название | Тип данных | Обязательно | Уникально | Длина | Валидатор |
    | :---: | :---: | :---: | :---: | :---: | :---: | :---: |
    | FIO | ФИО | строка | Да | Нет | 250 | - |
    | Phone | Номер телефона | строка | Да | Да | 11 | Только цифры, начинается с "7" |
    | Email | Email | строка | Да | Да | 150 | Валидация для email |
    | Password | Пароль | строка | Да | Нет | 20 | - |
    | PasswordConfirm | Подтверждение пароля | строка | Да | Нет | 20 | должно совпадать с полем Password |  
        
    - Если какие-то из полей не прошли валидацию, то необходимо подсветить их красным и написать ошибку под соответствующим полем.
    - Должна быть ссылка на страницу авторизации.
    - Если пользователь с указанным email или номером телефона уже есть в системе, то не разрешать регистрацию.
    - После успешной регистрации, показывать страницу успешной регистрации с текстом "Поздравляем %FIO%, вы стали пользователем системы !" и с одной ссылкой, которая ведёт на страницу авторищзации.
    
  + Страница авторизации пользователя:
    - Шаблон cshtml для razor
    - Должна быть возможность перейти на страницу по ссылке http://localhost:5000/account/login
    - Шаблон содержит форму с полями (валидация на стороне сервера):  

    | Поле | Название | Тип данных | Обязательно | Длина | Валидатор |
    | :---: | :---: | :---: | :---: | :---: | :---: |
    | Phone | Номер телефона | строка | Да | 11 | Только цифры, начинается с "7" |
    | Password | Пароль | строка | Да | 20 | - |
    
    - Если какие-то из полей не прошли валидацию, или пользователь не найден или пароль не верный, то всегда надо писать под полем "номер телефона" одно и тоже сообщение "Ошибка авторизации".
    - Если какие-то из полей не прошли валидацию, то необходимо подсветить их красным и написать ошибку под соответствующим полем.
    - Должна быть ссылка на страницу регистрации
    - 

2. Создать контроллер CabinetController, он должен содержать один метод (GetInfo), который возвращает шаблон Razor
  - Страница с информацией об авторизованном пользователе. Выводится (ФИО, Email, Номер телефона)
  - Есть кнопка Logout, которая заканчивает сессию и редиректит на страницу авторизации.
  - 
