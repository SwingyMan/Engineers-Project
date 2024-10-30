
resource "azurerm_key_vault" "example" {
  depends_on                  = [azurerm_resource_group.project_engineers]
  name                        = "socialplatformkv"
  location                    = azurerm_resource_group.project_engineers.location
  resource_group_name         = azurerm_resource_group.project_engineers.name
  enabled_for_disk_encryption = true
  tenant_id                   = data.azurerm_client_config.current.tenant_id
  sku_name                    = "standard"
  enable_rbac_authorization   = true
  # public_network_access_enabled = false
  purge_protection_enabled = true
  network_acls {
    bypass         = "AzureServices"
    default_action = "Deny"
    virtual_network_subnet_ids = [azurerm_subnet.snet2.id]
  }
lifecycle {
  ignore_changes = [network_acls[0].ip_rules]
}
}
resource "azurerm_key_vault_key" "storage" {
  name         = "storagekey"
  key_vault_id = azurerm_key_vault.example.id
  key_type     = "RSA"
  key_size = "4096"
  key_opts = [
    "decrypt",
    "encrypt",
    "sign",
    "unwrapKey",
    "verify",
    "wrapKey",
  ]
  expiration_date = "2025-03-02T15:04:05Z"
}
resource "azurerm_key_vault_key" "jwt" {
  name         = "jwtkey"
  key_vault_id = azurerm_key_vault.example.id
  key_type     = "RSA"
  key_size = "4096"
  key_opts = [
    "decrypt",
    "encrypt",
    "sign",
    "unwrapKey",
    "verify",
    "wrapKey",
  ]
  expiration_date = "2025-03-02T15:04:05Z"
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
  value           = random_password.db_password.result
  expiration_date = "2025-03-02T15:04:05Z"
}

resource "azurerm_key_vault_secret" "app_insights" {
  key_vault_id    = azurerm_key_vault.example.id
  name            = "insightskey"
  value           = azurerm_application_insights.example.connection_string
  expiration_date = "2025-03-02T15:04:05Z"

}
resource "azurerm_key_vault_secret" "admin_password" {
  key_vault_id    = azurerm_key_vault.example.id
  name            = "adminpassword"
  value           = random_password.admin_password.result
  expiration_date = "2025-03-02T15:04:05Z"

}
resource "azurerm_key_vault_secret" "user_password" {
  key_vault_id    = azurerm_key_vault.example.id
  name            = "userpassword"
  value           = random_password.user_password.result
  expiration_date = "2025-03-02T15:04:05Z"

}