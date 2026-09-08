#!/bin/bash

echo "===================="
echo " STARTING SAAS"
echo "===================="

dotnet restore
dotnet build
dotnet run
