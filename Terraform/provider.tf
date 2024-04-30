terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.96.0"
    }
  }
  backend "azurerm" {
    resource_group_name = "project-engineers"
    storage_account_name = "socialplatformsa"
    container_name = "terraform"
    key = "terraform.tfstate"
  }
}
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "project_engineers" {
  name     = "project-engineers"
  location = var.region
}