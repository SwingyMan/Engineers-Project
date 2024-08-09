resource "azurerm_storage_account" "example" {
  depends_on               = [azurerm_resource_group.project_engineers]
  name                     = "socialplatformsa"
  resource_group_name      = azurerm_resource_group.project_engineers.name
  location                 = azurerm_resource_group.project_engineers.location
  account_tier             = "Standard"
  account_replication_type = "LRS"
  allow_nested_items_to_be_public = false
 # shared_access_key_enabled = false
  min_tls_version = "TLS1_2"
 # public_network_access_enabled = false
  queue_properties {
    logging {
      delete                = true
      read                  = true
      write                 = true
      version               = "1.0"
      retention_policy_days = 10
    }
  }
  blob_properties {
    delete_retention_policy {
      days = 7
    }
  }
  sas_policy { 
    expiration_period = "90.00:00:00"
    expiration_action = "Log"
}
}

resource "azurerm_storage_container" "images" {
  depends_on            = [azurerm_storage_account.example]
  name                  = "images"
  storage_account_name  = azurerm_storage_account.example.name
  container_access_type = "private"
}
resource "azurerm_storage_container" "gifs" {
  depends_on            = [azurerm_storage_account.example]
  name                  = "gifs"
  storage_account_name  = azurerm_storage_account.example.name
  container_access_type = "private"
}
resource "azurerm_storage_container" "videos" {
  depends_on            = [azurerm_storage_account.example]
  name                  = "videos"
  storage_account_name  = azurerm_storage_account.example.name
  container_access_type = "private"
}