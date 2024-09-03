resource "azurerm_log_analytics_workspace" "example" {
  name                = "socialplatformla"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  sku                 = "PerGB2018"
  retention_in_days   = 30
}
resource "azurerm_application_insights" "example" {
  name                = "socialplatformai"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  workspace_id        = azurerm_log_analytics_workspace.example.id
  application_type    = "web"
  sampling_percentage = 0
}
resource "azurerm_monitor_diagnostic_setting" "appSerivce" {
  name               = "diagnostic"
  target_resource_id = azurerm_linux_web_app.example.id
  log_analytics_workspace_id = azurerm_log_analytics_workspace.example.id

  enabled_log {
    category = "AppServiceConsoleLogs"
  }
  enabled_log {
    category = "AppServiceHTTPLogs"
  }

  metric {
    category = "AllMetrics"
  }
}