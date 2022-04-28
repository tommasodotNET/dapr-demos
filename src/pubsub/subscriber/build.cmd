pushd %~dp0\..
docker build -t daprdemos-subscriber:latest -f ./subscriber/Subscriber/Dockerfile .
popd