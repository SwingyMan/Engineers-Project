variable "region" {
  type    = string
  default = "westeurope"
}
variable "password" {
  type      = string
  sensitive = true
}