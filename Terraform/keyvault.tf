
resource "azurerm_key_vault" "example" {
  depends_on                  = [azurerm_resource_group.project_engineers]
  name                        = "socialplatformkv"
  location                    = azurerm_resource_group.project_engineers.location
  resource_group_name         = azurerm_resource_group.project_engineers.name
  enabled_for_disk_encryption = true
  tenant_id                   = "ab840be7-206b-432c-bd22-4c20fdc1b261"
  sku_name                    = "standard"
  enable_rbac_authorization   = true
}
resource "azurerm_key_vault_secret" "storage" {
  key_vault_id = azurerm_key_vault.example.id
  name         = "storagekey"
  value        = azurerm_storage_account.example.primary_access_key
}
resource "azurerm_key_vault_secret" "signalr" {
  key_vault_id = azurerm_key_vault.example.id
  name         = "signalrkey"
  value        = azurerm_signalr_service.example.primary_access_key
}
resource "azurerm_key_vault_secret" "email" {
  key_vault_id = azurerm_key_vault.example.id
  name         = "emailkey"
  value        = azurerm_communication_service.example.primary_key
}
resource "azurerm_key_vault_secret" "db" {
  key_vault_id = azurerm_key_vault.example.id
  name         = "dbkey"
  value        = var.password
}
resource "azurerm_key_vault_secret" "ai" {
  key_vault_id = azurerm_key_vault.example.id
  name         = "aikey"
  value        = azurerm_cognitive_account.example.primary_access_key
}
resource "azurerm_key_vault_secret" "ai" {
  key_vault_id = azurerm_key_vault.example.id
  name         = "translatorkey"
  value        = azurerm_cognitive_account.translator.primary_access_key
}
resource "azurerm_key_vault_secret" "ai" {
  key_vault_id = azurerm_key_vault.example.id
  name         = "moderatorkey"
  value        = azurerm_cognitive_account.moderator.primary_access_key
}