#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["viecvat247API/viecvat247API.csproj", "viecvat247API/"]
COPY ["BussinessObject/BussinessObject.csproj", "BussinessObject/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
COPY ["viecvat247Service/viecvat247Service.csproj", "viecvat247Service/"]
RUN dotnet restore "viecvat247API/viecvat247API.csproj"
COPY . .
WORKDIR "/src/viecvat247API"
RUN dotnet build "viecvat247API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "viecvat247API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "viecvat247API.dll"]