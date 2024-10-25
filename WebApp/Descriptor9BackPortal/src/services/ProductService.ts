import { SaveProductData } from "../types/SaveProductData";

const BASE_URL =
  "https://dscr-ai-sw-001-gegda9egfnapd3da.swedencentral-01.azurewebsites.net";

export async function getAll() {
  const response = await fetch(`${BASE_URL}/api/products`);
  return response.json();
}

export async function saveProduct(formData: FormData) {
  const response = await fetch(`${BASE_URL}/api/products`, {
    method: "POST",
    body: formData,
  });
  return response.json();
}

export async function updateProducts(products: SaveProductData) {
  const response = await fetch(`${BASE_URL}/api/products`, {
    method: "PUT",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(products)
  });
  return response.json();
}

export async function aiDescriptor(formData: FormData) {
  const response = await fetch(`${BASE_URL}/api/descriptor`, {
    method: "POST",
    body: formData,
  });
  return response.json() as unknown as SaveProductData;
}

export async function aiDescriptorBulk(formData: FormData) {
  const response = await fetch(`${BASE_URL}/api/descriptor/bulk`, {
    method: "POST",
    body: formData,
  });
  return response.json() as unknown as SaveProductData;
}