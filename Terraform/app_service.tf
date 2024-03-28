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
  identity {
    type = "SystemAssigned"
  }

  site_config {  
    application_stack {
    docker_image_name = "socialplatformacr.azurecr.io/engineers_project.server:latest"
    # Replace <ACR_LOGIN_SERVER> with your ACR's login server URL
  }
  }
}