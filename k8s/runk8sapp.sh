#!/bin/bash
set -x

# .\runk8sapp.sh | Tee-Object -FilePath "log.txt"

# Start Minikube
minikube start --driver=docker

# Load images into Minikube
minikube image load communicationapi:dev
minikube image load authorizationapi:dev
minikube image load catalogapi:dev
minikube image load orderingapi:dev
minikube image load basketapi:dev
minikube image load documentapi:dev
minikube image load mongo:latest
minikube image load postgres:latest
minikube image load mcr.microsoft.com/mssql/server:latest

# Check if images are loaded in Minikube
minikube ssh -- docker images

# Create namespace if it doesn't exist
kubectl get namespace rentharbor || kubectl create namespace rentharbor

# Usunięcie deploymentów
kubectl delete deployment authorization-api basket-api catalog-api communication-api document-api ordering-api -n rentharbor

# Usunięcie serwisów
kubectl delete service authorization-service basket-service catalog-service communication-service document-service ordering-service -n rentharbor

# Usunięcie ConfigMap
kubectl delete configmap authorization-api-config basket-api-config catalog-api-config communication-api-config document-api-config ordering-api-config -n rentharbor

kubectl delete -f databases/mongo-deployment.yaml -n rentharbor
kubectl delete -f databases/mongo-service.yaml -n rentharbor
kubectl delete -f databases/mongo-pv.yaml -n rentharbor

kubectl delete -f databases/postgres-deployment.yaml -n rentharbor
kubectl delete -f databases/postgres-service.yaml -n rentharbor
kubectl delete -f databases/postgres-pv.yaml -n rentharbor

kubectl delete -f databases/sqlserver-deployment.yaml -n rentharbor
kubectl delete -f databases/sqlserver-service.yaml -n rentharbor
kubectl delete -f databases/sqlserver-pv.yaml -n rentharbor


kubectl apply -f authorization/authorization-configmap.yaml -n rentharbor
kubectl apply -f basket/basket-configmap.yaml -n rentharbor
kubectl apply -f catalog/catalog-configmap.yaml -n rentharbor
kubectl apply -f communication/communication-configmap.yaml -n rentharbor
kubectl apply -f document/document-configmap.yaml -n rentharbor
kubectl apply -f ordering/ordering-configmap.yaml -n rentharbor

kubectl apply -f authorization/authorization-deployment.yaml -n rentharbor
kubectl apply -f basket/basket-deployment.yaml -n rentharbor
kubectl apply -f catalog/catalog-deployment.yaml -n rentharbor
kubectl apply -f communication/communication-deployment.yaml -n rentharbor
kubectl apply -f document/document-deployment.yaml -n rentharbor
kubectl apply -f ordering/ordering-deployment.yaml -n rentharbor

kubectl apply -f authorization/authorization-service.yaml -n rentharbor
kubectl apply -f basket/basket-service.yaml -n rentharbor
kubectl apply -f catalog/catalog-service.yaml -n rentharbor
kubectl apply -f communication/communication-service.yaml -n rentharbor
kubectl apply -f document/document-service.yaml -n rentharbor
kubectl apply -f ordering/ordering-service.yaml -n rentharbor

kubectl apply -f databases/mongo-pv.yaml -n rentharbor
kubectl apply -f databases/mongo-deployment.yaml -n rentharbor
kubectl apply -f databases/mongo-service.yaml -n rentharbor

kubectl apply -f databases/postgres-pv.yaml -n rentharbor
kubectl apply -f databases/postgres-deployment.yaml -n rentharbor
kubectl apply -f databases/postgres-service.yaml -n rentharbor

kubectl apply -f databases/sqlserver-pv.yaml -n rentharbor
kubectl apply -f databases/sqlserver-deployment.yaml -n rentharbor
kubectl apply -f databases/sqlserver-service.yaml -n rentharbor


kubectl apply -f ingress.yaml -n rentharbor


# Apply Kubernetes configurations
kubectl apply -f E:/Studia/Magisterka/RentHarbour/k8s

# Wait for a few seconds to give Kubernetes time to start the pods
sleep 30

# Get status
kubectl get pods -n rentharbor
kubectl get services -n rentharbor
