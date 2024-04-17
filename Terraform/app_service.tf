resource "azurerm_service_plan" "example" {
  depends_on = [azurerm_resource_group.project_engineers]
  name                = "socialplatformsp"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  os_type = "Linux"
  sku_name = "B1"
}

resource "azurerm_linux_web_app" "example" {
  depends_on = [azurerm_service_plan.example]
  name                = "socialplatformwa"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  service_plan_id = azurerm_service_plan.example.id
  app_settings = {
    DOCKER_ENABLE_CI = true
  }
  identity {
    type = "SystemAssigned"
  }
  connection_string {
    name  = "Database"
    type  = "PostgreSQL"
    value = "Host=socialplatformser.postgres.database.azure.com;Database=socialplatformdb;Username=marcin;Password=${var.password}"
  }
  site_config {  
    always_on = false
    application_stack {
    docker_image_name = "engineers_project.server:latest"
    # Replace <ACR_LOGIN_SERVER> with your ACR's login server URL
  }
  }
}