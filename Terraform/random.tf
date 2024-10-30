resource "random_password" "db_password" {
  length = 15
}
resource "random_password" "admin_password" {
  length = 10
}
resource "random_password" "user_password" {
  length = 10
}
