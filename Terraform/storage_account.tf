resource "azurerm_storage_account" "example" {
  depends_on                      = [azurerm_resource_group.project_engineers]
  name                            = "socialplatformsa"
  resource_group_name             = azurerm_resource_group.project_engineers.name
  location                        = azurerm_resource_group.project_engineers.location
  account_tier                    = "Standard"
  account_replication_type        = "LRS"
  allow_nested_items_to_be_public = false
  shared_access_key_enabled = false
  default_to_oauth_authentication = true
  min_tls_version = "TLS1_2"
  # public_network_access_enabled = false
  network_rules {
    bypass         = ["AzureServices"]
    default_action = "Deny"
    virtual_network_subnet_ids = [azurerm_subnet.snet2.id]
  }
  lifecycle {
    ignore_changes = [network_rules[0].ip_rules]
  }
  identity {
    type = "SystemAssigned"
  }
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


resource "azurerm_storage_account_customer_managed_key" "ok_cmk" {
  depends_on = [azurerm_role_assignment.kv-key]
  storage_account_id = azurerm_storage_account.example.id
  key_vault_id       = azurerm_key_vault.example.id
  key_name           = azurerm_key_vault_key.storage.name
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