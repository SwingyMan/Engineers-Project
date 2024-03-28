resource "azurerm_storage_account" "example" {
  depends_on = [azurerm_resource_group.project_engineers]
  name                     = "socialplatformsa"
  resource_group_name      = azurerm_resource_group.project_engineers.name
  location                 = azurerm_resource_group.project_engineers.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_storage_container" "terraform" {
  depends_on = [azurerm_storage_account.example]
  name                  = "terraform"
  storage_account_name  = azurerm_storage_account.example.name
  container_access_type = "container"
}

resource "azurerm_storage_container" "images" {
  depends_on = [azurerm_storage_account.example]
  name                  = "images"
  storage_account_name  = azurerm_storage_account.example.name
  container_access_type = "container"
}