from fastapi import FastAPI, UploadFile, File
from fastapi.responses import StreamingResponse, HTMLResponse
import uvicorn
import numpy as np
import cv2
import io
from land_classifier_online import LandSegmenterSAM

app = FastAPI(title="Land Segmentation API - Red Outlines Only")

segmenter = LandSegmenterSAM()

@app.get("/", response_class=HTMLResponse)
def home():
    html = """
    <html>
        <body>
            <h2>رفع صورة للحصول على حدود الأراضي (باللون الأحمر)</h2>
            <form action="/segment" enctype="multipart/form-data" method="post">
                <input name="file" type="file" accept="image/*">
                <input type="submit" value="رفع وتحليل">
            </form>
        </body>
    </html>
    """
    return HTMLResponse(html)

@app.post("/segment")
async def segment_image(file: UploadFile = File(...)):
    # قراءة الصورة
    image_bytes = await file.read()
    arr = np.frombuffer(image_bytes, np.uint8)
    img = cv2.imdecode(arr, cv2.IMREAD_COLOR)

    # احصل على القطع
    results = segmenter.segment_and_classify(img)

    # رسم حدود القطع باللون الأحمر
    img_draw = img.copy()
    red_color = (0, 0, 255)  # BGR
    for region in results:
        pts = region["polygon"].astype(np.int32)
        cv2.polylines(img_draw, [pts], True, red_color, 2)

    # إرجاع الصورة
    _, buffer = cv2.imencode(".png", img_draw)
    return StreamingResponse(io.BytesIO(buffer.tobytes()), media_type="image/png")

if __name__ == "__main__":
    uvicorn.run("api:app", host="127.0.0.1", port=8080, reload=True)
