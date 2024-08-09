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
}

resource "azurerm_resource_group" "project_engineers" {
  name     = "project-engineers"
  location = var.region
}