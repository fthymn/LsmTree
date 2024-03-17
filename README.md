# .Net 7 Web Api ile LsmTree - SSTable Çalışması

- Proje ayağa kalktığında swagger üzerinden istek yapabilirsiniz.
- LsmEntity adında bir entity mevcut, id ve value'dan oluşuyor.
- DataStoreManager, commitlog - lsmTree (MemTable) - ssTable'ları yönetiyor.
- Yapılan her istek commitloga muhakkak yazılıyor,
  proje kırıldıysa proje ayağa kalktığında orada data varsa geri yükleniyor.
- İstekler max size'a göre kontrol edilip, beklenilen sayıya ulaşıldığında ssTable'lara bölünüyor.
- Arama işlemlerinde, önce memtable'ın son kaydından itibaren aranıyor, yoksa son oluşturulan ssTable'dan itibaren aramaya başlanıyor.
- Compaction işleminde de, öncelikle her dosyanın kendi içinde en son değerler kalacak şekilde birleştirme yapılıyor,
  sonrasında tüm dosyalarda kalan datalar birleştirilerek, son girilen kaydın güncelliği baz alınarak, diğer kayıtlar siliniyor.
  Eski dosyalar silinip bunlar tek dosyaya yazılıyor.
