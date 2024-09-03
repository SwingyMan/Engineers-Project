terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = "3.114.0"
    }
  }
  cloud {

    organization = "CaptchaCatchers"

    workspaces {
      name = "Engineers"
    }
  }
}
provider "azurerm" {
  features {}
  storage_use_azuread = true
}
data "azurerm_client_config" "current" {
}
resource "azurerm_resource_group" "project_engineers" {
  name     = "project-engineers"
  location = var.region
}