# This script utilizes the 'all-mpnet-base-v2' model from Sentence Transformers.
# The model is licensed under the Apache License 2.0.

# This script utilizes the 'hnswlib' library, which is licensed under the Apache License 2.0.

# indexing.py
import os
import json
import hnswlib
from sentence_transformers import SentenceTransformer
from extract_text import extract_text
from utils import chunk_text

topic = "Birds"

# Configuration
folder_path = "../InfoSource/" + topic + "/"
model_name = "../AI_Models/all-mpnet-base-v2"
model = SentenceTransformer(model_name)
embedding_dim = model.get_sentence_embedding_dimension()

# Read and process documents
all_text_chunks = []
doc_id = 0

# Iterate through all files in the folder
for filename in os.listdir(folder_path):
    filepath = os.path.join(folder_path, filename)
    if os.path.isfile(filepath):
        text = extract_text(filepath)
        if not text.strip():
            continue  # skip empty text
        # Chunk the text
        chunks = chunk_text(text, chunk_size=100, overlap=20)
        for chunk in chunks:
            all_text_chunks.append((doc_id, filename, chunk))
            doc_id += 1

# If no chunks found, no index will be built
if not all_text_chunks:
    print("No text chunks found. Check your documents and extraction logic.")
    exit()

# Embed all chunks
texts = [item[2] for item in all_text_chunks]
embeddings = model.encode(texts, batch_size=32, convert_to_numpy=True)

# Build hnswlib index
index = hnswlib.Index(space='cosine', dim=embedding_dim)
num_elements = len(embeddings)
index.init_index(max_elements=num_elements, ef_construction=200, M=32) # higher M is more resource and more accuracy (12-48) max edges per node
index.add_items(embeddings, ids=list(range(num_elements)))
index.set_ef(100) # higher ef is richer graph index higher accuracy
index.save_index("../VectorStores/" + topic + "/" + topic + "_index.bin")

# Save metadata
metadata = {}
for i, (id_, file_name, chunk_text) in enumerate(all_text_chunks):
    metadata[i] = {"source_file": file_name, "text": chunk_text}

with open("../VectorStores/" + topic + "/metadata.json", "w", encoding="utf-8") as f:
    json.dump(metadata, f, ensure_ascii=False, indent=4)

print("Index and metadata created successfully!")
