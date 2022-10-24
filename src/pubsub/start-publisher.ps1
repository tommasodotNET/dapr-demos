cd ./Publisher
dapr run --app-id publisher --app-port 5000 -- dotnet run --components-path ./dapr/components