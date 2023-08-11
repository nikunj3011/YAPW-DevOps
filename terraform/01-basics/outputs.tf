resource "aws_iam_user" "my_iam_user" {
    name = "my_iam_user_abc_updated"
}

output "my_s3_bucket_versioning" {
    value = aws_s3_bucket.my_s3_bucket.versioning[0].enabled
}

output "my_s3_bucket_complete_details" {
    value = aws_s3_bucket.my_s3_bucket
}

output "my_iam_user_complete_details" {
    value = aws_iam_user.my_iam_user
}
# resource "aws_iam_user" "my_iam_user" {
#     name = "my_iam_user_abc_updated"
# }
