#!/bin/bash

# مسح ملفات البنية السابقة
echo "تنظيف المشروع..."
dotnet clean

# بناء المشروع
echo "بناء المشروع..."
dotnet build -c Release

# تشغيل التطبيق
echo "تشغيل التطبيق..."
dotnet bin/Release/net8.0-windows/HRSystem.dll