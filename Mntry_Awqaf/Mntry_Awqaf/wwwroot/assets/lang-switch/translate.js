// تعيين اللغة على كل العناصر النصية
function setLanguage(lang) {
    document.querySelectorAll("*").forEach(el => {
        if (el.childNodes.length === 1 && el.childNodes[0].nodeType === 3) {
            let text = el.innerText.trim();
            if (translations[lang][text]) {
                el.innerText = translations[lang][text];
            }
        }
    });
    localStorage.setItem("lang", lang); // حفظ اللغة المختارة
}

// عند تحميل الصفحة
document.addEventListener("DOMContentLoaded", () => {
    // استرجاع اللغة المختارة مسبقاً أو تعيين الإنجليزية كافتراضي
    let savedLang = localStorage.getItem("lang") || "en";
    setLanguage(savedLang);

    // ربط أزرار اللغة
    document.querySelectorAll("[data-lang]").forEach(btn => {
        btn.addEventListener("click", () => {
            setLanguage(btn.getAttribute("data-lang"));
        });
    });
});






//// قاموس الترجمة
//const translations = {
//    "en": {
//        "Customer": "Customer",
//        "Customer List": "Customer List",
//        "Order List": "Order List",
//        "Create Invoice": "Create Invoice",
//        "Order Details": "Order Details",
//        "Product": "Product",
//        "Product Review": "Product Review"
//    },
//    "ar": {
//        "Customer": "العملاء",
//        "Customer List": "قائمة العملاء",
//        "Order List": "قائمة الطلبات",
//        "Create Invoice": "إنشاء فاتورة",
//        "Order Details": "تفاصيل الطلب",
//        "Product": "المنتجات",
//        "Product Review": "مراجعة المنتجات"
//    }
//};

//// قراءة اللغة المخزنة أو الافتراضية
//let currentLang = localStorage.getItem("lang") || "en";

//// دالة الترجمة
//function translatePage(lang) {
//    const walker = document.createTreeWalker(document.body, NodeFilter.SHOW_TEXT, null, false);
//    while (walker.nextNode()) {
//        let node = walker.currentNode;
//        let text = node.nodeValue.trim();
//        if (translations[currentLang][text]) {
//            node.nodeValue = translations[lang][text];
//        }
//    }
//    currentLang = lang;
//    localStorage.setItem("lang", lang); // حفظ اللغة المختارة
//}

//// تطبيق الترجمة عند تحميل الصفحة
//window.addEventListener("DOMContentLoaded", () => {
//    if (currentLang !== "en") {
//        translatePage(currentLang);
//    }
//});

//// ربط أزرار اللغة
//document.querySelectorAll("[data-lang]").forEach(el => {
//    el.addEventListener("click", e => {
//        e.preventDefault();
//        const newLang = el.getAttribute("data-lang");
//        translatePage(newLang);
//    });
//});
