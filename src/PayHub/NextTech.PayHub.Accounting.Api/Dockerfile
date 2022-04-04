#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/PayHub/NextTech.PayHub.Accounting.Api/NextTech.PayHub.Accounting.Api.csproj", "src/PayHub/NextTech.PayHub.Accounting.Api/"]
RUN dotnet restore "src/PayHub/NextTech.PayHub.Accounting.Api/NextTech.PayHub.Accounting.Api.csproj"
COPY . .
WORKDIR "/src/src/PayHub/NextTech.PayHub.Accounting.Api"
RUN dotnet build "NextTech.PayHub.Accounting.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "NextTech.PayHub.Accounting.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NextTech.PayHub.Accounting.Api.dll"]