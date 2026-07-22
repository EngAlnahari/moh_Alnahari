import os
import urllib.request
import re

# Create libs directory if it doesn't exist
libs_dir = r"c:\Users\ibrahim\Desktop\hhh\libs"
if not os.path.exists(libs_dir):
    os.makedirs(libs_dir)

# Asset URLs and target paths
assets = {
    "jquery.min.js": "https://cdn.jsdelivr.net/npm/jquery@3.6.4/dist/jquery.min.js",
    "bootstrap.rtl.min.css": "https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.rtl.min.css",
    "bootstrap.bundle.min.js": "https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js"
}

print("Downloading external libraries...")
for filename, url in assets.items():
    filepath = os.path.join(libs_dir, filename)
    print(f"Downloading {url} -> {filepath}")
    try:
        urllib.request.urlretrieve(url, filepath)
        print("Success!")
    except Exception as e:
        print(f"Failed to download {url}: {e}")

# HTML replacements map (handle both code.jquery.com and libs/jquery.min.js/etc if already done)
# We will check if the files reference libraries correctly
project_dir = r"c:\Users\ibrahim\Desktop\hhh"
print("\nVerifying/updating HTML files to use local libraries...")
for filename in os.listdir(project_dir):
    if filename.endswith(".html"):
        filepath = os.path.join(project_dir, filename)
        with open(filepath, "r", encoding="utf-8") as f:
            content = f.read()

        original_content = content
        
        # Replace CDN links if any remain
        content = re.sub(r"https://code\.jquery\.com/jquery-3\.6\.4\.min\.js", "libs/jquery.min.js", content)
        content = re.sub(r"https://cdn\.jsdelivr\.net/npm/bootstrap@5\.3\.3/dist/css/bootstrap\.rtl\.min\.css", "libs/bootstrap.rtl.min.css", content)
        content = re.sub(r"https://cdn\.jsdelivr\.net/npm/bootstrap@5\.3\.3/dist/js/bootstrap\.bundle\.min\.js", "libs/bootstrap.bundle.min.js", content)

        if content != original_content:
            with open(filepath, "w", encoding="utf-8") as f:
                f.write(content)
            print(f"Updated: {filename}")
        else:
            print(f"No changes needed for: {filename}")

print("\nDone!")
