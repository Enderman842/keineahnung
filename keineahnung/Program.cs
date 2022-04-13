using System;
using System.IO;
using System.Text;

namespace keineahnung
{
    class Program
    {
        static void Main(string[] args)
        {
            string dir = @"C:/Users/" + Environment.UserName + "/AppData/Local/keineahnung_";
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            int m = 0;

            while (true)
            {
                Console.Clear();
                if (m == 1) Console.WriteLine("\"To-Do\"-Eintrag erfolgreich erstellt!");
                if (m == 2) Console.WriteLine("Es gibt keine gespeicherten \"To-Do\"-Einträge!");
                if (m == 3) Console.WriteLine("Es konnte kein weiterer \"To-Do\"-Eintrag erstellt werden, da die maximale Anzahl von 9998 Einträgen erreicht wurde.");
                if (m == 4) Console.WriteLine("\"To-Do\"-Eintrag wurde erfolgreich gelöscht!");
                if (m == 5) Console.WriteLine("Es gibt keine gespeicherten \"To-Do\"-Einträge, die gelöscht werden können!");
                m = 0;
                Console.WriteLine("Gib einen Befehl ein:\r\n");
                string input = Console.ReadLine();

                if (input == "create")
                {
                    Console.Clear();
                    Console.WriteLine("Wie soll es heißen?\r\n(Drücke einfach nur [ENTER] um abzubrechen)");
                    string inputB = Console.ReadLine();
                    string[] dirList = Directory.GetFiles(dir);
                    int newestId;
                    int newId;
                    if (dirList.Length != 0)
                    {
                        newestId = Int32.Parse(dirList[0].Substring(dir.Length + 1, 4));
                        newId = newestId - 1;
                    }
                    else
                    {
                        newId = 9999;
                    }

                    if(newId != 0)
                    {
                        string theFileId;
                        if (newId <= 999 && newId > 99) theFileId = "0" + newId;
                        else if (newId <= 99 && newId > 9) theFileId = "00" + newId;
                        else if (newId <= 9) theFileId = "000" + newId;
                        else theFileId = newId.ToString();

                        if(inputB != "")
                        {
                            CreateFileWithString(dir + "/" + theFileId + ".txt", inputB);
                            m = 1;
                        }
                    }
                    else
                    {
                        m = 3;
                    }
                }

                if(input == "list")
                {
                    Console.Clear();
                    string[] dirList = Directory.GetFiles(dir);
                    if(dirList.Length != 0)
                    {
                        int i = 0;
                        int a = dirList.Length;
                        Console.WriteLine("To-Do-Liste:\r\n");
                        while(i < a)
                        {
                            Console.Write("[" + (i + 1) + "] ");
                            ReadFromFile(dirList[i]);
                            Console.Write("\r\n");
                            i++;
                        }
                        Console.WriteLine("\r\nDrücke [ENTER]...");
                        Console.ReadLine();
                    }
                    else
                    {
                        m = 2;
                    }
                }

                if(input == "help")
                {
                    Console.Clear();
                    Console.WriteLine("Die Befehle:\r\nlist - Alle Einträge mit ID auflisten\r\ncreate - Einen Eintrag erstellen\r\ndelete - Einen Eintrag löschen\r\nexit - Programm verlassen\r\ninfo - Infos über das Program");
                    Console.WriteLine("\r\nDrücke [ENTER]...");
                    Console.ReadLine();
                }

                if(input == "delete")
                {
                    Console.Clear();
                    string[] dirList = Directory.GetFiles(dir);
                    if(dirList.Length != 0)
                    {
                        int i = 0;
                        while (i < dirList.Length)
                        {
                            Console.Write("[" + (i + 1) + "] ");
                            ReadFromFile(dirList[i]);
                            Console.Write("\r\n");
                            i++;
                        }
                        Console.WriteLine("\r\nGib die ID von dem Eintrag ein, den du löschen möchtest:\r\n(Drücke einfach nur [ENTER] um abzubrechen)");
                        string a = Console.ReadLine();
                        if(a != "")
                        {
                            int b = Int32.Parse(a);
                            File.Delete(dirList[b - 1]);
                            m = 4;
                        }
                    }
                    else
                    {
                        m = 5;
                    }
                }
                if(input == "info")
                {
                    Console.Clear();
                    Console.WriteLine("KeineAhnung (Ein To-Do-Listen-Programm von puxped) v1.0\r\nGemacht aus langer Weile am 12. und 13.04.2022\r\n\r\nDrücke [ENTER]...");
                    Console.ReadLine();
                }

                if(input == "exit")
                {
                    return;
                }
            }
        }



        // -------------------------------------- NUR SO ZEUGS -------------------------------------- //
        
        static void CreateFileWithString(string path, string text)
        {
            byte[] crlf = { 13, 10 };

            string username = Environment.UserName;

            if (!File.Exists(path))
            {
                File.Delete(path);
            }

            using (FileStream fs = File.Create(path))
            {
                AddText(fs, text);
                fs.Write(crlf);
                fs.Close();
            }

        }

        static void ReadFromFile(string path)
        {
            using (StreamReader sr = File.OpenText(path))
            {
                string s;
                while ((s = sr.ReadLine()) != null)
                {
                    Console.Write(s);
                }
            }
        }

        private static void AddText(FileStream fs, string value)
        {
            byte[] info = new UTF8Encoding(true).GetBytes(value);
            fs.Write(info, 0, info.Length);
        }
    }
}
