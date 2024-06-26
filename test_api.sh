#!/bin/bash

# Create a product
curl -X POST "https://localhost:5001/api/products" -H "Content-Type: application/json" -d '{"name":"Product3","price":30}'

# Get all products
curl -X GET "https://localhost:5001/api/products"

# Get a specific product by ID
curl -X GET "https://localhost:5001/api/products/1"

# Update a product
curl -X PUT "https://localhost:5001/api/products/1" -H "Content-Type: application/json" -d '{"name":"UpdatedProduct1","price":100}'

# Delete a product
curl -X DELETE "https://localhost:5001/api/products/1"
