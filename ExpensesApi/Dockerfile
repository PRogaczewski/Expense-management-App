FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ExpensesApi/ExpensesApi.csproj", "ExpensesApi/"]
COPY ["Infrastructure.Authentication/Infrastructure.Authentication.csproj", "Infrastructure.Authentication/"]
COPY ["Application.Authentication/Application.Authentication.csproj", "Application.Authentication/"]
COPY ["DatabaseProj/Domain.csproj", "DatabaseProj/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.EF.csproj", "Infrastructure/"]
COPY ["SeedData/Infrastructure.SeedData.csproj", "SeedData/"]
COPY ["Infrastructure.ExternalServices/Infrastructure.ExternalServices.csproj", "Infrastructure.ExternalServices/"]
COPY ["Application.ExternalServices/Application.ExternalServices.csproj", "Application.ExternalServices/"]
COPY ["Infrastructure.Dapper/Infrastructure.Dapper.csproj", "Infrastructure.Dapper/"]
RUN dotnet restore "ExpensesApi/ExpensesApi.csproj"
COPY . .
WORKDIR "/src/ExpensesApi"
RUN dotnet build "ExpensesApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ExpensesApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ExpensesApi.dll"]