# This script utilizes the 'all-mpnet-base-v2' model from Sentence Transformers.
# The model is licensed under the Apache License 2.0.

# This script utilizes the 'hnswlib' library, which is licensed under the Apache License 2.0.

import hnswlib
import json
from sentence_transformers import SentenceTransformer

model_name = '../AI_Models/all-mpnet-base-v2'
model = SentenceTransformer(model_name)
embedding_dim = model.get_sentence_embedding_dimension()

# Load the index
index = hnswlib.Index(space='cosine', dim=embedding_dim)

def retrieve_context(question: str, topic: str) -> list:
    """
    Gets context from vector store based on the question
    Returns a list of texts
    """

    index.load_index("../VectorStores/" + topic + "/" + topic + "_index.bin")

    # Load metadata
    with open("../VectorStores/" + topic + "/metadata.json", "r", encoding="utf-8") as f:
        metadata = json.load(f)

    query_embedding = model.encode([question], convert_to_numpy=True)

    k = 5  # number of results to retrieve
    labels, distances = index.knn_query(query_embedding, k=k)

    contexts = []
    for label_id, dist in zip(labels[0], distances[0]):
        doc_info = metadata[str(label_id)]
        contexts.append(doc_info['text'])

    return contexts
