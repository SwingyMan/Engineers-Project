resource "azurerm_container_registry" "example" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformacr"
  resource_group_name = azurerm_resource_group.project_engineers.name
  location            = azurerm_resource_group.project_engineers.location
  sku                 = "Basic"
  admin_enabled       = true
}
resource "azurerm_role_assignment" "example" {
  depends_on           = [azurerm_linux_web_app.example, azurerm_container_registry.example]
  scope                = azurerm_container_registry.example.id
  role_definition_name = "AcrPull"
  principal_id         = azurerm_linux_web_app.example.identity.0.principal_id
}