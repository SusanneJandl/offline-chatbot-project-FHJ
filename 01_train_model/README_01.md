## Download and train a model

### Install dependencies

- install Python from [https://www.python.org](https://www.python.org/downloads/) if necessary
  - version 3.12.7 used in this project
- create a virtual environment
  - `python -m venv training_env`
- activate virtual environment
  - `.\training_env\Scripts\activate`
- navigate to `.\04_train_model` and install requirements
  - `pip install -r requirements.txt`

### Download a pretrained model

- navigate to `.\04_train_model`
- run `python 01_download_model.py`

### Train the model

- training data for this example is provided in `simple_data.json`
- navigate to `.\04_train_model`
- run `python 02_train_model.py`

### Test the model

- navigate to `.\04_train_model`
- run `python 03_simple_chat.py`
- Enter a question represented in the training data
