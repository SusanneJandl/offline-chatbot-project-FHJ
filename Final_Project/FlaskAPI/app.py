# This script utilizes 'flask', which is licensed under the Apache License 2.0.

from flask import Flask, request, jsonify
from chatbot_trigger import run_chatbot
from context_provider import retrieve_context

app = Flask(__name__)

@app.route('/startbot', methods=['POST'])
def start_bot():
    """
    Endpoint to trigger the chatbot exe
    """
    try:
        data = request.json
        topic = data.get('topic')

        if not topic:
            return jsonify({"status": "error", "message": "Topic is required"}), 400
        
        run_chatbot(topic)
        return jsonify({"status": "success", "message": "Chatbot started successfully"})
    except Exception as e:
        return jsonify({"status": "error", "message": str(e)}), 500
    
@app.route('/query', methods=['POST'])
def get_context():
    """
    Endpoint to retrieve the context
    """
    try:
        data = request.json
        question = data.get('question')
        topic = data.get('topic')

        if not question:
            return jsonify({"status": "error", "message": "Question is required"}), 400

        if not topic:
            return jsonify({"status": "error", "message": "Topic is required"}), 400

        context = retrieve_context(question, topic)
        return jsonify({"status": "success", "context": context}), 200
    except Exception as e:
        return jsonify({"status": "error", "message": str(e)}), 500

if __name__ == '__main__':
    app.run(host = '0.0.0.0', port = 5000, debug = True)