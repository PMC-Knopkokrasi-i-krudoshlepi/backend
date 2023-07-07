using System.Security.Cryptography;
using System.Text;

namespace DPOBackend.Helpers;

public static class SercurityAlgs
{
    public static string ComputeUserCode(int id, string name, string password)
    {
        byte[] tmpSource;
        byte[] tmpHash;
        
        tmpSource = ASCIIEncoding.ASCII.GetBytes(string.Concat(id.ToString(), name, password));
        tmpHash = new MD5CryptoServiceProvider().ComputeHash(tmpSource);
        return ToHexString(tmpHash);
    }

    private static string ToHexString(byte[] tmpHash)
    {
        StringBuilder sOutput = new StringBuilder(tmpHash.Length);
        for (int i = 0; i < tmpHash.Length; i++)
        {
            sOutput.Append(tmpHash[i].ToString("X2"));
        }

        return sOutput.ToString();
        
    }
}