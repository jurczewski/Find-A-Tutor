[![Build Status](https://travis-ci.org/jurczewski/Find-A-Tutor.svg?branch=master)](https://travis-ci.org/jurczewski/Find-A-Tutor)
# Find-A-Tutor
.Net Core Web application for tutors and students.
Part of my BSc thesis entitled "Application for tutors with Paypal transactions in .Net Core technology".

# Technology Stack
- .Net Core 2.2
- Docker with MsSQL image

# How to set up project
- Download repository
- Restore and build Find-A-Tutor.Api and .Frontend
- Use docker-setup-mssql.ps1
  - Docker
  - Create database
  - Perform migration
- Run NLog-SetUp.sql and SeedSchoolSubjects.sql
- "dotnet run" Find-A-Tutor.Api and .Frontend
