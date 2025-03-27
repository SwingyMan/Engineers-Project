# Engineering Project Overview  

## Description  
This project is a cloud-native application built using **.NET**, **Terraform**, and **Docker**, deployed via an automated **CI/CD pipeline** in **Azure DevOps**. It follows DevOps best practices, ensuring reliability, scalability, and security.  

## Tech Stack  
- **.NET** – Backend application built using .NET Core/.NET 6+  
- **Terraform** – Infrastructure as Code (IaC) for cloud provisioning  
- **Docker** – Containerization for environment consistency  
- **Azure DevOps** – CI/CD pipeline for automation  
- **CQRS** – Separates commands (writes) from queries (reads) to enhance performance  
- **Clean Architecture** – Enforces separation of concerns and modularity  
- **Azure Services**:  
  - **Key Vault** – Secure secrets and certificate management  
  - **Blob Storage** – Scalable object storage  
  - **VNet Integration** – Private network communication  
  - **App Service** – Web application hosting  
  - **Container Registry** – Container storage and management  
  - **Communication Service** – Messaging and notifications  
  - **SignalR** – Real-time web communication  
  - **PostgreSQL DB** – Cloud-based relational database  
  - **Managed Identity** – Secure authentication without secrets  
  - **Application Insights** – Monitoring and telemetry  
  - **Cognitive Account** – AI and cognitive services integration  
  - **Log Analytics** – Centralized logging and real-time insights via Azure Monitor  

## Repository Structure  
```plaintext
/
 ├── Terraform/   # Terraform scripts for cloud resources  
 ├── Application/ # Application layer (CQRS, business logic)  
 ├── Application.Tests/ # Application layer tests  
 ├── Domain/      # Domain entities and business rules  
 ├── Domain.Tests/ # Domain layer tests 
 ├── Infrastructure/ # Database, logging, external integrations 
 ├── Infrastructure.Tests/ # Infrastructure validation tests  
 ├── Engineers_Project.Server/        # Shared infrastructure components  
 ├── engineers_project.client/    # Frontend infrastructure setup  
 ├── Dockerfile        # Dockerfile with build definition  
 ├── pipeline/       # Azure DevOps YAML CI/CD pipeline definitions  
 ├── README.md        # Project documentation  
