resource "azurerm_role_assignment" "kv" {
  principal_id         = azurerm_linux_web_app.example.identity[0].principal_id
  role_definition_name = "Key Vault Secrets User"
  scope                = azurerm_key_vault.example.id
}
resource "azurerm_role_assignment" "sa" {
  principal_id         = azurerm_linux_web_app.example.identity[0].principal_id
  scope                = azurerm_storage_account.example.id
  role_definition_name = "Storage Blob Data Contributor"
}
resource "azurerm_role_assignment" "acr" {
  principal_id         = azurerm_linux_web_app.example.identity[0].principal_id
  scope                = azurerm_container_registry.example.id
  role_definition_name = "AcrPull"
}
resource "azurerm_role_assignment" "content" {
  principal_id = azurerm_linux_web_app.example.identity[0].principal_id
  scope        = azurerm_cognitive_account.example.id
  role_definition_name = "Cognitive Services User"
}
resource "azurerm_role_assignment" "moderator" {
  principal_id = azurerm_linux_web_app.example.identity[0].principal_id
  scope        = azurerm_cognitive_account.moderator.id
  role_definition_name = "Cognitive Services User"
}
resource "azurerm_role_assignment" "translator" {
  principal_id = azurerm_linux_web_app.example.identity[0].principal_id
  scope        = azurerm_cognitive_account.translator.id
  role_definition_name = "Cognitive Services User"
}
resource "azurerm_role_assignment" "signalr" {
  principal_id = azurerm_linux_web_app.example.identity[0].principal_id
  scope        = azurerm_signalr_service.example.id
  role_definition_name = "SignalR App Server"
}