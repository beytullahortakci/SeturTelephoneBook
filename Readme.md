# Telefon Rehberi Mikroservis Projesi

## Genel

Bu proje, kişi ve iletişim bilgilerini yöneten, ayrıca konuma göre raporlar oluşturan mikroservislerden oluşmaktadır. Servisler arasında Rabbitmq tabanlı asenkron iletişim vardır.

## Teknolojiler

- .NET Core 9
- MongoDB
- RabbitMq (Message Queue)
- EF Core / MongoDB driver
- xUnit ve Moq (Unit Test)


## Servisler

### ContactService

- Kişi ve iletişim bilgisi CRUD işlemleri
- REST API endpoints
- Veritabanı migration yapısı

### ReportService

- Rapor talebi API
- RabbitMq ile asenkron rapor üretimi
- Rapor listesi ve detay API

## Çalıştırma

1. RabbitMQ / veritabanları için Docker Compose kullanarak ortamı ayağa kaldırın.
2. Her servisin kendi klasöründe:

```bash
dotnet ef database update
dotnet run