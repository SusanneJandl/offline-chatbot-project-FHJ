# extract_text.py
import os
from bs4 import BeautifulSoup
import pdfplumber

def extract_text_from_pdf(filepath):
    text = ""
    with pdfplumber.open(filepath) as pdf:
        for page in pdf.pages:
            page_text = page.extract_text()
            if page_text:
                text += page_text + "\n"
    return text.strip()

def extract_text_from_html(filepath):
    with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
        html_content = f.read()
    soup = BeautifulSoup(html_content, 'html.parser')
    # Extract visible text
    text = soup.get_text(separator=' ', strip=True)
    return text.strip()

def extract_text_from_txt(filepath):
    with open(filepath, 'r', encoding='utf-8', errors='ignore') as f:
        text = f.read()
    return text.strip()

def extract_text(filepath):
    ext = os.path.splitext(filepath)[1].lower()
    if ext == '.pdf':
        return extract_text_from_pdf(filepath)
    elif ext == '.html' or ext == '.htm':
        return extract_text_from_html(filepath)
    elif ext == '.txt':
        return extract_text_from_txt(filepath)
    else:
        # Unsupported file type
        return ""
