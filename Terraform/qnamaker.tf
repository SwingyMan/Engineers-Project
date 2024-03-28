resource "azurerm_cognitive_account" "example" {
  depends_on = [azurerm_resource_group.project_engineers]
  name                = "socialplatformqa"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "S"
  kind = "TextAnalytics"
}
