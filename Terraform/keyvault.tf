
resource "azurerm_key_vault" "example" {
  depends_on                  = [azurerm_resource_group.project_engineers]
  name                        = "socialplatformkv"
  location                    = azurerm_resource_group.project_engineers.location
  resource_group_name         = azurerm_resource_group.project_engineers.name
  enabled_for_disk_encryption = true
  tenant_id                   = var.tenant
  sku_name                    = "standard"
  enable_rbac_authorization   = true
  # public_network_access_enabled = false
  purge_protection_enabled = true
  network_acls {
    bypass         = "AzureServices"
    default_action = "Deny"
    virtual_network_subnet_ids = [azurerm_subnet.snet2.id]
    ip_rules = ["79.124.116.245/32","83.30.0.0/16","95.160.141.51/32"]
  }

}
resource "azurerm_key_vault_secret" "signalr" {
  key_vault_id    = azurerm_key_vault.example.id
  name            = "signalrkey"
  value           = azurerm_signalr_service.example.primary_access_key
  expiration_date = "2025-03-02T15:04:05Z"

}
resource "azurerm_key_vault_secret" "db" {
  key_vault_id    = azurerm_key_vault.example.id
  name            = "dbkey"
  value           = var.password
  expiration_date = "2025-03-02T15:04:05Z"

}

resource "azurerm_key_vault_secret" "app_insights" {
  key_vault_id    = azurerm_key_vault.example.id
  name            = "insightskey"
  value           = azurerm_application_insights.example.connection_string
  expiration_date = "2025-03-02T15:04:05Z"

}