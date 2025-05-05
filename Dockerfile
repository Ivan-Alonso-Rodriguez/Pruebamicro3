FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . ./

RUN dotnet new sln && dotnet sln add imagenes-ms.csproj
RUN dotnet add package MongoDB.Driver
RUN dotnet add package Microsoft.AspNetCore.Http.Abstractions
RUN dotnet restore
RUN dotnet publish imagenes-ms.csproj -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "imagenes-ms.dll"]
