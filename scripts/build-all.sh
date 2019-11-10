#!/bin/bash
cd Backend
dotnet restore
dotnet build -c release
cd ..
cd Frontend
dotnet restore
dotnet build -c release
