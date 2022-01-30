FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["MailMe.Backend/MailMe.Backend.csproj", "MailMe.Backend/"]
RUN dotnet restore "MailMe.Backend/MailMe.Backend.csproj"
COPY . .
WORKDIR "/src/MailMe.Backend"
RUN dotnet build "MailMe.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MailMe.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MailMe.Backend.dll"]
