<INSTRUCTIONS>
## Project Overview
- Fullstack dev container for .NET 10 Web API + Angular 21, with MongoDB 7.0.
- Dev tooling runs in `.devcontainer/docker/docker-compose.fullstack.yml`.
- Shared Mongo helper script: `.devcontainer/docker/run-shared-mongo.sh`.

## Key Paths
- API: `backend/src/Mvp/Mvp.WebApi/Program.cs`
- App settings: `backend/src/Mvp/Mvp.WebApi/appsettings.json` and `backend/src/Mvp/Mvp.WebApi/appsettings.Development.json`
- Devcontainer compose: `.devcontainer/docker/docker-compose.fullstack.yml`
- Dev env vars: `.devcontainer/docker/.env`

## Configuration Conventions
- Mongo settings are supplied via compose environment variables (container-first).
- `MongoDb__ConnectionString` and `MongoDb__DatabaseName` are used by the API.
- Shared Mongo host port is `MONGO_PORT` in `.devcontainer/docker/.env` (used by `run-shared-mongo.sh`).
- Containers connect to Mongo at `mongo:27017` on the shared network.

## Repo Docs
- Update `README.md` when changing devcontainer behavior or env variable names.
</INSTRUCTIONS>
