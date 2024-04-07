resource "azurerm_communication_service" "example" {
  name                = "socialplatformcs"
  resource_group_name = azurerm_resource_group.project_engineers.name
  data_location       = "Europe"
}