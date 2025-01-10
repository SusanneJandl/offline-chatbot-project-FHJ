## create vector store

Example vector stores (Dogs and Birds) for demonstration purpose are provided in this project.
To adapt for other projects the paths in python scripts have to be adapted.
The value passed from the sample app when starting the chatbot has to be the name of the vector store.

- create a virtual environment
  - `python -m venv indexing_env`
- activate virtual environment
  - `.\indexing_env\Scripts\activate`
- navigate to `.\Final_Project\CreateVS` and install requirements
  - `pip install -r requirements.txt`
- add folder with information to be indexed to `.\Final_Project\InfoSource`
  - must be same name as value passed from sample app
- update topic in indexing.py in `.\Final_Project\CreateVS`
  - `topic = "Birds"`
  - replace "Birds" with value you want to use
- create vector store
  - run `python indexing.py`
