# .NET OCR with Tesseract

This repository showcases a simple .NET console application leveraging the power of the [Tesseract](https://github.com/tesseract-ocr/tesseract) OCR engine, wrapped using the Tesseract NuGet package, to extract text content from images.

## Objective

The main goal was to investigate the feasibility of implementing OCR using .NET, specifically targeting the extraction of "greentext" styled content from images.

## Purpose

This repository serves as a code sample and a proof-of-concept (POC) for implementing OCR in a .NET environment. It's a foundational step for potential larger projects in the future.

## Processing Rules

The application processes the OCR-extracted text based on the following rules:

1. Split the content on every double newline (`\n\n`).
2. Replace all subsequent newline characters (`\n`) with nothing.
3. Replace the character `|` with `I`.
4. Ensure every line or sentence starts with `>`.

## Web API

I added a web API in the form of C# minimal API.

Currently, hasn't been dockerized yet. Will be in the future.

## Usage

The OCR service is exposed via a HTTP POST endpoint at `/ocr`.

- **Endpoint:** `/ocr`
- **Method:** POST
- **Parameters:** 
    - `imageFile` (form-data): The image file to process.

### Example Request

Using a tool like `curl`, you can send a request to the service as follows:

```bash
curl -X POST -H "Content-Type: multipart/form-data" -F "imageFile=@path_to_your_image.jpg" http://localhost:5075/ocr
```

Replace path_to_your_image.jpg with the path to the image file you want to process.

### Response
The service will respond with the OCR processed text lines if the image processing is successful, or an error message if there is an issue with the request or processing.

## Learnings

While the application works decently, OCR extraction is not always perfect. Real-world application would likely require user intervention to validate and correct the extracted text.

A potential workflow for future projects could be:

1. User uploads an image.
2. The image is sent to the OCR service.
3. OCR service returns the extracted text.
4. Display the text to the user in an easily editable format.
5. The user can make corrections, if necessary, and then submit.

This workflow ensures that inaccuracies in OCR extraction can be manually corrected by the user.

---

Feedback, contributions, and suggestions are always welcome! Dive in and explore the power of OCR with .NET.
