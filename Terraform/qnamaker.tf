resource "azurerm_cognitive_account" "example" {
  name                = "example-qnamaker"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "S0"
  kind = "QnAMaker"
}