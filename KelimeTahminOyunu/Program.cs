using System;

class Program
{
    static void Main()
    {
        bool oyunDevamEtsin = true;

        while (oyunDevamEtsin)
        {
            Console.WriteLine("Kelime Tahmin Oyununa Hoş Geldiniz!");
            string secilenKelime = "";
            do
            {
                Console.Write("Birinci oyuncu, tahmin edilecek kelimeyi girsin: \n");
                secilenKelime = gizliKelimeAl();
            } while (string.IsNullOrEmpty(secilenKelime)); 

            
            char[] tahminler = tahminleriBaslat(secilenKelime.Length);
            int hak = 6;
            bool kazandi = false;

            
            while (hak > 0 && !kazandi)
            {
                Console.Clear();
                ekranaDurumYazdir(tahminler, hak);

                
                Console.WriteLine("1: Harf tahmin et");
                Console.WriteLine("2: Kelime tahmin et");
                Console.Write("Seçiminiz: ");
                string secim = Console.ReadLine();

                if (secim == "1")
                {
                    char tahmin = kullaniciHarfTahminiAl();

                    if (kelimeIceriyorMu(secilenKelime, tahmin))
                    {
                        harfiAc(secilenKelime, tahminler, tahmin);
                    }
                    else
                    {
                        hak--;
                    }
                }
                else if (secim == "2")
                {
                    try
                    {
                        string kelimeTahmin = KullaniciKelimeTahminiAl(secilenKelime.Length);

                        if (kelimeTahmin == secilenKelime)
                        {
                            kazandi = true;
                        }
                        else
                        {
                            hak--;
                            Console.WriteLine("Yanlış tahmin! Kalan hakkınız: " + hak);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("yanlış seçim",ex); 
                    }
                }
                else
                {
                    Console.WriteLine("Geçersiz seçim, lütfen 1 veya 2 seçin.");
                }

                kazandi = kazandi || kazandimi(tahminler);
            }

            oyunSonucuYazdir(kazandi, secilenKelime);

            
            Console.WriteLine("Oyunu yeniden başlatmak için 'r' tuşuna basın, çıkmak için herhangi bir tuşa basın.");

           
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);

            if (keyInfo.KeyChar == 'r' || keyInfo.KeyChar == 'R')
            {
                Console.Clear();
                continue; 
            }
            else
            {
                oyunDevamEtsin = false; 
            }
        }

        Console.WriteLine("Oyundan çıkılıyor. Teşekkürler!");
    }

    static string gizliKelimeAl()
    {
        string kelime = "";
        ConsoleKeyInfo keyInfo;

        do
        {
            keyInfo = Console.ReadKey(true); 
            if (keyInfo.Key != ConsoleKey.Enter)
            {
                kelime += keyInfo.KeyChar;
                Console.Write("*"); 
            }
        } while (keyInfo.Key != ConsoleKey.Enter);

        //Console.Clear();
        return kelime.ToLower();  
    }

    static char[] tahminleriBaslat(int kelimeUzunlugu)
    {
        char[] tahminler = new char[kelimeUzunlugu];
        for (int i = 0; i < tahminler.Length; i++)
        {
            tahminler[i] = '_';
        }
        return tahminler;
    }

    static void ekranaDurumYazdir(char[] tahminler, int hak)
    {
        Console.WriteLine("Kelimeyi tahmin et: " + new string(tahminler));
        Console.WriteLine("Kalan hakkınız: " + hak);
    }

    static char kullaniciHarfTahminiAl()
    {
        Console.Write("Bir harf tahmin edin: ");
        return Console.ReadLine().ToLower()[0];
    }

    static string KullaniciKelimeTahminiAl(int kelimeUzunlugu)
    {
        Console.Write("Kelime tahmin edin: ");
        string tahmin = Console.ReadLine().ToLower();

        
        if (tahmin.Length != kelimeUzunlugu)
        {
            throw new Exception($"Tahmininizin uzunluğu {kelimeUzunlugu} olmalı. Lütfen doğru uzunlukta bir kelime tahmin edin.");
        }

        return tahmin;
    }

    static bool kelimeIceriyorMu(string kelime, char harf)
    {
        return kelime.Contains(harf);
    }

    static void harfiAc(string kelime, char[] tahminler, char harf)
    {
        for (int i = 0; i < kelime.Length; i++)
        {
            if (kelime[i] == harf)
            {
                tahminler[i] = harf;
            }
        }
    }

    static bool kazandimi(char[] tahminler)
    {
        return !new string(tahminler).Contains('_');
    }

    static void oyunSonucuYazdir(bool kazandi, string secilenKelime)
    {
        if (kazandi)
        {
            Console.WriteLine("Tebrikler! Kelimeyi doğru tahmin ettiniz: " + secilenKelime);
        }
        else
        {
            Console.WriteLine("Üzgünüm, kaybettiniz. Kelime: " + secilenKelime);
        }
    }
}
