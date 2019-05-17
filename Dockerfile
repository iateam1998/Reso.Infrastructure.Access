FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["./Reso.Infrastructure.Access/Reso.Infrastructure.Access.csproj", "Reso.Infrastructure.Access/"]
COPY ["./DataService/DataService.csproj", "DataService/"]
RUN dotnet restore "Reso.Infrastructure.Access/Reso.Infrastructure.Access.csproj"
COPY . .
WORKDIR "/src/Reso.Infrastructure.Access"
RUN dotnet build "Reso.Infrastructure.Access.csproj" -c Debug -o /app

FROM build AS publish
RUN dotnet publish "Reso.Infrastructure.Access.csproj" -c Debug-o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Reso.Infrastructure.Access.dll"]