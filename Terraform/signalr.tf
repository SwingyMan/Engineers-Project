resource "azurerm_signalr_service" "example" {
  name                = "example-signalr"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku {
    name     = "Free_F1"
    capacity = 1
  }
}
