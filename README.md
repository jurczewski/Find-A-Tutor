[![Build Status](https://travis-ci.org/jurczewski/Find-A-Tutor.svg?branch=master)](https://travis-ci.org/jurczewski/Find-A-Tutor)
# 👨‍🏫 Find-A-Tutor
.NET Core Web application for tutors and students.
Part of my BSc thesis entitled ***"Application for tutors with Paypal transactions in .Net Core technology".***

# Technology Stack
- Backend (Api): .NET Core 2.2
- Front: ASP.NET Razor
- Databse: MSSQL image (docker)

# How to set up project
- Git clone
- docker-compose --build
- Connect to db and exec .sql from /scripts to set up database

or
- change "sql:inMemory" to **true** in *Backend\Find-A-Tutor.Api\appsettings.json* and rerun docker-compose
