# Overview

This repository includes some approaches to create an offline chatbot, resulting in the final project.

All of the approaches have their own description.

## First approach: Train a model

- this includes downloading a pretrained model and fine tuning it
- test the fine tuned model in a chat application
- for details see [01_train_model](./01_train_model/README_01.md)

## Second approach: Chat with a widget

- this includes the installation and use of langflow
- the implementation of the available chat widget in a simple html page, with just a few lines of code
- for details see [03_chat_with_widget](./03_chat_with_widget/README_03.md)

## Third approach: Chat with langflow and ollama

- this includes a C# conslole app
- it uses langflow in the background to retrieve context information from the vector store
- it uses ollama with OllamaSharp to generate a response
- it uses Python.Included to use python scripts in C#
- for details see [02_chat_with_langflow_vs](./02_chat_with_langflow_vs/README_02.md)

## Final solution: Final project

- this includes the final solution
- it includes a blazor sample app that triggers the chatbot and passes the topic as parameter
- it includes a flask API with two endpoints:
  - startbot: starts the chatbot.exe and passes the topic as environment variable
  - query: the flask API retrieves the context information from the vector store based on the topic and query
- it includes a C# WPF application for the chatbot
  - it uses the flask API to retrieve the context information
  - it uses Ollama with OllamaSharp to generate a response
- for details see [FinalProject](./FinalProject/README_FP.md)

## Documentation

- a detailed documentation is available: [Documentation](./Documentation/Documentation_en.pdf)