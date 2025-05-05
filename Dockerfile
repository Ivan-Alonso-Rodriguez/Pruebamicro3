FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app
COPY . ./
RUN dotnet restore "imagenes-ms.csproj"
RUN dotnet publish "imagenes-ms.csproj" -c Release -o /out

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS final
WORKDIR /app
COPY --from=build /out .
ENTRYPOINT ["dotnet", "imagenes-ms.dll"]
