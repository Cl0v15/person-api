#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["TPICAP.Person.API/TPICAP.Person.API.csproj", "TPICAP.Person.API/"]
RUN dotnet restore "TPICAP.Person.API/TPICAP.Person.API.csproj"
COPY . .
WORKDIR "/src/TPICAP.Person.API"
RUN dotnet build "TPICAP.Person.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TPICAP.Person.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TPICAP.Person.API.dll"]