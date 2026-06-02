List<Stand> daftarStand = new List<Stand>()
{
    new StandOutdoor("Outdoor-1", 400000),
    new StandOutdoor("Outdoor-2", 350000),

    new StandIndoor("Indoor-1", 600000),
    new StandIndoor("Indoor-2", 550000),

    new StandPremium("Premium-1", 1000000),
    new StandPremium("Premium-2", 900000),
};

while (true)
{
    Console.Clear();

    Console.WriteLine("=== Moklet Expo Management Center ===");
    Console.WriteLine("\nDaftar Stand Tersedia:");
    foreach (var stand in daftarStand)
    {
        if (stand.IsAvailable)
        {
            Console.WriteLine($"{stand.NamaStand} \t| Rp {stand.HargaSewaPerHari}/hari \t| Tersedia");
        }
    }

    Console.WriteLine("\n1. Sewa Stand");
    Console.WriteLine("2. Akhiri Sewa Stand");
    Console.WriteLine("3. Keluar");
    Console.Write("Pilihan: ");
    string pilihan = Console.ReadLine();

    if (pilihan == "1")
    {
        Console.Write("\nMasukkan nama stand yang ingin disewa: ");
        string nama_stand = Console.ReadLine();

        var cari_stand = daftarStand.FirstOrDefault(cs => string.Equals(nama_stand, cs.NamaStand, StringComparison.OrdinalIgnoreCase));

        if (cari_stand == null)
        {
            Console.WriteLine("\nStand tidak ditemukan.");
        }
        else if (!cari_stand.IsAvailable)
        {
            Console.WriteLine("\nStand sedang tidak tersedia.");
        }
        else
        {
            Console.WriteLine($"Stand ditemukan: {cari_stand.NamaStand} | Rp {cari_stand.HargaSewaPerHari} / hari");

            Console.Write("\nMasukkan jumlah hari: ");
            int hari = int.Parse(Console.ReadLine());

            double total_biaya = cari_stand.HitungTotal(hari);

            Console.WriteLine($"\nTotal Biaya: Rp {total_biaya}");
            Console.WriteLine($"\nStand {cari_stand.NamaStand} berhasil disewakan selama {hari} hari");

            cari_stand.UbahStatus();
        }
    }
    else if (pilihan == "2")
    {
        Console.WriteLine("\nDaftar Stand yang Sedang Disewakan");
        foreach (var stand in daftarStand)
        {
            if (!stand.IsAvailable)
            {
                Console.WriteLine($"{stand.NamaStand,-12} | Rp {stand.HargaSewaPerHari} / hari \t| Tidak tersedia");
            }
        }

        Console.Write("\nMasukkan nama stand: ");
        string nama_stand = Console.ReadLine();

        var cari_stand = daftarStand.FirstOrDefault(cs => string.Equals(nama_stand, cs.NamaStand, StringComparison.OrdinalIgnoreCase));

        if (cari_stand == null)
        {
            Console.WriteLine("\nStand tidak ditemukan.");
        }
        else if (cari_stand.IsAvailable)
        {
            Console.WriteLine("\nStand belum disewa.");
        }
        else
        {
            Console.WriteLine($"Stand ditemukan: {cari_stand.NamaStand} | Rp {cari_stand.HargaSewaPerHari} / hari");
            cari_stand.UbahStatus();
            Console.WriteLine($"\nSewa stand {cari_stand.NamaStand} berhasil diakhiri");
        }
    }
    else if (pilihan == "3")
    {
        Console.WriteLine("\nTekan ENTER untuk menutup aplikasi...");
        Console.ReadLine();
        break;
    }
    else
    {
        Console.WriteLine("\nPilihan tidak valid.");
    }

    Console.WriteLine("\nTekan ENTER untuk mengulang...");
    Console.ReadLine();
}


class Stand
{
    protected string _namaStand;
    protected double _hargaSewaPerHari;
    protected bool _isAvailable;

    public Stand(string namaStand, double hargaSewaPerHari)
    {
        NamaStand = namaStand;
        HargaSewaPerHari = hargaSewaPerHari;
        _isAvailable = true;
    }

    public string NamaStand
    {
        get { return _namaStand; }
        set
        {
            if (!string.IsNullOrWhiteSpace(value))
            {
                _namaStand = value;
            }
        }
    }

    public double HargaSewaPerHari
    {
        get { return _hargaSewaPerHari; }
        set
        {
            if (value > 0)
            {
                _hargaSewaPerHari = value;
            }
        }
    }

    public bool IsAvailable
    {
        get { return _isAvailable; }
    }

    public void DisplayInfo()
    {
        Console.WriteLine("Nama Stand : " + NamaStand);
        Console.WriteLine("Harga/Hari : Rp" + HargaSewaPerHari);
        Console.WriteLine("Status : " + (IsAvailable ? "Tersedia" : "Disewa"));
    }

    public void UbahStatus()
    {
        _isAvailable = !_isAvailable;
    }

    public virtual double HitungTotal(int jumlahHari)
    {
        return HargaSewaPerHari * jumlahHari;
    }
}

class StandOutdoor : Stand
{
    protected double _biayaTenda = 75000;

    public StandOutdoor(string namaStand, double hargaSewaPerHari)
        : base(namaStand, hargaSewaPerHari)
    {
    }

    public double BiayaTenda
    {
        get { return _biayaTenda; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        return (HargaSewaPerHari * jumlahHari)
             + (BiayaTenda * jumlahHari);
    }
}

class StandIndoor : Stand
{
    protected double _biayaListrik = 100000;

    public StandIndoor(string namaStand, double hargaSewaPerHari)
        : base(namaStand, hargaSewaPerHari)
    {
    }

    public double BiayaListrik
    {
        get { return _biayaListrik; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        return (HargaSewaPerHari * jumlahHari)
             + (BiayaListrik * jumlahHari);
    }
}

class StandPremium : Stand
{
    protected double _biayaKeamanan = 300000;

    public StandPremium(string namaStand, double hargaSewaPerHari) : base(namaStand, hargaSewaPerHari) { }

    public double BiayaKeamanan
    {
        get { return _biayaKeamanan; }
    }

    public override double HitungTotal(int jumlahHari)
    {
        return (HargaSewaPerHari * jumlahHari) + BiayaKeamanan;
    }
}