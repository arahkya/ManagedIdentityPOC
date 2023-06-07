FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
RUN mkdir /build
WORKDIR /build
COPY *.csproj .
COPY Nuget.Config .
RUN dotnet restore
COPY . .
RUN dotnet build -c Release --no-restore
RUN dotnet publish -c Release -o /app --no-build

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
RUN mkdir /app
WORKDIR /app
COPY --from=build /app .
ENV ASPNETCORE_ENVIRONMENT=Production
ENTRYPOINT [ "dotnet", "ManagedIdentityPOC.dll" ]