variable "region" {
  type    = string
  default = "westeurope"
}
variable "password" {
  type      = string
  sensitive = true
}
variable "tenant" {
  type    = string
  default = "ab840be7-206b-432c-bd22-4c20fdc1b261"
}