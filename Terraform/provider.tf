terraform {
  required_providers {
    azurerm = {
      source = "hashicorp/azurerm"
      version = "3.96.0"
    }
  }
}
provider "azurerm" {
  features {}
}

resource "azurerm_resource_group" "project_engineers" {
  name     = "project-engineers"
  location = var.region
}