# Dapr Demos

The solution contains demos for various Dapr scenarios.

## Pub Sub

The pub sub scenario demonstrates how to handle pub/sub messages using dapr. 

The **publisher** microservice has a ```/publishmessage/{greetingName}``` endpoint that receives a name and publishes it to the ```greetingname``` topic.

The **subsriber** microservice has a ```/greetingName``` endpoint that subscribes to the ```greetingname``` topic. It will greet the requested name in the standard output and will also save the received name as a state.

For the purpose of this demo, we have used an Azure Service Bus (Standard Tier, in order to leverage Topics) for the messages and an Azure Redis Cache for the state store.

### K8S Configuration

On k8s we need to create the secrets for the Service Bus connection string and redis password.

```
kubectl create secret generic sb-connection-string --from-literal=connectionstring="Endpoint=sb://pecmanager.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=YOUR_KEY"

kubectl create secret generic redis-password --from-literal=password="YOUR_PASSWORD"
```

Also, dapr components which can be found in [dapr/components](./src/pubsub/dapr/components/) directory, have to be deployed.

```
kubectl apply -f src/pubsub/dapr/components/pubsub.yaml
kubectl apply -f src/pubsub/dapr/components/statestore.yaml
```
## Dapr Configuration

To configure Dapr on your cluster, please reference the [Dapr documentation](https://docs.dapr.io/developing-applications/integrations/azure/azure-kubernetes-service-extension/#create-the-extension-and-install-dapr-on-your-aks-cluster).

## Build Docker Images

In each microservice folder, there is a `build.cmd` file, which will build the docker image.

## Helm Deploy

To deploy the microservices to Kubernetes, you can leverage the helm chart provided inside each microservice folder.