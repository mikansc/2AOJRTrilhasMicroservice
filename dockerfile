FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /App

# Copy everything
COPY . ./
# Restore as distinct layers
RUN dotnet restore TrilhasMicroservice.csproj
# Build and publish a release
RUN dotnet publish TrilhasMicroservice.csproj -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /App
COPY --from=build-env /App/out .

EXPOSE 3000
ENV ASPNETCORE_URLS=http://*:3000

ENTRYPOINT ["dotnet", "TrilhasMicroservice.dll"]