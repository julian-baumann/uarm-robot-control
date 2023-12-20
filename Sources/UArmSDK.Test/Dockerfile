FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UArmSDK.Test/UArmSDK.Test.csproj", "UArmSDK.Test/"]
RUN dotnet restore "UArmSDK.Test/UArmSDK.Test.csproj"
COPY . .
WORKDIR "/src/UArmSDK.Test"
RUN dotnet build "UArmSDK.Test.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "UArmSDK.Test.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UArmSDK.Test.dll"]
