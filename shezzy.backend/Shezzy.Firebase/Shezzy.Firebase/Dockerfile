#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

WORKDIR /app
COPY ["https", "https"]
COPY "/gcloud/application_default_credentials.json" "/home/.config/gcloud/application_default_credentials.json"
#Copy Google application default credentials
#%APPDATA%\gcloud\application_default_credentials.json
#$HOME/.config/gcloud/application_default_credentials.json

EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Shezzy.Firebase/Shezzy.Firebase.csproj", "Shezzy.Firebase/"]
RUN dotnet restore "Shezzy.Firebase/Shezzy.Firebase.csproj"
COPY . .
WORKDIR "/src/Shezzy.Firebase"
RUN dotnet build "Shezzy.Firebase.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Shezzy.Firebase.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Shezzy.Firebase.dll"]