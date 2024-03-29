# Опрос пациентов

* Платформа: WPF .NET 8.0

## Краткое описание

Проект для автозаполнения форм осмотра пациентов.

## Состав проекта

Ключевые понятия, которые присутствуют в проекте:

* База данных PostgreSQL
* EntityFrameworkCore
* Unit of Work
* MediatR
* FluentValidation
* AutoMapper
* OperationResult
* Microsoft Identity
* Vertical Slice Architecture
* MVVM
* AppDefinitions
* DDD
* Material Design
* Navigation и ModalNavigation

# Требования, бизнес-логика

## Роли в системе

В проекте используется доступны 3 роли, которые регламентируют доступ к функционалу.
`Administrator` - (администратор) пользователь, который может выполнять все операции с любыми сущностями.
`Doctor` - (врач) пользователь, который может добавить информацию опросу и указать диагноз.
`Nurse` - (медицинская сестра) пользователь, который может только добавить информацию опроса.

Работа только в режиме авторизации. 
