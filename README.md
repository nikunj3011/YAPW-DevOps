**YAPW DevOps**

[Azure Devops Board](https://dev.azure.com/nikunjrathod3011/YAPW/_workitems/recentlyupdated)

**Idea:** 

Something similar to youtube but only for watching all movie trailers

**Key Technologies:**

**TD:LR:**

Create software(microservice api) -> deploy(github, azure devops) -> build (k8s, docker) -> provision server(terraform) -> cloud management -> configure software (ansible/chef/puppet) -> deploy app (jenkins/azure devops) -> monitor

**PLANNING**

Jira

**DATABASE DESIGN – CODE FIRST**

SQL Server and MySQL

**CODE**

Asp.Net Core 7 API EFCORE DOMAIN Driven 

GraphQL

Java Spring 6 API

Microservices – 2 ASP and 1 Java

Asp.Net Core 7 Website Javascript JQuery

Service Brokers (Trigger DB update) 

**DEPLOY**

Github, GIT and Github Actions

**ORCHESTRATION AND AUTOMATION**

Docker

Kubernetes (Helm)

Azure Pipelines

Jenkins

**IAAC**

Terraform

- AWS
- Azure

**CLOUD**

Azure

AWS

Serverless

Functions

**CONFIG MANAGEMENT**

Ansible

Chef

Puppet

**PERFORMANCE METRICS AND EVENT MONITORING** 

SEQ

DataDog

Splunk


**TODOS:** 

Data Engineering

AI

ML

BI

Open Id and Identity server

Generative AN

Cloudflare

DATABASE DESIGN – CODE FIRST

SQL Server – Code first by creating models, testing all done and ef core   

MySQL – Db transferred to MySQL using Pomelo's MySQL database provider for Entity Framework Core.

![A diagram of a computer

Description automatically generated](images/databaseDiagram.jpg)

**CODE**

Asp.Net Core 7 API EFCORE DOMAIN Driven 

ASP Core API:

Singleton: These services are created once and when you request (call or use or refer) these services, the same instance is used. this means there is no new instance of service, and same instance is used everywhere in the application. Transient: These services are created every time that service is requested. imagine you inject OrderService into OrderController and you inject IdGeneratorService in both. When OrderController receive the request, an IdGeneratorService instance is created and use in OrderController and another is created and use in OrderService (2 instances created for a request) Scope: These services are created only once per each request (scope), and be reuse multiple times within the request.to explain better, imagine you inject OrderService into OrderController and we inject IdGeneratorService in both. When OrderController receive request, an IdGeneratorService instance is created and use in controller and same is use in OrderService. this hierarchy can continue if there is another service inside OrderService.

GraphQL

Java Spring 6 API

Microservices – 2 ASP and 1 Java

Asp.Net Core 7 Website Javascript JQuery

Service Brokers (Trigger DB update) 

**DEPLOY**

Github, GIT and Github Actions

**ORCHESTRATION AND AUTOMATION**

Docker

Kubernetes

Azure Pipelines

Jenkins

**IAAC**

Terraform

- AWS
- Azure

**CLOUD**

Azure

AWS

- Resource group
- Key vault
- Storage account
- AKS
- Identity Service
- SQL Databases
- Virtual Machines

Serverless

Functions

**CONFIG MANAGEMENT**

Ansible

Chef

Puppet

**PERFORMANCE METRICS AND EVENT MONITORING** 

SEQ

DataDog

Splunk
