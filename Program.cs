using System;

public class Program
{
    public static void Main()
    {
        Robot robotS = new RobotNormal("Soyou", 200, 50, 15);
        Robot bosRobot = new BosRobot("LordInoen", 500, 100, 30, 25);

        IKemampuan perbaikan = new Perbaikan();
        IKemampuan seranganListrik = new SeranganListrik();
        IKemampuan seranganPlasma = new SeranganPlasma();
        IKemampuan pertahananSuper = new PertahananSuper();

        robotS.GunakanKemampuan(perbaikan);
        bosRobot.GunakanKemampuan(seranganListrik);

        robotS.Serang(bosRobot);
        bosRobot.Serang(robotS);

        robotS.CetakInformasi();
        bosRobot.CetakInformasi();
    }
}
public abstract class Robot
{
    public string Nama { get; set; }
    public int Energi { get; set; }
    public int Armor { get; set; }
    public int Serangan { get; set; }

    public Robot(string nama, int energi, int armor, int serangan)
    {
        Nama = nama;
        Energi = energi;
        Armor = armor;
        Serangan = serangan;
    }

    public void Serang(Robot target)
    {
        int damage = Serangan - target.Armor;
        if (damage < 0) damage = 0;

        Console.WriteLine($"{Nama} menyerang {target.Nama} dengan damage {damage}.");
        target.Energi -= damage;
        if (target.Energi <= 0)
        {
            target.Mati();
        }

        PulihkanEnergi();
    }

    public abstract void GunakanKemampuan(IKemampuan kemampuan);

    public void CetakInformasi()
    {
        Console.WriteLine($"Nama : {Nama}, Energi : {Energi}, Armor : {Armor}, Serangan : {Serangan}");
    }

    public virtual void Mati()
    {
        Console.WriteLine($"{Nama} telah mati.");
    }

    public void PulihkanEnergi()
    {
        int energiPulih = 25;
        Energi += energiPulih;
        Console.WriteLine($"{Nama} memulihkan sebanyak {energiPulih} energi. Energi sekarang : {Energi}");
    }
}


public interface IKemampuan
{
    void Gunakan(Robot target);
    bool DalamCooldown { get; }
}

public class Perbaikan : IKemampuan
{
    public bool DalamCooldown { get; private set; }

    public void Gunakan(Robot target)
    {
        if (!DalamCooldown)
        {
            target.Energi += 20;
            DalamCooldown = true;
            Console.WriteLine($"{target.Nama} Menggunakan perbaikan, energi bertambah 20.");
        }
        else
        {
            Console.WriteLine("Perbaikan masih cooldown.");
        }
    }
}
public class SeranganListrik : IKemampuan
{
    public bool DalamCooldown { get; private set; }

    public void Gunakan(Robot target)
    {
        if (!DalamCooldown)
        {
            target.Energi -= 30;
            target.Armor = 0;
            DalamCooldown = true;
            Console.WriteLine($"{target.Nama} terkena Serangan Listrik, energi berkurang 30.");
        }
        else
        {
            Console.WriteLine("Serangan Listrik masih cooldown.");
        }
    }
}
public class SeranganPlasma : IKemampuan
{
    public bool DalamCooldown { get; private set; }

    public void Gunakan(Robot target)
    {
        if (!DalamCooldown)
        {
            target.Energi -= 25;
            DalamCooldown = true;
            Console.WriteLine($"{target.Nama} terkena Serangan Plasma, energi berkurang 25.");
        }
        else
        {
            Console.WriteLine("Serangan Plasma masih cooldown.");
        }
    }
}
public class PertahananSuper : IKemampuan
{
    public bool DalamCooldown { get; private set; }

    public void Gunakan(Robot target)
    {
        if (!DalamCooldown)
        {
            target.Armor += 20;
            DalamCooldown = true;
            Console.WriteLine($"{target.Nama} menggunakan Pertahanan Super, armor meningkat 20.");
        }
        else
        {
            Console.WriteLine("Pertahanan Super masih cooldown.");
        }
    }
}
public class RobotNormal : Robot
{
    public RobotNormal(string nama, int energi, int armor, int serangan)
        : base(nama, energi, armor, serangan) { }

    public override void GunakanKemampuan(IKemampuan kemampuan)
    {
        kemampuan.Gunakan(this);
    }
}

public class BosRobot : Robot
{
    public int PertahananTambahan { get; set; }

    public BosRobot(string nama, int energi, int armor, int serangan, int pertahananTambahan)
        : base(nama, energi, armor, serangan)
    {
        PertahananTambahan = pertahananTambahan;
        Armor += PertahananTambahan;
    }

    public override void GunakanKemampuan(IKemampuan kemampuan)
    {
        kemampuan.Gunakan(this);
    }

    public override void Mati()
    {
        Console.WriteLine($"{Nama}, sang Bos Robot, telah dikalahkan.");
    }
}