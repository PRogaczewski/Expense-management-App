FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 90
EXPOSE 450

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["DocumentApi/DocumentApi/DocumentApi.csproj", "DocumentApi/DocumentApi/"]
COPY ["DocumentApi/Application/Application.Documents/Application.Documents.csproj", "DocumentApi/Application/Application.Documents/"]
RUN dotnet restore "DocumentApi/DocumentApi/DocumentApi.csproj"
COPY . .
WORKDIR "/src/DocumentApi/DocumentApi"
RUN dotnet build "DocumentApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DocumentApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DocumentApi.dll"]