#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["./SelXPressApi.csproj", ""]

RUN dotnet restore "./SelXPressApi.csproj"
COPY . .
WORKDIR "/src/."
ARG ASPNETCORE_PASSWORD

RUN dotnet dev-certs https -ep /https/aspnetapp.pfx -p $ASPNETCORE_PASSWORD
RUN dotnet dev-certs https --trust

RUN dotnet build "SelXPressApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SelXPressApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY --from=build /https/aspnetapp.pfx /https/aspnetapp.pfx
ENTRYPOINT ["dotnet", "SelXPressApi.dll"]