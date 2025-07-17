# RabbitMQ Hello World (Docker + .NET)

This is a minimal **RabbitMQ Hello World** demo using:

- ASP.NET Core (.NET 8)
- `RabbitMQ.Client`
- Docker + Docker Compose

The project contains:
- 🟩 `Producer`: Sends a "Hello World" message to a queue
- 🟦 `Consumer`: Listens on the queue and prints the message
- 🐇 `RabbitMQ`: Runs locally in a container with the management UI enabled

---

## 🛠️ Project Structure
.
├── docker-compose.yml
├── Producer/
│ ├── Program.cs
│ └── Dockerfile
├── Consumer/
│ ├── Program.cs
│ └── Dockerfile
└── README.md


---

## 🚀 Getting Started


### Prerequisites

- .NET 8 SDK
- Docker Desktop
- Git

### Run the App

bash
git clone https://github.com/calle192/calle192.github.io
cd calle192.github.io
docker-compose up --build

---

Visit the RabbitMQ UI at: http://localhost:15672
Login: guest | Password: guest

---

💡 Environment Variables
Variable	Default	Purpose
RABBITMQ_HOST	rabbitmq	Docker service name
RABBITMQ_PORT	5672	AMQP port
RABBITMQ_USER	guest	RabbitMQ username
RABBITMQ_PASS	guest	RabbitMQ password

---
📌 Notes
Producer sends "Hello World" and exits

Consumer listens and logs the message

RabbitMQ dashboard is available for inspection
