using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DrawDeploy
{
    public class Angular
    {
        public static bool Do()
        {
            string root = Config.Get("angular-root");
            string drawExe = Config.Get("draw-exe");
            string drawFileName = Config.Get("draw-file-name");
            string backupFolder = Config.Get("backup-folder");
            string appExe = Config.Get("angular-exe");

            #region backup 
            if (!Directory.Exists(root))
            {
                Console.WriteLine("root not exists");
                return false;
            }

            if (!Directory.Exists(string.Format(@"{0}\{1}", root, backupFolder)))
            {
                Directory.CreateDirectory(string.Format(@"{0}\{1}", root, backupFolder));
            }

            if(File.Exists(string.Format(@"{0}\{1}", root, appExe)))
            {
                try
                {
                    File.Move(string.Format(@"{0}\{1}", root, appExe), string.Format(@"{0}\{1}\{2}", root, backupFolder, appExe));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (File.Exists(string.Format(@"{0}\{1}.config", root, appExe)))
            {
                try
                {
                    File.Move(string.Format(@"{0}\{1}.config", root, appExe), string.Format(@"{0}\{1}\{2}.config", root, backupFolder, appExe));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            #endregion

            if (File.Exists(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, drawExe))
                && File.Exists(string.Format(@"{0}\{1}.config", AppDomain.CurrentDomain.BaseDirectory, drawExe))
                && File.Exists(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, drawFileName))
                )
            {
                try
                {
                    File.Copy(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, drawExe)
                        , string.Format(@"{0}\{1}", root, appExe));
                    File.Copy(string.Format(@"{0}\{1}.config", AppDomain.CurrentDomain.BaseDirectory, drawExe)
                        , string.Format(@"{0}\{1}.config", root, appExe));
                    File.Copy(string.Format(@"{0}\{1}", AppDomain.CurrentDomain.BaseDirectory, drawFileName)
                        , string.Format(@"{0}\{1}", root, drawFileName));

                    Console.WriteLine("Success!");
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            else
                return false;

            return true;
        }

        public static bool Undo()
        {
            string root = Config.Get("angular-root");
            string drawExe = Config.Get("draw-exe");
            string drawFileName = Config.Get("draw-file-name");
            string backupFolder = Config.Get("backup-folder");
            string appExe = Config.Get("angular-exe");

            if (!File.Exists(string.Format(@"{0}\{1}\{2}", root, backupFolder, appExe)))
            {
                Console.WriteLine("Angular Backup not found");
                return false;
            }

            try
            {
                File.Delete(string.Format(@"{0}\{1}", root, appExe));
                File.Move(string.Format(@"{0}\{1}\{2}", root, backupFolder, appExe), string.Format(@"{0}\{1}", root, appExe));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            if(File.Exists(string.Format(@"{0}\{1}.config", root, appExe)))
            {
                try
                {
                    File.Delete(string.Format(@"{0}\{1}.config", root, appExe));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if (File.Exists(string.Format(@"{0}\{1}\{2}.config", root, backupFolder, appExe)))
            {
                try
                {
                    File.Move(string.Format(@"{0}\{1}\{2}.config", root, backupFolder, appExe), string.Format(@"{0}\{1}.config", root, appExe));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            if(File.Exists(string.Format(@"{0}\{1}", root, drawFileName)))
            {
                try
                {
                    File.Delete(string.Format(@"{0}\{1}", root, drawFileName));
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            return true;
        }
    }
}
