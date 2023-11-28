using System;
using System.IO;
using System.Linq;

namespace Main
{
    class Program
    {
        static void Main(string[] args)
        {
            string path;
            Dictionary<string, string> listFolderExport = new Dictionary<string, string>();
            Console.WriteLine("Welcom To Tool Get Spine");
            Console.WriteLine("Nhap duong dan Folder :");
            path = Console.ReadLine();
            Console.WriteLine($"Ban Muon Lay Spin Tu Folder :{path}");
            if (Directory.Exists(path))
            {
                string pathExport = path + "\\ExportData_Tool_Tuyen";
                try
                {
                    if (!Directory.Exists(pathExport))
                    {
                        Directory.CreateDirectory(pathExport);
                        Console.WriteLine("Creat Success Folder : ExportData_Tool_Tuyen");
                    }
                    else
                    {
                        Console.WriteLine("Da ton tai");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error : " + ex);
                }
                //Get file only floder
                //string[] files = Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly);
                List<string> files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories).ToList();
                Console.WriteLine($"List file in floder '{path}':");

                foreach (string file in files)
                {
                    if (Path.GetFileName(file).Contains("atlas.txt"))
                    {
                        Console.WriteLine(Path.GetFileName(file));
                        string nameFile = Path.GetFileName(file);
                        int index = nameFile.IndexOf(".atlas");
                        if (index != -1)
                        {
                            string result = nameFile.Substring(0, index);
                            Console.WriteLine("\".atlas\" := " + result);
                            if (!listFolderExport.ContainsKey(result))
                            {
                                listFolderExport.Add(result, (pathExport + "\\" + result));
                            }
                        }
                        else
                        {
                            Console.WriteLine("********************** Not Found *********************");
                        }
                    }
                }

                Console.WriteLine($"Total File In Folder :{files.Count}");


                foreach (var item in listFolderExport)
                {
                    try
                    {
                        if (!Directory.Exists(item.Value))
                        {
                            Directory.CreateDirectory(item.Value);
                            Console.WriteLine("Creat Success Folder : " + item.Value);
                        }
                        else
                        {
                            Console.WriteLine("Da ton tai");
                        }
                        //Get
                        List<string> dataSpine = files.Where(x => GetnameFile(x, item.Key)).ToList();
                        Console.WriteLine("Get File*********************** Count :" + dataSpine.Count + ":" + item.Key);
                        foreach (string file in dataSpine)
                        {
                            try
                            {
                                string destinationFilePath = Path.Combine(item.Value, Path.GetFileName(file));
                                File.Move(file, destinationFilePath);
                                RenameFile(file);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Trung Ten ########" + file + "_____" + file);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error : " + ex);
                    }
                }
            }
            else
            {
                Console.WriteLine($"Folder ___{path}___ not.");
            }
        }
        public static bool GetnameFile(string _path, string _key)
        {
            string atlas = $"{_key}.atlas";
            string skel = $"{_key}.skel";
            return Path.GetFileNameWithoutExtension(_path).Equals(_key) || Path.GetFileNameWithoutExtension(_path).Equals(atlas) || Path.GetFileNameWithoutExtension(_path).Equals(skel);
        }
        public static void RenameFile(string _path)
        {
            if (Path.GetFileName(_path).Contains("atlas.txt") || Path.GetFileName(_path).Contains("png"))
            {
                return;
            }
            if (Path.GetFileName(_path).Contains("skel"))
            {
                File.Move(_path, Path.ChangeExtension(_path, ".bytes"));
            }
            else
            {
                File.Move(_path, Path.ChangeExtension(_path, ".json"));
            }
        }
    }
}
