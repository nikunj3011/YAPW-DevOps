#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore "./YAPW/YAPW.csproj"
WORKDIR "/src/YAPW"
RUN dotnet build "YAPW.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "YAPW.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "YAPW.dll"]

#docker run -p 5001:80 -d yapw
#docker run -p 5000:80 -p 5001:443 -e https://localhost:7024;http://localhost:5206

# docker run -p 8080:80 yapw .
# http://localhost:8080/swagger/index.html