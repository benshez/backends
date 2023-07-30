#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
COPY ["https", "https"]

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Shezzy.Core/Shezzy.Core.csproj", "Shezzy.Core/"]
RUN dotnet restore "Shezzy.Core/Shezzy.Core.csproj"
FROM build AS publish
COPY . .
WORKDIR "/src/Shezzy.Core"
RUN dotnet build "Shezzy.Core.csproj" -c Release -o /app/build
RUN dotnet publish "Shezzy.Core.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "Shezzy.Core.dll"]