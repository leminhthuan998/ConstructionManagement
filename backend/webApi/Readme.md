# Khởi Cài đặt backend

## 1. Cài đặt sql server 2017 trở lên

## 2. Cài đặt dotnet core SDK 3.1

## 3. IDE - Visual Studio Comunity hoặc Rider

## 4. Migrate data:
0. Kiểm tra lại connection string kết nối đến sql server

1. Tạo file migration
chạy lệnh trong package manage console ```add-migration Init_Migrate```

2. Nếu đã tạo migration rồi
chạy lệnh trong package manage console ```update-database```

## 5 . Kiểm tra document api - swagger
http://localhost:5000/swagger/index.html

## account azure:
18521456@ms.uit.edu.vn
acmilan%%123

## https://docs.microsoft.com/vi-vn/learn/modules/publish-azure-web-app-with-visual-studio/
## https://docs.microsoft.com/en-us/azure/app-service/app-service-web-tutorial-dotnetcore-sqldb
--- Tạo resource group: cloud-shell-storage-southeastasia
--- Tạo hosting plan


## Tạo một instance sql server
az sql server create --name "do-an-tot-nghiep-tdtu" --resource-group "cloud-shell-storage-southeastasia" --location "Southeast Asia" --admin-user sa_constructmanagement --admin-password "Cm@123$%^208Tdtu2@18"


## enable firewall-rule sql server
az sql server firewall-rule create --resource-group "cloud-shell-storage-southeastasia" --server "do-an-tot-nghiep-tdtu" --name AllowAzureIps --start-ip-address 0.0.0.0 --end-ip-address 0.0.0.0

## tạo một database instance
az sql db create --resource-group "cloud-shell-storage-southeastasia" --server "do-an-tot-nghiep-tdtu" --name ConstructManagementDB --service-objective S0

## generate connection
az sql db show-connection-string --client ado.net --server "do-an-tot-nghiep-tdtu" --name ConstructManagementDB 


## create deploy ftp account
az webapp deployment user set --user-name sa_deploy.constructmanagement@tdtu --password sa@123$%^administrator


## enable log azure
az webapp log tail --name <app-name> --resource-group cloud-shell-storage-southeastasia

