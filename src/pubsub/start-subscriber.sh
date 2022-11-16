cd ./Subscriber
dapr run --app-id subscriber --app-port 5001 --components-path ./dapr/components -- dotnet run --project ./Subscriber/