# ParkingWebAPI
ASP.NET Core Web API for ParkingSimulating

How to compile
==============

1. Install software

- Git
  https://git-for-windows.github.io/
  Select a file summarized as "Full installer for official Git for Windows"
   with the highest version
- Visual Studio
  http://www.visualstudio.com/downloads/download-visual-studio-vs
  Select "Visual Studio Community 2017 for Windows Desktop" version 15.7.1 or newer.
- Microsoft .NET Core 2.0

2. Check out

- Create an empty folder anywhere
- In explorer left click and select "Git Clone"
  set URL https://github.com/5avel/ParkingWebAPI.git
  OK

3. Build

- Open ParkingWebAPI.sln with Visual Studio 2017 for windows desktop.
- Compile.

4. Use

-Open Postman (https://www.getpostman.com/) or Fiddler (https://www.telerik.com/fiddler)
## Endpoints API

 Type |         Method        | Description
------|-----------------------|------------
GET   | /api/cars             | Список всіх машин
GET   | /api/cars/{id}        | Деталі по одній машині
DELETE| /api/cars/{id}        | Видалити машину
POST  | /api/cars             | Додати машину {"id":"10","balance":13.0,"carType":2}
GET   | /api/Parking/free     | Кількість вільних місць
GET   | /api/Parking/occupied | Кількість зайнятих місць
GET   | /api/Parking/income   | Загальний дохід
GET   | /api/Transactions/log   | Вивести Transactions.log
GET   | /api/Transactions   | Вивести транзакції за останню хвилину
GET   | /api/Transactions/{id}   | Вивести транзакції за останню хвилину по одній конкретній машині
PUT   | /api/Transactions/{id}   | Поповнити баланс машини: 10.25

## Azure URL http://myparkingwebapi.azurewebsites.net  
