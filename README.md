# 👨‍🏫 Find-A-Tutor
.NET Core Web application for tutors and students.  
Part of my BSc thesis entitled ***"Application for tutors with Paypal transactions in .NET Core technology".***

# Technology Stack
- Backend (WebAPI): .NET Core 2.2
- Front: ASP.NET Razor
- Database: MSSQL (docker image)

# How to set up project
- Git clone
- docker-compose --build
- Connect to db and exec all .sql files from /scripts to set up database

or
- In *Backend\Find-A-Tutor.Api\appsettings.json*, change "sql:inMemory" to **true** and rerun docker-compose

# Bachelor Thesis
Below you can find my bachelor thesis (unfortunately only in polish):  
https://github.com/jurczewski/Find-A-Tutor/blob/master/Docs/Bachelor_Thesis.pdf
