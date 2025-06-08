# CODE_SMELLS Raporu – KelimeEzberApp

Bu rapor, GitHub projesi KelimeEzberApp için yapılan code smells (kod kokuları) analizini içermektedir. 

---

 1. Uzun Metotlar:

Örnek Dosya: 'FormTest.cs', 'FormKelimeEkle.cs'  
'LoadNewQuestion()', 'buttonKontrolEt_Click()' gibi metotlar çok fazla işlev içermiş.  
Tek bir metotta hem veritabanı bağlantısı kurulmuş hem UI güncellenmiş hem de mantıksal kararlar verilmiş.  

Öneri: 
“Single Responsibility” ilkesine uygun olarak metotları küçük parçalara bölünmeli ve işlevleri azaltılmalıdır.

 2. Sihirli Sayılar ve Sabitler:
Örnek:
int dailyLimit = 10;

Sorun:Sabitler doğrudan sayısal olarak kullanılmış ama bu kullanım da okunabilirliği düşürür.  
Öneri:  
const int DefaultDailyLimit = 10;
 
şeklinde kullanılması okunabilirlik açısından daha iyi olur.

 3. Yinelenen Kod:
 Dosyalar: 'FormKelimeEkle.cs', 'FormTest.cs'  
Sorun: Bu dosyalarda SQLite bağlantı kodları tekrar tekrar aynı şekilde yazılmış.  
Öneri:  
Ortak bir 'DatabaseHelper.cs' sınıfı oluşturulup bağlantılar merkezi hale getirilirse kod tekrarlarından kaçınılmış olunur.

4. Hard-Coded Dosya Yolları:
Örnek:
string imagePath = Application.StartupPath + "\\Resimler\\" + imageName;
Sorun: Dosya yolları sabit yazılmış ve bu durum taşınabilirliği azaltır.  
Öneri:
'Path.Combine()' kullanılmalıdır:  
Path.Combine(Application.StartupPath, "Resimler", imageName);

5. Global Değişken Kullanımı:

Dosya: 'FormTest.cs'
Sorun: 'correctAnswer', 'currentWordId', 'userId' gibi değişkenler sınıf seviyesinde ama sadece belirli metotlarda kullanılmış.
Öneri:  
Gereksiz global değişkenler lokal seviyeye indirilmelidir.

 6. Yetersiz Hata Yönetimi:
Sorun:
'try-catch' blokları az kullanılmış veya hiç yok. Kullanıcıya hata mesajı dönülmüyor.
Öneri:  
Hatalar loglanmalı ve kullanıcı bilgilendirilmelidir.

 7. Yorum Eksikliği ve Anlamı Belirsiz Değişken Adları:
Sorun:
int id = reader.GetInt32(0);
Öneri:
int wordId = reader.GetInt32(0);
şeklinde daha anlamlı isimler kullanılmalıdır. Bu şekilde kod daha anlamlı olur ve ne istediği daha belirli olur.

 8. UI Logic ve Business Logic Karışıklığı:

Sorun:
Tüm iş mantığı doğrudan 'Form' sınıflarında yazılmış.
Öneri: 
Kodlar MVC veya MVVM mimarilerine göre katmanlara ayrılabilir. Bu şekilde karışıklık daha aza indirgenmiş olur.
