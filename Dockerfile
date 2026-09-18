FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY FieldFix/FieldFix.csproj ./FieldFix/
RUN dotnet restore ./FieldFix/FieldFix.csproj

COPY . ./
RUN dotnet publish ./FieldFix/FieldFix.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["sh", "-c", "ASPNETCORE_URLS=http://+:${PORT:-8080} dotnet FieldFix.dll"]
