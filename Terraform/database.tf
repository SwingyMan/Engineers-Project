resource "azurerm_postgresql_flexible_server" "example" {
  depends_on             = [azurerm_resource_group.project_engineers]
  name                   = "socialplatformser"
  location               = "polandcentral"
  resource_group_name    = azurerm_resource_group.project_engineers.name
  sku_name               = "B_Standard_B1ms"
  version                = "15"
  zone                   = "1"
  administrator_login    = "marcin"
  administrator_password = data.azurerm_key_vault_secret.db.value

  storage_mb                    = 32768 # 5GB
  backup_retention_days         = 7
  geo_redundant_backup_enabled  = false
  public_network_access_enabled = true
}

# Create PostgreSQL Database
resource "azurerm_postgresql_flexible_server_database" "example" {
  depends_on = [azurerm_postgresql_flexible_server.example]
  server_id  = azurerm_postgresql_flexible_server.example.id
  name       = "socialplatformdb"
  charset    = "UTF8"
  collation  = "en_US.utf8"
}

# Grant access to the PostgreSQL Server from Web App
resource "azurerm_postgresql_flexible_server_firewall_rule" "example" {
  depends_on       = [azurerm_postgresql_flexible_server.example]
  name             = "allow-webapp-access"
  server_id        = azurerm_postgresql_flexible_server.example.id
  start_ip_address = azurerm_linux_web_app.example.outbound_ip_address_list[0]
  end_ip_address   = azurerm_linux_web_app.example.outbound_ip_address_list[0]
}