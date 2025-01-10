from transformers import AutoModelForCausalLM, AutoTokenizer

model_name = "Llama-3.2-1B-Instruct"
prefix = "meta-llama"
models_path = "../Final_Project/AI_Models/"
model = AutoModelForCausalLM.from_pretrained(prefix + "/" + model_name)
tokenizer = AutoTokenizer.from_pretrained(prefix + "/" + model_name)

# Save model and tokenizer locally
model.save_pretrained(models_path + model_name)
tokenizer.save_pretrained(models_path + model_name)
