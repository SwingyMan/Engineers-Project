resource "azurerm_cognitive_account" "example" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformqa"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "S"
  kind                = "TextAnalytics"
}
resource "azurerm_cognitive_account" "moderator" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformmd"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "S"
  kind                = "ContentModerator"
}
resource "azurerm_cognitive_account" "translator" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformtr"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "S"
  kind                = "TextTranslation"
}