#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app

ENV ASPNETCORE_ENVIRONMENT=Development

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY *.sln .
COPY ["TPICAP.Persons.API/*.csproj", "./TPICAP.Persons.API/"]
COPY ["TPICAP.Persons.Domain/*.csproj", "./TPICAP.Persons.Domain/"]
COPY ["TPICAP.Persons.Persistence/*.csproj", "./TPICAP.Persons.Persistence/"]
COPY ["TPICAP.Persons.Shared/*.csproj", "./TPICAP.Persons.Shared/"]
COPY ["TPICAP.Persons.Tests/*.csproj", "./TPICAP.Persons.Tests/"]

RUN dotnet restore

COPY ["TPICAP.Persons.API/.", "./TPICAP.Persons.API/"]
COPY ["TPICAP.Persons.Domain/.", "./TPICAP.Persons.Domain/"]
COPY ["TPICAP.Persons.Persistence/.", "./TPICAP.Persons.Persistence/"]
COPY ["TPICAP.Persons.Shared/.", "./TPICAP.Persons.Shared/"]

WORKDIR "/src/TPICAP.Persons.API"
RUN dotnet build "TPICAP.Persons.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TPICAP.Persons.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TPICAP.Persons.API.dll"]