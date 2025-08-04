#!/bin/bash

# Check if migration name argument is provided
if [ $# -eq 0 ]; then
    echo "Error: Migration name not specified"
    echo "Usage: ./add-migration.sh <migration_name>"
    exit 1
fi

MIGRATION_NAME=$1

dotnet build

# Execute EF Core migration command
dotnet ef migrations add "$MIGRATION_NAME" \
    --project "src/Torrnox.Migrations.Sqlite" \
    --startup-project "src/Torrnox.Web" \
    --context EntityDataContext \
    --output-dir Entity/Migrations \
    --no-build \
    -- "sqlite"

dotnet ef migrations add "$MIGRATION_NAME" \
    --project "src/Torrnox.Migrations.Sqlite" \
    --startup-project "src/Torrnox.Web" \
    --context CacheDataContext \
    --output-dir Cache/Migrations \
    --no-build \
    -- "sqlite"

dotnet ef migrations add "$MIGRATION_NAME" \
    --project "src/Torrnox.Migrations.Npgsql" \
    --startup-project "src/Torrnox.Web" \
    --context EntityDataContext \
    --output-dir Entity/Migrations \
    --no-build \
    -- "npgsql"
