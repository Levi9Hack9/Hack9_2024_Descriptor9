# Hack9_2024_Descriptor9
DescriptorAI: Writing descriptions for images or products can be a tedious and uninspiring task, especially when dealing with a large volume of items. This is where AI steps in to provide quick, aesthetically pleasing, and precise solutions. With AI, you not only save time and effort but also ensure consistency across descriptions.

## Project Overview

### Theme and Objectives

#### Project Introduction
Descriptor9 is a BackOffice application built to manage a range of products and accessories, such as lanyards, in an efficient and scalable way. The project integrates GenAI to streamline the creation and updating of product data, offering a modern solution for product management.

#### Problem Statement
Managing product information across a large inventory can be time-consuming and prone to inconsistencies, especially for companies handling a variety of accessories and customizations. Manually creating and updating product details is not only labor-intensive but also challenging to scale, leading to potential inaccuracies in descriptions and specifications. Descriptor9 addresses this by leveraging AI to automate and enhance the product description process, ensuring consistency, quality, and accuracy.

## Solution Approach

### AI and Technical Details

#### AI Models and Techniques
Descriptor9 utilizes Azure OpenAI Service, specifically the GPT-4 model, chosen for its advanced language generation capabilities as well as its ability to process and understand images. This image-reading feature enhances Descriptor9 by enabling it to analyze visual content associated with products, generating descriptions that reflect both textual data and visual attributes. This dual capability significantly streamlines product data management, ensuring that generated descriptions are highly relevant and consistent across a wide range of products and accessories.

The application consists of two main pages:

1. **All Products Page**: This page displays a comprehensive list of all products. Users can click on any product to view its details, and a URL link will initiate a modal for uploading a CSV file for bulk updates. The modal supports only CSV files, which must contain the following columns: Title, Colors, Widths, Clips, Metal Closure, Logo Print, and Logo.

2. **Add New Product Page**: Here, users can enter the title, select a category, and upload an image. After this, an AI button is available, which, when clicked, will provide translations for the entered product details.

## Technologies Used

### AI Technologies
- **AI Models**: Azure OpenAI GPT-4 model
- **Libraries/Frameworks**: Azure OpenAI SDK (for seamless integration of AI functionalities in .NET)

### Non-AI Technologies
- **Backend**: .NET 8, used for building a secure and scalable backend API.
- **Database**: In-Memory Entity Framework, chosen to showcase AI capabilities within a simplified, transient data storage setting, making it ideal for demoing AI usage without a permanent database.
- **Frontend**: React, providing a dynamic, interactive interface for managing product data and descriptions.
- **Cloud/Hosting**: Azure App Service (backend), Azure Blob Storage (for frontend hosting as a static website), enabling fast performance, scalability, and ease of deployment.

  ## Future and Next Steps

### Potential Improvements

#### Additional Features
Future versions of Descriptor9 could include support for additional file types and more complex file structures, allowing the system to process a wider variety of product data formats and types. This would enhance flexibility for users managing diverse inventories.

#### Integrations
Adding integrations with third-party APIs, such as e-commerce platforms, inventory management systems, and ERP tools, would streamline Descriptor9’s functionality and provide a unified experience for users across platforms.

#### Optimizations
To further improve Descriptor9’s performance, we plan to optimize AI processing for better handling of complex files and to incorporate additional metadata fields for products. This would allow for even more accurate and detailed product descriptions, elevating Descriptor9’s ability to manage large inventories with a high degree of specificity and usability.

### Online Access 
[Live App](https://descriptoraiapp.z1.web.core.windows.net/)

## Setup and Installation

### Instructions for Running the Frontend

1. **Clone the Frontend Repository**  
   Run the following command to clone the repository:
   ```bash
   git clone https://github.com/Levi9Hack9/Hack9_2024_Descriptor9.git

2. **Move to the Frontend Directory**
   Navigate to the frontend directory:

   ```bash
   cd WebApp/Descriptor9BackPortal
3. **Install Dependencies**
   Install the required frontend dependencies:

   ```bash
   npm install  # Install frontend dependencies
4. **Run the Frontend Project**
   Start the frontend project with the following command:

   ```bash
   npm run start

   ### Instructions for Running the Backend

#### Setup and Configuration

1. **Configure `appsettings.json`**
   
   Set up your `appsettings.json` file with the following configuration:
   
   ```json
   {
     "ConnectionStrings": {
       "AzureStorageConnection": "DefaultEndpointsProtocol=https;AccountName=proddescriptorstorage;AccountKey=P3uFDHjIsApDjoEoPefRttoH4/9nDvazjTExxjOFUJ0M34rDPhPxO/sUKsrAw4AZik5m5lnv4rdw+AStacVahA==;EndpointSuffix=core.windows.net"
     },
     "BlobStorage": {
       "ContainerName": "product-images"
     },
     "OpenAI": {
       "ApiKey": "1f5125431b704fdf90b85bbfc0832b2f",
       "Endpoint": "https://product-descriptor-ai.openai.azure.com/openai/deployments/gpt-4o/chat/completions?api-version=2024-08-01-preview"
     },
     "Logging": {
       "LogLevel": {
         "Default": "Information",
         "Microsoft.AspNetCore": "Warning"
       }
     },
     "AllowedHosts": "*"
   }

2. **Position Yourself in the Project Directory**  
   Navigate to the `DescriptorGeneratorAPI` directory:
   ```bash
   cd Services/DescriptorGeneratorAPI
3. **Open the Solution in Visual Studio**  
   Open `DescriptorGeneratorAPI.sln` in Visual Studio Professional.

4. **Run the Backend Project**  
   In Visual Studio, select the project and click the "Start" button to launch the backend application.

5. **Access Swagger Documentation**  
   Once the service is running, append `/swagger` to the localhost URL in your browser (e.g., `http://localhost:<port>/swagger`) to access the Swagger UI. This interface allows you to explore and test the available endpoints for the backend API.

