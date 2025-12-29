# Clean Architecture MVP Template

This template provides a batteries-included Clean Architecture setup for building .NET 10 APIs and Angular 21 front-ends in a single workspace. A prebuilt Docker image (`ghcr.io/hallboard-team/fullstack-dev:dotnet10.0-node24-ng21`) ships with all tooling so you can focus on writing features instead of bootstrapping environment scripts.

## Stack
- .NET 10 Web API (`backend/`)
- Angular 21 + Node 24 tooling (`frontend/`)
- MongoDB 7.0 for local persistence
- VS Code dev container + docker compose workflow

## Repository Layout
| Path | Description |
| --- | --- |
| `backend/src` | Clean Architecture source (domain, application, infrastructure, API). |
| `backend/tests` | Test projects. |
| `frontend` | Angular workspace. |
| `.devcontainer` | Dev container definition, compose file, and helper scripts. |

## Development Environments

### VS Code Dev Container
1. Install Docker Desktop + VS Code with the *Dev Containers* extension.
2. Open this folder in VS Code and run `Dev Containers: Reopen in Container`.
3. VS Code builds the stack defined in `.devcontainer/docker/docker-compose.full.yml` and runs the `ghcr.io/hallboard-team/fullstack-dev:dotnet10.0-node24-ng21` image with MongoDB.

The container exposes:
- API at http://localhost:5002 (override with `API_PORT`)
- Angular landing dev server at http://localhost:4202 (`LANDING_PORT`)
- Angular dashboard dev server at http://localhost:4203 (`DASHBOARD_PORT`)
- MongoDB at localhost:28000 (`MONGO_PORT`)

Inside the container you can run normal workflows:
```bash
dotnet build backend/src/Mvp/Mvp.sln
dotnet test backend/tests
npm install && npm start --prefix frontend
```

### VS Code Mongo Tools Dev Container
If you only need MongoDB tools (`mongosh`, replica set setup, etc.), open the alternative devcontainer at `.devcontainer/mongo/devcontainer.json`. It attaches to the `mongo-tools` service (with the workspace mounted) and keeps `mongo` running in the background.

Replica set note: the MongoDB container starts with auth enabled and `--replSet rs0`. A keyfile is generated on first run at `/data/db/mongo-keyfile` inside the MongoDB volume.

### Standalone Docker Compose Runner
If you prefer running outside VS Code, use the helper script:
```bash
cd .devcontainer/docker
./pull-start-full-dev.sh 5002 4202 10.0 24 21 7.0
```
The script pulls the `fullstack-dev` image for the requested versions, ensures the VS Code server cache has correct permissions, and launches the compose stack in detached mode.

## Configuration

You can tweak the environment through compose variables or script arguments:

| Variable | Default | Purpose |
| --- | --- | --- |
| `DOTNET_VERSION` | `10.0` | Maps to the `dotnet` segment of the `fullstack-dev` image tag. |
| `NODE_VERSION` | `24` | Maps to the `node` segment of the image tag. |
| `ANGULAR_VERSION` | `21` | Maps to the `ng` segment of the image tag. |
| `MONGO_VERSION` | `7.0` | Chooses the backing MongoDB version. |
| `COMPOSE_PROJECT_NAME` | `template` | Prefix for running containers, networks, and volumes. |
| `API_PORT` | `5002` | Host port forwarded to the .NET API. |
| `LANDING_PORT` | `4202` | Host port forwarded to the Angular landing dev server. |
| `DASHBOARD_PORT` | `4203` | Host port forwarded to the Angular dashboard dev server. |
| `MONGO_PORT` | `28000` | Host port forwarded to MongoDB. |
| `DB_NAME` | `backend_mongo_db` | Database name set on the MongoDB container. |
| `DB_USER` / `DB_PASSWORD` | `backend_mongo_user` / `backend_mongo_password` | Credentials for the default connection string. |

Override any variable via `.env`, exported environment variables, or by editing `.devcontainer/docker/docker-compose.full.yml`.

## Database Access
MongoDB data persists in the `mongodata` Docker volume declared in the compose file. Connect with any client using the connection string printed inside the container (identical to `MongoDb__ConnectionString` in the compose file). Remove the volume (`docker volume rm <project>_mongodata`) if you want a clean slate.
This project expects the API to run inside the dev container, so MongoDB settings are supplied via compose environment variables rather than `appsettings*.json`.

## Next Steps
- Drop your existing services into `backend/src` and `frontend/`.
- Update `pull-start-full-dev.sh` or the compose file if you need additional services (Redis, storage emulators, etc.).
- Add CI to build/test using the same container to guarantee parity across the team.
