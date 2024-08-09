resource "azurerm_user_assigned_identity" "example" {
  location            = azurerm_resource_group.project_engineers.location
  name                = "socialplatformmi"
  resource_group_name = azurerm_resource_group.project_engineers.name
}
resource "azurerm_role_assignment" "kv" {
  principal_id = azurerm_user_assigned_identity.example.principal_id
  role_definition_name = "Key Vault Secrets Officer"
  scope = azurerm_key_vault.example.id
}
resource "azurerm_role_assignment" "sa" {
  principal_id = azurerm_user_assigned_identity.example.principal_id
  scope        = azurerm_storage_account.example.id
  role_definition_name = "Storage Blob Data Contributor"
}
resource "azurerm_role_assignment" "acr" {
  principal_id = azurerm_user_assigned_identity.example.principal_id
  scope        = azurerm_container_registry.example.id
  role_definition_name = "AcrPull"
}