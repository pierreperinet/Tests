using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CleanPhotos
{
    class Program
    {
        static void Main(string[] args)
        {
            //KEEP var directories = Directory.GetDirectories(@"C:\Users\Pierre\Pictures");
            //KEEP foreach (var dir in directories)
            //KEEP {
            //KEEP     ListDirectory(dir);
            //KEEP }
            var pictures = ListFiles(@"C:\Users\Pierre\Pictures");
            var picturesInfos = pictures.Select(p => new FileInfo(p));
            foreach (var p in picturesInfos)
            {
                var picturesDuplicates = ListFiles(@"C:\Users\Pierre\Photos");
                var picturesDuplicatesInfos = picturesDuplicates.Select(px => new FileInfo(px)).ToList();
                foreach (var pd in picturesDuplicatesInfos)
                {
                    if (FilesAreEqual_OneByte(p, pd))
                    {
                        Console.WriteLine($"  {p.FullName}  = {pd.FullName}");
                        pd.MoveTo($"C:\\Users\\Pierre\\PicturesTrash\\{pd.Name}");
                        //picturesDuplicatesInfos.Remove(pd);
                    }
                }
            }
            Console.WriteLine($"Pictures Count           = {pictures.Count}");

            //KEEP foreach (var dir in directories)
            //KEEP {
            //KEEP     CleanDirectory(dir);
            //KEEP }
            Console.ReadKey();
        }

        private static void ListDirectory(string dir)
        {
            Console.WriteLine($"-------------- {dir} -------------------------");
            var directories = Directory.GetDirectories(dir);
            foreach (var d in directories)
            {
                ListDirectory(d);
            }
            var files = Directory.GetFiles(dir);
            foreach (var f in files)
            {
                Console.WriteLine($"  {f}");
            }
        }

        private static List<string> ListFiles(string dir)
        {
            var list = new List<string>();
            foreach (var d in Directory.GetDirectories(dir))
            {
                list.AddRange(ListFiles(d));
            }
            list.AddRange(Directory.GetFiles(dir));
            return list;
        }

        private static void CleanDirectory(string dir)
        {
            Console.WriteLine($"-------------- {dir} -----------------------------------------");
            var di = new DirectoryInfo(dir);
            var oneJpgFiles = di.GetFiles("* (1).jpg");
            if (oneJpgFiles.Length == 0)
            {
                Console.WriteLine("no files present");
            }
            foreach (var deleteFileInfo in oneJpgFiles)
            {
                var deleteFileFullName = deleteFileInfo.FullName.ToLower();
                var oneJpgIndex = deleteFileFullName.IndexOf(" (1).jpg");
                var KeeperName = deleteFileFullName.Substring(0, oneJpgIndex) + ".jpg";
                var KeeperFileInfo = new FileInfo(KeeperName);
                if (File.Exists(KeeperName) && KeeperFileInfo.Length == deleteFileInfo.Length)
                {
                    Console.WriteLine(KeeperName);
                    Console.WriteLine(deleteFileFullName);
                    //KEEP File.Delete(deleteFileFullName);
                }
            }
        }
        static bool FilesAreEqual_OneByte(FileInfo first, FileInfo second)
        {
            if (first.Length != second.Length)
                return false;

            if (string.Equals(first.FullName, second.FullName, StringComparison.OrdinalIgnoreCase))
                return true;

            using (FileStream fs1 = first.OpenRead())
            using (FileStream fs2 = second.OpenRead())
            {
                for (int i = 0; i < first.Length; i++)
                {
                    if (fs1.ReadByte() != fs2.ReadByte())
                        return false;
                }
            }

            return true;
        }

        static bool FilesAreEqual_Hash(FileInfo first, FileInfo second)
        {
            byte[] firstHash = MD5.Create().ComputeHash(first.OpenRead());
            byte[] secondHash = MD5.Create().ComputeHash(second.OpenRead());

            for (int i = 0; i < firstHash.Length; i++)
            {
                if (firstHash[i] != secondHash[i])
                    return false;
            }
            return true;
        }
    }
}
