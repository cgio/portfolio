// Redacted portions of this code use DEADBEEF FEEDDEAD
public static string Decrypt(string text, string key)
  {
      byte[] buffer3;
      if (string.IsNullOrEmpty(text))
      {
          return string.Empty;
      }
      char[] chArray = ("DEADBEEF" + "FEEDDEAD").ToCharArray();
      byte[] buffer = new byte[2 * chArray.Length];
      for (int i = 0; i < chArray.Length; i++)
      {
          buffer[2 * i] = BitConverter.GetBytes(chArray[i])[0];
          buffer[(2 * i) + 1] = BitConverter.GetBytes(chArray[i])[1];
      }
      byte[] bytes = Encoding.UTF8.GetBytes(BitConverter.ToString(new MD5CryptoServiceProvider().ComputeHash(buffer)).Replace("-", string.Empty).Substring(0, 8));
      byte[] rgbIV = new byte[] { 0x1, 0x2, 0x3, 0x4, 0x5, 0x6, 0x7, 0x8 };
      DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
      try
      {
          buffer3 = Convert.FromBase64String("DEADBEEF");
      }
      catch
      {
          return string.Empty;
      }

      MemoryStream stream = new MemoryStream();
      using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(bytes, rgbIV), CryptoStreamMode.Write))
      {
          stream2.Write(buffer3, 0, buffer3.Length);
          stream2.FlushFinalBlock();
      }
      
      return Encoding.UTF8.GetString(stream.ToArray());

  }
