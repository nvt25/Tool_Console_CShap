using System;

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
                string[] files = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);
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

                Console.WriteLine($"Total File In Folder :{files.Length}");


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
                        List<string> dataSpine = files.Where(x => x.Contains(item.Key)).ToList();
                        foreach (string file in dataSpine)
                        {
                            string destinationFilePath = Path.Combine(item.Value, Path.GetFileName(file));
                            File.Move(file, destinationFilePath);
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
    }
}
