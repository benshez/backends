#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Shezzy.Api/Shezzy.Api.csproj", "Shezzy.Api/"]
RUN dotnet restore "Shezzy.Api/Shezzy.Api.csproj"
COPY . .
WORKDIR "/src/Shezzy.Api"
RUN dotnet build "Shezzy.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shezzy.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 4025
ENV HOST=0.0.0.0 PORT=4025
ENTRYPOINT ["dotnet", "Shezzy.Api.dll"]