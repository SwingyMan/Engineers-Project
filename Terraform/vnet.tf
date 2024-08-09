resource "azurerm_virtual_network" "vnet" {
  name                = "socialplatformvnet"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  address_space       = ["10.0.0.0/16"]
}
resource "azurerm_subnet" "snet" {
  address_prefixes     = ["10.0.1.0/24"]
  name                 = "snet"
  resource_group_name  = azurerm_resource_group.project_engineers.name
  virtual_network_name = azurerm_virtual_network.vnet.name
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
      actions = ["Microsoft.Network/virtualNetworks/subnets/join/action", "Microsoft.Network/virtualNetworks/subnets/prepareNetworkPolicies/action"]
    }
  }
}
resource "azurerm_private_dns_zone" "sa" {
  name                = "privatelink.blob.core.windows.net"
  resource_group_name = azurerm_resource_group.project_engineers.name
}
resource "azurerm_private_dns_zone_virtual_network_link" "sa" {
  name                  = "sa"
  resource_group_name   = azurerm_resource_group.project_engineers.name
  private_dns_zone_name = azurerm_private_dns_zone.sa.name
  virtual_network_id    = azurerm_virtual_network.vnet.id
}
resource "azurerm_private_dns_zone" "db" {
  name                = "privatelink.postgres.database.azure.com"
  resource_group_name = azurerm_resource_group.project_engineers.name
}
resource "azurerm_private_dns_zone_virtual_network_link" "db" {
  name                  = "db"
  resource_group_name   = azurerm_resource_group.project_engineers.name
  private_dns_zone_name = azurerm_private_dns_zone.db.name
  virtual_network_id    = azurerm_virtual_network.vnet.id
}
resource "azurerm_private_dns_zone" "kv" {
  name                = "privatelink.vaultcore.azure.net"
  resource_group_name = azurerm_resource_group.project_engineers.name
}
resource "azurerm_private_dns_zone_virtual_network_link" "kv" {
  name                  = "kv"
  resource_group_name   = azurerm_resource_group.project_engineers.name
  private_dns_zone_name = azurerm_private_dns_zone.kv.name
  virtual_network_id    = azurerm_virtual_network.vnet.id
}
resource "azurerm_private_endpoint" "sa" {
  location            = azurerm_resource_group.project_engineers.location
  name                = "pe-sa"
  resource_group_name = azurerm_resource_group.project_engineers.name
  subnet_id           = azurerm_subnet.snet.id
  private_service_connection {
    is_manual_connection = false
    name                 = "pe-sa"
    private_connection_resource_id = azurerm_storage_account.example.id
    subresource_names              = ["blob"]
  }
  private_dns_zone_group {
    name                 = "dns-sa"
    private_dns_zone_ids = [azurerm_private_dns_zone.sa.id]
  }
}
resource "azurerm_private_endpoint" "db" {
  location            = azurerm_resource_group.project_engineers.location
  name                = "pe-db"
  resource_group_name = azurerm_resource_group.project_engineers.name
  subnet_id           = azurerm_subnet.snet.id
  private_service_connection {
    is_manual_connection           = false
    name                           = "pe-db"
    private_connection_resource_id = azurerm_postgresql_flexible_server.example.id
    subresource_names              = ["postgresqlServer"]
  }
  private_dns_zone_group {
    name                 = "dns-db"
    private_dns_zone_ids = [azurerm_private_dns_zone.db.id]
  }
}
  resource "azurerm_private_endpoint" "kv" {
    location            = azurerm_resource_group.project_engineers.location
    name                = "pe-kv"
    resource_group_name = azurerm_resource_group.project_engineers.name
    subnet_id           = azurerm_subnet.snet.id
    private_service_connection {
      is_manual_connection           = false
      name                           = "pe-kv"
      private_connection_resource_id = azurerm_key_vault.example.id
      subresource_names              = ["vault"]
    }
    private_dns_zone_group {
      name                 = "dns-kv"
      private_dns_zone_ids = [azurerm_private_dns_zone.kv.id]
    }
  }