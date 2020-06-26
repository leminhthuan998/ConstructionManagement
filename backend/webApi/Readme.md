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


