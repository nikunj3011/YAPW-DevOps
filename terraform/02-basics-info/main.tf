variable "iam_user_name_prefix" {
    type = string #any, number, bool, list, map, set, object, tuple
    default = "my_iam_user" #ask in console if default is null also possible with environment variable
}
provider "aws" {
    region = "us-east-1"
    //version = "~> 2.46" (No longer necessary)
}

resource "aws_iam_user" "my_iam_user" {
    count = 3
    name = "${var.iam_user_name_prefix}_${count.index}"
}