FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . ./

# Instalar paquetes necesarios
RUN dotnet add imagenes-ms.csproj package MongoDB.Driver
RUN dotnet add imagenes-ms.csproj package Microsoft.AspNetCore.Http.Abstractions
RUN dotnet add imagenes-ms.csproj package Swashbuckle.AspNetCore

RUN dotnet restore imagenes-ms.csproj
RUN dotnet build imagenes-ms.csproj -c Release -o /out
RUN dotnet publish imagenes-ms.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "imagenes-ms.dll"]
