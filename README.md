# VCard Service Project

## Overview

This project implements a `VCardService` class in the `VCardProject.Implementations` namespace. The service allows users to convert user data obtained from the RandomUser.me API into VCard format. Users can specify the number of VCards they want to create, and the service generates unique VCard files for each user.

## How to Use

1. **Initialize the VCardService:**

    ```csharp
    VCardService vCardService = new VCardService();
    ```

2. **Invoke the `ConvertObjectToVCardAsync` Method:**

    ```csharp
    List<string> createdFiles = await vCardService.ConvertObjectToVCardAsync();
    ```

    - The method prompts the user to enter the count of VCards to create.
    - It fetches user data from the RandomUser.me API for the specified count.
    - It converts the user data into VCard format and writes it to unique files.
    - The method returns a list of file names of the created VCard files.

3. **VCard Files:**

    - The created VCard files are saved in the temporary directory with unique file names.
    - File names are displayed in the console for each successfully created VCard.

## Dependencies

- Newtonsoft.Json
- System.Net.Http
- System.IO

## Error Handling

- The service includes basic error handling for API requests and file writing.
- In case of an error, a default VCard string is added to the list of created files.
