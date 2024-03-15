resource "azurerm_app_service_plan" "example" {
  name                = "example-appservice-plan"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  kind                = "Linux"
  reserved            = true

  sku {
    tier = "Basic"
    size = "B1"
  }
}

resource "azurerm_linux_web_app" "example" {
  name                = "example-webapp"
  location            = azurerm_resource_group.project_engineers.location
  resource_group_name = azurerm_resource_group.project_engineers.name
  service_plan_id = azurerm_app_service_plan.example.id


  site_config {  
    application_stack {
    docker_image_name = "<ACR_LOGIN_SERVER>/engineers_project.server:latest"
    # Replace <ACR_LOGIN_SERVER> with your ACR's login server URL
  }
  }
}