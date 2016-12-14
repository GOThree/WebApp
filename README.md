# Web App API
Copyright © GoThree 2016 | All Rights Reserved.

## How to start
To run the server:
* cd WebApp.API
* dotnet restore
* dotnet ef database update
* dotnet run

## Configuration
- Client base url - ClientSettings -> BaseUrl
- Access Token Lifetime(in minutes) - AuthSettings -> AccessTokenLifetime
- Refresh Token Lifetime(in days) - AuthSettings -> RefreshTokenLifetime
