from transformers import AutoTokenizer, AutoModelForCausalLM, Trainer, TrainingArguments
from datasets import load_dataset
import torch

models_path = "../Final_Project/AI_Models/"
model_source = "Llama-3.2-1B-Instruct"
target_path = models_path + "fine_tuned_" + model_source

class CustomTrainer(Trainer):
    def compute_loss(self, model, inputs, **kwargs):
        # Forward pass
        outputs = model(**inputs)
        logits = outputs.get("logits")
        labels = inputs.get("input_ids")
        
        # Shift labels to the right
        shift_logits = logits[..., :-1, :].contiguous()
        shift_labels = labels[..., 1:].contiguous()
        
        # Compute the loss
        loss_fct = torch.nn.CrossEntropyLoss()
        loss = loss_fct(shift_logits.view(-1, shift_logits.size(-1)), shift_labels.view(-1))
        return loss
    
source_path = models_path + model_source
tokenizer = AutoTokenizer.from_pretrained(source_path)
tokenizer.pad_token = tokenizer.eos_token  # Set padding token
model = AutoModelForCausalLM.from_pretrained(source_path)

# Load the dataset
dataset = load_dataset("json", data_files="simple_data.json")

# Tokenize the dataset
def preprocess_function(examples):
    inputs = [f"User: {q}\nBot: {a}" for q, a in zip(examples['user'], examples['bot'])]
    return tokenizer(inputs, padding=True, truncation=True)

tokenized_dataset = dataset.map(preprocess_function, batched=True)

training_args = TrainingArguments(
    output_dir=target_path,
    num_train_epochs=10,
    per_device_train_batch_size=2,
    save_total_limit=2,
)

trainer = CustomTrainer(
    model=model,
    args=training_args,
    train_dataset=tokenized_dataset["train"],
)

trainer.train()

model.save_pretrained(target_path)
tokenizer.save_pretrained(target_path)
