FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 5000

ENV ASPNETCORE_URLS=http://+:5000

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY . .
RUN dotnet restore ./publisher/Publisher/Publisher.csproj
RUN dotnet build ./publisher/Publisher/Publisher.csproj -c Release

FROM build as publish
RUN dotnet publish ./publisher/Publisher/Publisher.csproj -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Publisher.dll"]