#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/TravelBook.Web/TravelBook.Web.csproj", "TravelBook.Web/"]
COPY ["src/TravelBook.Infrastructure/TravelBook.Infrastructure.csproj", "TravelBook.Infrastructure/"]
COPY ["src/TravelBook.Core/TravelBook.Core.csproj", "TravelBook.Core/"]
RUN dotnet restore "TravelBook.Web/TravelBook.Web.csproj"
COPY ["src/", "./"]
WORKDIR "/src/TravelBook.Web"
RUN dotnet build "TravelBook.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TravelBook.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT [ "dotnet", "TravelBook.Web.dll" ]