// نظام ترجمة بسيط - يترجم النصوص فقط مع الحفاظ على المواقع
class SimpleTranslator {
    constructor() {
        this.currentLanguage = 'en';
        this.init();
    }

    init() {
        // تحميل اللغة المحفوظة
        this.loadLanguage();
        
        // إضافة مستمعي الأحداث لأزرار اللغة
        this.addLanguageButtons();
        
        // تطبيق الترجمة
        this.translatePage();
    }

    // تحميل اللغة المحفوظة
    loadLanguage() {
        const saved = localStorage.getItem('language');
        if (saved === 'ar' || saved === 'en') {
            this.currentLanguage = saved;
        }
    }

    // حفظ اللغة
    saveLanguage() {
        localStorage.setItem('language', this.currentLanguage);
    }

    // إضافة مستمعي الأحداث لأزرار اللغة
    addLanguageButtons() {
        const buttons = document.querySelectorAll('[data-lang]');
        buttons.forEach(button => {
            button.addEventListener('click', (e) => {
                e.preventDefault();
                const lang = button.getAttribute('data-lang');
                this.switchLanguage(lang);
            });
        });
    }

    // تبديل اللغة
    switchLanguage(lang) {
        if (lang === this.currentLanguage) return;
        
        this.currentLanguage = lang;
        this.saveLanguage();
        this.translatePage();
        
        // رسالة تأكيد بسيطة
        this.showMessage();
    }

    // تطبيق الترجمة على الصفحة
    translatePage() {
        // ترجمة العناصر التي تحتوي على data-translate
        const elements = document.querySelectorAll('[data-translate]');
        elements.forEach(element => {
            const key = element.getAttribute('data-translate');
            const translation = this.getTranslation(key);
            if (translation) {
                if (element.tagName === 'INPUT' && element.type === 'placeholder') {
                    element.placeholder = translation;
                } else {
                    element.textContent = translation;
                }
            }
        });

        // ترجمة النصوص المعروفة في القائمة الجانبية
        this.translateSidebar();
    }

    // ترجمة القائمة الجانبية
    translateSidebar() {
        // البحث عن النصوص المعروفة في القائمة الجانبية
        const sidebarTexts = document.querySelectorAll('.pc-sidebar, .sidebar, nav, .menu, .navigation');
        
        sidebarTexts.forEach(container => {
            const textNodes = this.getTextNodes(container);
            textNodes.forEach(node => {
                const text = node.textContent.trim();
                const translation = this.findAndTranslate(text);
                if (translation) {
                    node.textContent = translation;
                }
            });
        });
    }

    // الحصول على عقد النص
    getTextNodes(element) {
        const textNodes = [];
        const walker = document.createTreeWalker(
            element,
            NodeFilter.SHOW_TEXT,
            {
                acceptNode: function(node) {
                    // تجاهل النصوص في script و style
                    if (node.parentElement && 
                        (node.parentElement.tagName === 'SCRIPT' || 
                         node.parentElement.tagName === 'STYLE' ||
                         node.parentElement.tagName === 'INPUT')) {
                        return NodeFilter.FILTER_REJECT;
                    }
                    return node.textContent.trim() ? NodeFilter.FILTER_ACCEPT : NodeFilter.FILTER_REJECT;
                }
            }
        );

        let node;
        while (node = walker.nextNode()) {
            textNodes.push(node);
        }
        
        return textNodes;
    }

    // البحث عن الترجمة
    findAndTranslate(text) {
        if (!text || text.length < 2) return null;
        
        // البحث في اللغة الإنجليزية
        for (const [key, value] of Object.entries(window.translations.en)) {
            if (value === text) {
                return window.translations.ar[key] || text;
            }
        }
        
        // البحث في اللغة العربية
        for (const [key, value] of Object.entries(window.translations.ar)) {
            if (value === text) {
                return window.translations.en[key] || text;
            }
        }
        
        return null;
    }

    // الحصول على الترجمة
    getTranslation(key) {
        if (!window.translations || !window.translations[this.currentLanguage]) {
            return null;
        }
        return window.translations[this.currentLanguage][key];
    }

    // رسالة تأكيد بسيطة
    showMessage() {
        const message = this.currentLanguage === 'ar' ? 'تم تغيير اللغة إلى العربية' : 'Language changed to English';
        
        const notification = document.createElement('div');
        notification.textContent = message;
        notification.style.cssText = `
            position: fixed;
            top: 20px;
            right: 20px;
            background: #28a745;
            color: white;
            padding: 10px 20px;
            border-radius: 5px;
            z-index: 9999;
            font-size: 14px;
        `;
        
        document.body.appendChild(notification);
        
        setTimeout(() => {
            if (notification.parentNode) {
                notification.parentNode.removeChild(notification);
            }
        }, 2000);
    }
}

// بدء النظام عند تحميل الصفحة
document.addEventListener('DOMContentLoaded', () => {
    window.translator = new SimpleTranslator();
});

}
