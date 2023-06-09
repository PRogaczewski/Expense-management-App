# Expense-management-App
Expense tracker application

Simple API for tracking your daily expenses.

## Features
* Insertion of expenses based on categories
* Insertion of incomes
* Insertion of monthly goals

## Technologies
Project is created with:
* ASP.NET Core API and .NET 6.0
* Entity framework Core 6.0.9
* MSSQL server 19

## Docker (in progress)
Docker compose includes .NET API, database MSSQL server 2019 and client application in Angular v14 [Client application](https://github.com/PRogaczewski/Expense-management-angular)

You can get set-up using Docker.
If you just want an environment with webserver, API and database you shuold use this option. 

```
docker-compose up -d
```
