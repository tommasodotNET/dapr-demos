pushd %~dp0\..
docker build -t daprdemos-publisher:latest -f ./publisher/Publisher/Dockerfile .
popd