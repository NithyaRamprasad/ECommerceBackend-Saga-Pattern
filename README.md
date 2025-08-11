# ECommerceBackend-Saga-Pattern
This repository implements a microservices-based system for product registration and borrowing/buying requests, coordinated using the Saga Pattern with Kafka as the message broker.

The system consists of three main services:

Saga Orchestrator – Coordinates distributed transactions and approval processes.
Product Service – Manages product registration and updates.
Request Service – Handles borrow/buy requests and their approval status.
