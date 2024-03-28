resource "azurerm_postgresql_server" "example" {
  depends_on = [azurerm_resource_group.project_engineers]
  name                = "socialplatforms"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "B_Gen5_1"
  version             = "11"
  ssl_enforcement_enabled = true
  administrator_login          = "marcin"
  administrator_login_password = var.password

    storage_mb            = 5120  # 5GB
    backup_retention_days = 7
    geo_redundant_backup_enabled  = false
}

# Create PostgreSQL Database
resource "azurerm_postgresql_database" "example" {
  depends_on = [azurerm_postgresql_server.example]
  name                = "socialplatformdb"
  resource_group_name = azurerm_resource_group.project_engineers.name
  server_name         = azurerm_postgresql_server.example.name
  charset             = "UTF8"
  collation           = "English_United States.1252"
}

# Grant access to the PostgreSQL Server from Web App
resource "azurerm_postgresql_firewall_rule" "example" {
  depends_on = [azurerm_linux_web_app.example]
  name                = "allow-webapp-access"
  resource_group_name = azurerm_resource_group.project_engineers.name
  server_name         = azurerm_postgresql_server.example.name
  start_ip_address   = azurerm_linux_web_app.example.outbound_ip_address_list[0]
  end_ip_address     = azurerm_linux_web_app.example.outbound_ip_address_list[0]
}