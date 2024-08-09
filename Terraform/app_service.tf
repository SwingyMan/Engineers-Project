resource "azurerm_service_plan" "example" {
  depends_on          = [azurerm_resource_group.project_engineers]
  name                = "socialplatformsp"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  os_type             = "Linux"
  sku_name            = "B1"
}

resource "azurerm_linux_web_app" "example" {
  depends_on          = [azurerm_service_plan.example]
  name                = "socialplatformwa"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  service_plan_id     = azurerm_service_plan.example.id
  https_only = true
  app_settings = {
    DOCKER_ENABLE_CI = true
  }
  virtual_network_subnet_id = azurerm_subnet.snet2.id
  identity {
    type = "UserAssigned"
    identity_ids = [azurerm_user_assigned_identity.example.id]
  }
  connection_string {
    name  = "Database"
    type  = "PostgreSQL"
    value = "Host=socialplatformser.postgres.database.azure.com;Database=socialplatformdb;Username=marcin;Password=${var.password}"
  }
  site_config {
    ftps_state = "FtpsOnly"
    always_on                     = false
    ip_restriction_default_action = "Deny"
    http2_enabled = true
    ip_restriction {
      action     = "Allow"
      ip_address = "103.21.244.0/22,103.22.200.0/22,103.31.4.0/22,104.16.0.0/13,104.24.0.0/14,108.162.192.0/18,131.0.72.0/22,141.101.64.0/18"
      name       = "Multi-source rule"
      priority   = 100
    }
    ip_restriction {
      action     = "Allow"
      ip_address = "162.158.0.0/15,172.64.0.0/13,173.245.48.0/20,188.114.96.0/20,190.93.240.0/20,197.234.240.0/22,198.41.128.0/17"
      name       = "Multi-source rule"
      priority   = 101
    }
    application_stack {
      docker_image_name = "engineers_project.server:14"
      # Replace <ACR_LOGIN_SERVER> with your ACR's login server URL

    }

  }
}