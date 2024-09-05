

public class Utils
{
    public static void InitLog(string fileName = "log.txt")
    {
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, false);
        sw.Close();
    }

    public static void LogWrite(string msg, string fileName = "log.txt")
    {
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true);
        sw.Write(msg);
        sw.Close();
    }

    public static void LogWriteLine(string msg, string fileName = "log.txt")
    {
        System.IO.StreamWriter sw = new System.IO.StreamWriter(fileName, true);
        sw.WriteLine(msg);
        sw.Close();
    }
}