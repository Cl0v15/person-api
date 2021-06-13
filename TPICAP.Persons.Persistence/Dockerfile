FROM mcr.microsoft.com/dotnet/sdk:5.0

WORKDIR /src
COPY ["TPICAP.Persons.Persistence/*.csproj", "./TPICAP.Persons.Persistence/"]
COPY ["TPICAP.Persons.Persistence/Setup.sh", "TPICAP.Persons.Persistence/Setup.sh"]

RUN dotnet restore "TPICAP.Persons.Persistence/TPICAP.Persons.Persistence.csproj"
COPY . .
WORKDIR "/src/."

RUN dotnet tool install --global dotnet-ef --version 5.0.7
ENV PATH="${PATH}:/root/.dotnet/tools"
RUN chmod +x TPICAP.Persons.Persistence/Setup.sh
CMD /bin/bash TPICAP.Persons.Persistence/Setup.sh