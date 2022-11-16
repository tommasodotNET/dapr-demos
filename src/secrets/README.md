# Dapr Secrets Component Demo

This demo shows how to use the Dapr secret component to read secrets from localfile.

## Run Demo

Run the following command to start the demo:

Linux:
```bash
./start-secretsdemo.sh
```

Windows:
```powershell
./start-secretsdemo.ps1
```

## Demo Output

Once the app is running, use the `sampleRequest.http` file to make a request and read the secret. This is a good way to show how to interact with Dapr using HTTP.

The result should be
```json
{
  "my-secret": "My_Secret_From_Local_File"
}
```