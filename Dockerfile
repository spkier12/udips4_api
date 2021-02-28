#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
EXPOSE 1400

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["udips4_api.csproj", ""]
RUN dotnet restore "./udips4_api.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "udips4_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "udips4_api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "udips4_api.dll"]