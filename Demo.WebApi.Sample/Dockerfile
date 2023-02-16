#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Pm.Core/Pm.WebApi.Ergonomed/Pm.WebApi.Ergonomed.csproj", "Pm.Core/Pm.WebApi.Ergonomed/"]
COPY ["Pm.Core/Pm.Resources.Core/Pm.Resources.Core.csproj", "Pm.Core/Pm.Resources.Core/"]
COPY ["Pm.Core/Pm.Services/Pm.Services.Core.csproj", "Pm.Core/Pm.Services/"]
COPY ["Pm.Core/Pm.Data.Core/Pm.Data.Core.csproj", "Pm.Core/Pm.Data.Core/"]
COPY ["Pm.Core/Pm.Core/Pm.Core.csproj", "Pm.Core/Pm.Core/"]
COPY ["Pm.Core/Pm.Common/Pm.Common.csproj", "Pm.Core/Pm.Common/"]
COPY ["Pm.Core/Pm.Common.Model/Pm.Common.Model.csproj", "Pm.Core/Pm.Common.Model/"]
COPY ["Pm.Core/Pm.Recurring/Pm.Recurring.csproj", "Pm.Core/Pm.Recurring/"]
RUN dotnet restore "Pm.Core/Pm.WebApi.Ergonomed/Pm.WebApi.Ergonomed.csproj"
COPY . .
WORKDIR "/src/Pm.Core/Pm.WebApi.Ergonomed"
RUN dotnet build "Pm.WebApi.Ergonomed.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Pm.WebApi.Ergonomed.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Pm.WebApi.Ergonomed.dll"]