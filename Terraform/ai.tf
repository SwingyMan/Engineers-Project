resource "azurerm_cognitive_account" "example" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformqa"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "F0"
  kind                = "TextAnalytics"
  local_auth_enabled  = false
  custom_subdomain_name = "socialplatformqa"
  network_acls {
    default_action = "Deny"
    virtual_network_rules {
      subnet_id = azurerm_subnet.snet2.id
    }
  }
  lifecycle {
    ignore_changes = [network_acls[0].ip_rules]
  }
}
resource "azurerm_cognitive_account" "moderator" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformmd"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "F0"
  kind                = "ContentSafety"
  local_auth_enabled  = false
  custom_subdomain_name = "safetext"
  network_acls {
    default_action = "Deny"
    virtual_network_rules {
      subnet_id = azurerm_subnet.snet2.id
    }
  }
  lifecycle {
    ignore_changes = [network_acls[0].ip_rules]
  }
}
resource "azurerm_cognitive_account" "translator" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformtr"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku_name            = "F0"
  kind                = "TextTranslation"
  local_auth_enabled  = false
  custom_subdomain_name = "questionsandanswers"
  network_acls {
    default_action = "Deny"
    virtual_network_rules {
      subnet_id = azurerm_subnet.snet2.id
    }
  }
  lifecycle {
    ignore_changes = [network_acls[0].ip_rules]
  }
}