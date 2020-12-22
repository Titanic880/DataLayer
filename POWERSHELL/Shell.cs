using System;
using System.Diagnostics;

namespace DataLayer.POWERSHELL
{
    public static class Shell
    {
        #region Github
        /// <summary>
        /// Clones a Repo from github and puts it in a folder with the repo name
        /// </summary>
        /// <param name="Link"></param>
        public static bool GitClone(string Link)
        {
            //Gets the repository's Name
            string item = Link.Split('/')[Link.Split('/').Length - 1];

            Byte[] bytes = System.Text.Encoding.Unicode.GetBytes($"cd {item} {Environment.NewLine} git clone {Link}");
            string Scripts = Convert.ToBase64String(bytes);

            ERRORCHECK.ErrorLog.Output(Scripts, ERRORCHECK.ErrorLog.ErrorLevel.Debug);
            try
            {
                //Makes a process that runs the powershell window with a predefined string input
                ProcessStartInfo start = new ProcessStartInfo()
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy unrestricted -EncodedCommand \"{Scripts}\"",
                    UseShellExecute = false
                };
                Process.Start(start);
            }
            catch (Exception Ex)
            {
                ERRORCHECK.ErrorLog.Output(Ex.Message, ERRORCHECK.ErrorLog.ErrorLevel.Major);
                return false;
            }

            ERRORCHECK.ErrorLog.Output("Cloned Repository Successfully!");
            return true;
        }
        #endregion Github

        #region Zipping

        public static bool ZipFile(string path)
        {


            throw new NotImplementedException();
        }

        public static bool UnZipFile(string path)
        {

            throw new NotImplementedException();
        }
        #endregion Zipping
    }
}
