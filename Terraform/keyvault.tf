
resource "azurerm_key_vault" "example" {
  name                        = "socialplatformkv"
  location                    = azurerm_resource_group.project_engineers.location
  resource_group_name         = azurerm_resource_group.project_engineers.name
  enabled_for_disk_encryption = true
  tenant_id                   = "ab840be7-206b-432c-bd22-4c20fdc1b261"
  sku_name                    = "standard"
}