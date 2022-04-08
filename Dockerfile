FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["eShop-DHA/eShop-DHA.csproj", "eShop-DHA/"]
RUN dotnet restore "eShop-DHA/eShop-DHA.csproj"
COPY . .
WORKDIR "/src/eShop-DHA"
RUN dotnet build "eShop-DHA.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "eShop-DHA.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "eShop-DHA.dll"]
CMD ASPNETCORE_URLS=http://*:$PORT dotnet eShop-DHA.dll