# This script utilizes the `all-mpnet-base-v2` model from Sentence Transformers.
# The model is licensed under the Apache License 2.0.

from sentence_transformers import SentenceTransformer
model_name = "all-mpnet-base-v2"
prefix = "sentence-transformers/"
models_path = "./AI_Models/"

model = SentenceTransformer(prefix + model_name)
model.save(models_path + model_name)