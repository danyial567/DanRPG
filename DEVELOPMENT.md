# 🎮 راهنمای توسعه DanRPG

## 📋 ساختار پروژه

```
Assets/
├── Scripts/
│   ├── Player/
│   │   ├── PlayerController.cs       # کنترل حرکت
│   │   └── PlayerStats.cs            # آمار کاراکتر
│   ├── Combat/
│   │   └── PlayerCombat.cs           # سیستم مبارزه
│   ├── Enemies/
│   │   └── Enemy.cs                  # هوش مصنوعی دشمن
│   ├── UI/
│   │   └── UIManager.cs              # رابط کاربری
│   └── World/
│       └── WorldManager.cs           # مدیریت دنیا
├── Scenes/
│   └── MainScene.unity               # صحنه اصلی
├── Prefabs/
│   ├── Player.prefab
│   └── Enemy.prefab
├── Models/
├── Audio/
└── Input/
    └── PlayerInput.inputactions
```

## 🚀 شروع کار

### 1. نصب Unity
- دانلود **Unity 2022 LTS** یا بالاتر
- ایجاد پروژه جدید

### 2. کلون کردن Repository
```bash
git clone https://github.com/danyial567/DanRPG.git
cd DanRPG
```

### 3. باز کردن در Unity
- فایل > Open Project
- انتخاب پوشه DanRPG
- صبر برای import کردن Assets

### 4. ایجاد Scene
- Scenes > New Scene
- نام: MainScene
- ذخیره در `Assets/Scenes/`

## 🎯 مرحله‌های بعدی

### مرحله 1: Setup صحنه (هفته ۱)
- [ ] ایجاد terrain/ground
- [ ] اضافه کردن Lighting
- [ ] تنظیم Camera
- [ ] اضافه کردن Skybox

### مرحله 2: شخصیت اصلی (هفته ۲)
- [ ] ایمپورت مدل 3D دان
- [ ] Setup Animator
- [ ] تست حرکت
- [ ] تست کنترل‌ها

### مرحله 3: دشمنان (هفته ۳)
- [ ] ایمپورت مدل شیاطین
- [ ] Setup AI
- [ ] تست تعقیب
- [ ] تست مبارزه

### مرحله 4: UI & منو (هفته ۴)
- [ ] UI منو اصلی
- [ ] UI داخل بازی
- [ ] پنل آمار
- [ ] منوی توقف

### مرحله 5: صوت و موسیقی (هفته ۵)
- [ ] موسیقی پس‌زمینه
- [ ] افکت صوتی حمله
- [ ] افکت صوتی مرگ
- [ ] صدای قدم‌ها

## 💡 نکات مهم

### برای کنترل موبایلی
- استفاده از **Input System** جدید
- طراحی UI برای **صفحات کوچک**
- تست بر روی **دستگاه واقعی**

### بهینه‌سازی
- استفاده از **Object Pooling** برای دشمنان
- **LOD** برای مدل‌های 3D
- **Culling** برای کاهش رندر

### کنترل نسخه
```bash
# ایجاد branch جدید
git checkout -b feature/enemy-ai

# commit کردن تغییرات
git add .
git commit -m "Add enemy AI system"

# push کردن
git push origin feature/enemy-ai
```

## 🔗 منابع مفید

- [Unity Documentation](https://docs.unity.com)
- [Unity Learn](https://learn.unity.com)
- [Input System Guide](https://docs.unity3d.com/Packages/com.unity.inputsystem)
- [Networking (اگر multiplayer مد نظر باشد)](https://docs.unity.com/netcode/)

## 📞 تماس و کمک

برای سوالات و مشکلات:
- ایجاد Issue در GitHub
- Discussion در Repository

---

**خوش بگذرید! 🎮✨**
