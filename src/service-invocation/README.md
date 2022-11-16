# Dapr Service-to-Service Invocation Demo

This demo shows how to use Dapr to make invoke REST API between two services.

## Run Demo

Run the following command to start the demo:

Linux:

1. Start server:
```bash
./start-server.sh
```
2. Start client:
```bash
./start-client.sh
```

Windows:
1. Start server:
```powershell
./start-server.ps1
```

2. Start client:
```powershell
./start-client.ps1
```

## Demo Output

Show the different ways to invoke a service:
- Using standard HTTP Client
- Using Dapr HTTP Client
- Using Dapr Client

Make sure to define the different ways in which you can send datas in the different methods.