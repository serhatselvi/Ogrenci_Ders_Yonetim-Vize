using System;
using System.Collections.Generic;

namespace OgrenciDersYonetimSistemi
{
    // Temel sınıf (Base Class)
    public abstract class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public abstract void BilgiGoster(); // Polymorphism için
    }

    // Interface tanımı
    public interface ILogin
    {
        void Login(string username, string password);
    }

    // Öğrenci sınıfı (Student)
    public class Ogrenci : Person, ILogin
    {
        public string OgrenciNo { get; set; }

        public override void BilgiGoster()
        {
            Console.WriteLine($"Öğrenci: {Name}, No: {OgrenciNo}, Email: {Email}");
        }

        public void Login(string username, string password)
        {
            Console.WriteLine($"{Name} kullanıcısı giriş yaptı.");
        }
    }

    // Öğretmen sınıfı (Teacher)
    public class Ogretmen : Person, ILogin
    {
        public override void BilgiGoster()
        {
            Console.WriteLine($"Öğretmen: {Name}, Email: {Email}");
        }

        public void Login(string username, string password)
        {
            Console.WriteLine($"{Name} öğretmen giriş yaptı.");
        }
    }

    // Ders sınıfı (Course)
    public class Ders
    {
        public string DersAdi { get; set; }
        public int Kredi { get; set; }
        public Ogretmen Ogretmen { get; set; }
        public List<Ogrenci> KayitliOgrenciler { get; set; }

        public Ders()
        {
            KayitliOgrenciler = new List<Ogrenci>();
        }

        public void DersBilgileriGoster()
        {
            Console.WriteLine($"Ders: {DersAdi}, Kredi: {Kredi}, Öğretmen: {Ogretmen.Name}");
            Console.WriteLine("Kayıtlı Öğrenciler:");
            foreach (var ogrenci in KayitliOgrenciler)
            {
                Console.WriteLine($"- {ogrenci.Name} ({ogrenci.OgrenciNo})");
            }
        }

        public void OgrenciKaydet(Ogrenci ogrenci)
        {
            KayitliOgrenciler.Add(ogrenci);
            Console.WriteLine($"{ogrenci.Name} derse kaydedildi.");
        }
    }

    class Program
    {
        static List<Ders> dersler = new List<Ders>();

        static void Main(string[] args)
        {
            Console.WriteLine("--- Öğrenci ve Ders Yönetim Sistemi ---");

            while (true)
            {
                Console.WriteLine("\nMenü:");
                Console.WriteLine("1. Yeni Ders Ekle");
                Console.WriteLine("2. Yeni Öğrenci Ekle");
                Console.WriteLine("3. Tüm Dersleri ve Öğrencileri Listele");
                Console.WriteLine("4. Çıkış");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                switch (secim)
                {
                    case "1":
                        YeniDersEkle();
                        break;
                    case "2":
                        YeniOgrenciEkle();
                        break;
                    case "3":
                        TumDersleriListele();
                        break;
                    case "4":
                        Console.WriteLine("Çıkış yapılıyor...");
                        return;
                    default:
                        Console.WriteLine("Geçersiz seçim! Lütfen tekrar deneyin.");
                        break;
                }
            }
        }

        static void YeniDersEkle()
        {
            Console.Write("Ders Adı: ");
            string dersAdi = Console.ReadLine();

            Console.Write("Ders Kredisi: ");
            int kredi = int.Parse(Console.ReadLine());

            Console.Write("Öğretmenin Adı Soyadı: ");
            string ogretmenAdi = Console.ReadLine();

            Console.Write("Öğretmenin Email: ");
            string email = Console.ReadLine();

            var ogretmen = new Ogretmen
            {
                Id = dersler.Count + 1,
                Name = ogretmenAdi,
                Email = email
            };

            var ders = new Ders
            {
                DersAdi = dersAdi,
                Kredi = kredi,
                Ogretmen = ogretmen
            };

            dersler.Add(ders);
            Console.WriteLine($"Ders {dersAdi} başarıyla eklendi!");
        }

        static void YeniOgrenciEkle()
        {
            if (dersler.Count == 0)
            {
                Console.WriteLine("Henüz ders eklenmemiş. Lütfen önce ders ekleyin.");
                return;
            }

            Console.WriteLine("Hangi derse öğrenci eklemek istiyorsunuz?");
            for (int i = 0; i < dersler.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {dersler[i].DersAdi}");
            }

            Console.Write("Ders seçiniz (numara): ");
            int dersIndex = int.Parse(Console.ReadLine()) - 1;

            if (dersIndex < 0 || dersIndex >= dersler.Count)
            {
                Console.WriteLine("Geçersiz seçim!");
                return;
            }

            Console.Write("Öğrenci Adı Soyadı: ");
            string adSoyad = Console.ReadLine();

            Console.Write("Öğrenci Email: ");
            string email = Console.ReadLine();

            Console.Write("Öğrenci Numarası: ");
            string ogrenciNo = Console.ReadLine();

            var yeniOgrenci = new Ogrenci
            {
                Id = dersler[dersIndex].KayitliOgrenciler.Count + 1,
                Name = adSoyad,
                Email = email,
                OgrenciNo = ogrenciNo
            };

            dersler[dersIndex].OgrenciKaydet(yeniOgrenci);
        }

        static void TumDersleriListele()
        {
            if (dersler.Count == 0)
            {
                Console.WriteLine("Henüz eklenmiş bir ders bulunmuyor.");
                return;
            }

            foreach (var ders in dersler)
            {
                Console.WriteLine($"\n--- {ders.DersAdi} ---");
                ders.DersBilgileriGoster();
            }
        }
    }
}
