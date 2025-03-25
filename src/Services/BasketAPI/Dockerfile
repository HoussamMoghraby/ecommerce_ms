FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 7000

ENV ASPNETCORE_URLS=http://+:7000

USER app
FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG configuration=Release
WORKDIR /src
COPY ["Services/BasketAPI/BasketAPI.csproj", "Services/BasketAPI/"]
RUN dotnet restore "Services/BasketAPI/BasketAPI.csproj"
COPY . .
WORKDIR "/src/Services/BasketAPI"
RUN dotnet build "BasketAPI.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BasketAPI.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BasketAPI.dll"]
