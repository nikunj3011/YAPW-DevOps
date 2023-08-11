variable "names" {
    default = ["a","b"]
}
provider "aws" {
    region = "us-east-1"
    //version = "~> 2.46" (No longer necessary)
}

resource "aws_iam_user" "my_iam_user" {
    # count = length(var.names)
    # name = var.names[count.index]
    for_each = toset(var.names)
    name = each.value
}