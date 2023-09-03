#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 81
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./YAPW.Website/YAPW.Website.csproj"
WORKDIR "/src/YAPW.Website"
RUN dotnet build "YAPW.Website.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "YAPW.Website.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YAPW.Website.dll"]

#docker build -t yapw.website .
# docker run -p 8080:80 yapw .
# http://localhost:8080/swagger/index.html