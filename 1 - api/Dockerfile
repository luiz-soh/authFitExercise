#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["1 - api/Fitexerciselogin_api.csproj", "1 - api/"]
COPY ["3 - models/Models.csproj", "3 - models/"]
COPY ["2 - application/Application.csproj", "2 - application/"]
COPY ["4 - infrastructure/Infrastructure.csproj", "4 - infrastructure/"]
RUN dotnet restore "1 - api/Fitexerciselogin_api.csproj"
COPY . .
WORKDIR "/src/1 - api"
RUN dotnet build "Fitexerciselogin_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Fitexerciselogin_api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Fitexerciselogin_api.dll"]