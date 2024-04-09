resource "azurerm_dns_zone" "socialplatformdns" {
  name                = "polslsocial.pl"
  resource_group_name = azurerm_resource_group.project_engineers.name
}
resource "azurerm_dns_a_record" "example" {
  name                = "@"
  zone_name           = azurerm_dns_zone.socialplatformdns.name
  resource_group_name = azurerm_resource_group.project_engineers.name
  ttl                 = 300
  records             = [azurerm_linux_web_app.example.default_hostname]
}