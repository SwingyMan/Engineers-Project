resource "azurerm_virtual_network" "vnet" {
  name                = "socialplatformvnet"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  address_space       = ["10.0.0.0/16"]
}
resource "azurerm_subnet" "snet2" {
  address_prefixes     = ["10.0.2.0/29"]
  name                 = "snet1"
  resource_group_name  = azurerm_resource_group.project_engineers.name
  virtual_network_name = azurerm_virtual_network.vnet.name
  delegation {
    name = "delegation"

    service_delegation {
      name    = "Microsoft.Web/serverFarms"
      actions = ["Microsoft.Network/virtualNetworks/subnets/action"]
    }
  }
  service_endpoints = ["Microsoft.KeyVault","Microsoft.Storage","Microsoft.CognitiveServices"]
}
resource "azurerm_network_security_group" "example" {
  name                = "projectNSG"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
}
resource "azurerm_subnet_network_security_group_association" "example" {
  network_security_group_id = azurerm_network_security_group.example.id
  subnet_id                 = azurerm_subnet.snet2.id
}