resource "azurerm_container_registry" "example" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformacr"
  resource_group_name = azurerm_resource_group.project_engineers.name
  location            = azurerm_resource_group.project_engineers.location
  sku                 = "Standard"
  admin_enabled       = true
}