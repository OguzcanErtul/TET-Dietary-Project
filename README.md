# TET-Dietary Program

## Proje Tanımı
TET-Dietary, kullanıcıların günlük kalori alımını takip edebilmelerini sağlayan bir Windows Form uygulamasıdır. Bu uygulama, sağlıklı beslenme alışkanlıklarını desteklemek ve kullanıcının beslenme hedeflerine ulaşmasına yardımcı olmak için tasarlanmıştır.

## Ana Özellikler

- **Giriş ve Kayıt**: Kullanıcılar, uygulamaya giriş yapabilir veya yeni bir hesap oluşturabilir.
- **Profil Bilgileri**: Kullanıcılar kişisel sağlık bilgilerini (kilo, boy, cinsiyet, doğum tarihi, hedef) girebilir.
- **Yemek İşlemleri**: Kullanıcılar yedikleri yemekleri kaydedebilir ve bu yemeklerin kalori bilgilerini takip edebilir.
- **Raporlama**: Günlük, haftalık ve aylık kalori alım raporları görüntülenebilir.

## Kullanıcı Arayüzü ve Ekran Görüntüleri

![image](https://github.com/user-attachments/assets/a34587e6-ca3e-4255-8ceb-24b682794e28)
- Giriş ve kayıt ekranı, kullanıcıların uygulamaya erişimini sağlar.

![image](https://github.com/user-attachments/assets/53c2b515-bd4a-4e84-9000-4720723ea50d)
- Yeni kullanıcılar için kayıt formu.
  
![image](https://github.com/user-attachments/assets/1c25309a-4bba-4c9f-8739-43942ac203ab)
- İstenilen bilgileri doldurmazsanız bu uyarı ekranı ile karşılaşırsınız.
  
![image](https://github.com/user-attachments/assets/3052dc18-b91c-4585-9020-79f5d4e99523)
- Kullanıcı adınız mail adresiniz olmalıdır. Mail adresi olmayan bir kullanıcı adı belirlerseniz bu konuda bilgilendirilirsiniz.

![image](https://github.com/user-attachments/assets/b1c98517-2d7f-4f5e-b1a2-e66186544696)
- Eğer şifreniz istenilen özellikleri karşılamıyorsa bu uyarı mesajı ile karşılaşırsınız.

![image](https://github.com/user-attachments/assets/4b4304d5-a505-42f7-8068-3d501c2db5f5)

![image](https://github.com/user-attachments/assets/56aa4807-70fa-4500-a187-17275d48d5b9)
- Uygulamaya giriş ekranı.

![image](https://github.com/user-attachments/assets/019e93be-f203-47f2-a3f3-a708c37f0334)
- Şifre veya kullanıcı adı bilgileriniz yanlış veya eksik ise bu konuda bilgilendirilirsiniz.

![image](https://github.com/user-attachments/assets/8b6260fa-5423-4c8b-abd5-02e090ca99f8)
- Kullanıcıların kişisel bilgilerini girebildikleri form ekranıdır. Bu bilgilerin girilmesi zorunludur.

![image](https://github.com/user-attachments/assets/2ef347f3-943d-41ed-9177-71a90688b6f6)
- Ana ekran Paneli.
- **Meal Process:** Yemek kaydı ve kalori hesaplamalarının yapıldığı form ekranına yönlendirir.
- **Reports:** Kullanıcıların tüketim raporlarına ulaşabilecekleri form ekranına yönlendirir.
- **Profile Information:** Daha önce doldurulan Kullanıcıların kişisel bilgilerini girebildikleri form ekranına yönlendirir. Burada bilgilerinizi güncelleyebilirsiniz.
- **Exit:** Uygulamadan çıkar.

![image](https://github.com/user-attachments/assets/91b6db47-7cf2-49ef-b5d2-1f3819c5240a)
- Yemek kaydı yapılabilen ve kalori hesaplamalarının yapıldığı form ekranıdır.
- Meals bölümünde yediğiniz besinin öğününü seçiyorsunuz.
- Ardından hangi gün yediğinizi belirtiyorsunuz.

![image](https://github.com/user-attachments/assets/286d915c-faa9-4414-98a4-456b86865864)
- Food Category bölümünden yemek kategorisini belirliyorsunuz.

![image](https://github.com/user-attachments/assets/0c719e99-e862-487f-a814-3c745ec8db0a)
![image](https://github.com/user-attachments/assets/b77e74c3-1822-4a4d-a8a3-b684a4a2aeff)
- Seçilen yemek kategorisine göre yandaki yiyecek veya içecek görselleri güncellenmektedir.

![image](https://github.com/user-attachments/assets/3ebaf08d-7f4a-4ba9-a1ff-31af745e2e2f)
- Bir öğünde yediğimiz yiyecekleri seçebilir ve tabloya ekleyebiliriz. Hatta güncelleyebilir ve silebilirsiniz. O öğünde tütkettiğimiz yiyeceklerin hepsini tabloya aktardıktan sonra Done(Bitir)'e tıklayarak Dishes List'e kaydedilir.

![image](https://github.com/user-attachments/assets/5b5ca441-0c8d-4112-bc57-7189029ac977)
- En son örneğin üç öğün için oluşturulan günlük kaloriler Dishes List kısmında tutulur. Burada istenilen öğün silinebilir veya güncellenebilir.

![image](https://github.com/user-attachments/assets/a4c8a8ea-b978-482e-8afd-d917900f82d9)
- Kullanıcıların tüketim raporlarını görebileceği form ekranıdır.
- **The End Day Report:** Seçilen tarihte sadece günlük toplam kalori miktarı bildirilmektedir. Ayrıca tabloda öğün bazında tüketilen kalorilerde listelenir.
- **Most Consumed Foods:** günlük, haftalık, aylık, on beş günlük... en çok tüketilen yiyecek ve içecekler, tüketilen gramajlarıyla birlikte listelenir.
- **Most Consumed Foods By Meal:** İstenilen tarihler arasında öğün bazlı yiyecek ve içeceklerin toplam ne kadar (gram) tüketidiğini göstermektedir.
- **Summaries:** Kullanıcı hakkında özet bilgiler içerir. Vücut Kitle İndeksi bilgisini kullanıcıya verir. Ayruca Get Information butonuna tıkladığımızda VKİ'ye göre hangi kategoride olduğumuzu ve hedefimizin neler olması gerektiği ile ilgili uyarı mesajı verir.
![image](https://github.com/user-attachments/assets/71bd15cc-b34f-40c7-aebe-ed5488a33dd0)


## Teknik Detaylar

- **Programlama Dili**: C#
- **Framework**: .NET Framework
- **Veritabanı**: MS SQL Server, Entity Framework
- **Arayüz Teknolojileri**: Windows Forms
