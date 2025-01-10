from transformers import pipeline

generator = pipeline("text-generation", model="../Final_Project/AI_Models/fine_tuned_Llama-3.2-1B-Instruct", tokenizer="../Final_Project/AI_Models/fine_tuned_Llama-3.2-1B-Instruct")


conversation_history = ""

while True:
    user_input = input("User: ")
    if user_input.lower() == 'exit':
        break

    # Add user input to conversation history
    conversation_history += f"User: {user_input}\nBot:"

    # Generate response with refined parameters
    response = generator(
        conversation_history,
        max_new_tokens=70,       # Increased to allow longer responses
        temperature=0.2,         # Lowered to reduce randomness
        top_k=3,                 # Reduced top_k for more focused sampling
        top_p=0.8,               # Slightly reduced for more controlled response
        repetition_penalty=1.5,  # Increased to discourage repetitive output
        pad_token_id=50256,
        do_sample=True,
        truncation=True
    )[0]['generated_text']
    
    # Extract the bot's response
    bot_response = response[len(conversation_history):].split("User:")[0].strip()
    conversation_history += f"{bot_response}\n"

    # Display the bot's response
    print("Bot:", bot_response)
