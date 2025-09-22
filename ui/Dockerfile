# use the official Bun image
# see all versions at https://hub.docker.com/r/oven/bun/tags
FROM oven/bun:1.2 AS base
WORKDIR /app
COPY package.json bun.lock* ./
RUN bun install --frozen-lockfile --ignore-scripts

# -------------------
# Build stage
# -------------------
# FROM base AS build
# COPY . .
# RUN bun --bun run build

# -------------------
# Production stage
# -------------------
# FROM oven/bun:1.2 AS production
# WORKDIR /app
# COPY --from=build /app/.output /app
# EXPOSE 3000
# ENTRYPOINT [ "bun", "--bun", "run", "/app/server/index.mjs" ]

# -------------------
# Development stage
# -------------------
FROM base AS dev
WORKDIR /app
COPY . .

EXPOSE 3000
# Run in watch mode for hot reload
ENTRYPOINT [ "bun", "--bun", "run", "dev" ]
