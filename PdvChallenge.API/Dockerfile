FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["PdvChallenge.API/PdvChallenge.API.csproj", "PdvChallenge.API/"]
RUN dotnet restore "PdvChallenge.API/PdvChallenge.API.csproj"
COPY . .
WORKDIR "/src/PdvChallenge.API"
RUN dotnet build "PdvChallenge.API.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "PdvChallenge.API.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "PdvChallenge.API.dll"]