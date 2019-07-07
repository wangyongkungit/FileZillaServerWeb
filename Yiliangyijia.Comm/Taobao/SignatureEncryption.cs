using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Yiliangyijia.Comm.Taobao
{
    public class SignatureEncryption
    {
        public Dictionary<string, string> QQDB = new Dictionary<string, string>();
        private static String[] hexDigits = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
        HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create("http://www.jishibao.top/rest");
        public string MainUrl = "http://www.jishibao.top/JSB/rest";
        public string ProduceUrl(string ApiType,string ClassName, Dictionary<string, string> UrlParameter)
        {
            // Url格式 http://120.55.246.87/JSB/rest/{"类别"}/{"接口名称"}? {"查询参数"}
            int i = 0;
            string Url = MainUrl;
            Url = Url + "/"+ ApiType;
            Url = Url + "/" + ClassName;
            if (UrlParameter.Count != 0)
            {
                foreach (var a in UrlParameter)
                {
                    string Key = a.Key;
                    string Val = a.Value;
                    if (i == 0)
                    {
                        Url = Url + "?" + Key + "=" + Val;
                        i++;
                    }
                    else
                    {
                        Url = Url + "&" + Key + "=" + Val;
                    }
                }
            }
            return Url;
        }

        public string HeadField(string timestamp,string uuid,string uri)
        {
            string HF = "x-jsb-sdk-req-timestamp:"+timestamp;
            HF = HF + "\nx-jsb-sdk-req-uuid:" + uuid;

            req = WebRequest.CreateHttp(uri);
            req.Method = "PUT";
            req.Headers["x-jsb-sdk-req-timestamp"] = timestamp;
            req.Headers["x-jsb-sdk-req-uuid"] = uuid;

            QQDB.Add("Method", "PUT");
            QQDB.Add("Url", uri);
            QQDB.Add("x-jsb-sdk-req-timestamp", timestamp);
            QQDB.Add("x-jsb-sdk-req-uuid", uuid);

            return HF;
        }

        public string GetStringA()
        {
            string Rinfo = null;

            try
            {
                string UrlEncode = System.Web.HttpUtility.UrlEncode(QQDB["Url"].ToLower(), System.Text.Encoding.GetEncoding("UTF-8"));
                Rinfo = QQDB["Method"] + "\n";//
                Rinfo = Rinfo+UrlEncode + "\n";//
                Rinfo = Rinfo+"x-jsb-sdk-req-timestamp:" + QQDB["x-jsb-sdk-req-timestamp"] + "\n";//
                Rinfo = Rinfo+"x-jsb-sdk-req-uuid:" + QQDB["x-jsb-sdk-req-uuid"] + "\n";//
            }
            catch
            {

            }
            

            return Rinfo;
        }

        public string GetSignString(string key,string reqId,string StrToSign)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(StrToSign);
            byte[] hashedBytes = System.Security.Cryptography.SHA1.Create().ComputeHash(bytes);
            String hexDigest = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

            //   //计算摘要
            //MessageDigest sha1 = MessageDigest.getInstance("SHA-1");
            //sha1.update(Encoding.GetEncoding("UTF-8").GetBytes(strToSign));
            //String hexDigest = byteArrayToHexString(sha1.digest());

            //   //使用SK加密reqId，得到一个新的秘钥kReqid。
            byte[] kSecret = Encoding.UTF8.GetBytes("JSB4" + key);
            byte[] kReqid = HmacSHA256(reqId, kSecret);

            //   //使用新秘钥kReqid对摘要进行加密
            byte[] kSigning = HmacSHA256(hexDigest, kReqid);
            return byteArrayToHexString(kSigning);
        }
        private static byte[] HmacSHA256(String data, byte[] key)
        {
            using (HMAC hmac = new HMACSHA256(key))
            {
                byte[] byteData = Encoding.UTF8.GetBytes(data);
                byte[] hash = hmac.ComputeHash(byteData);

                return hash;
            }
        }
        //将字节数组转换为十六进制的字符串
        private static String byteArrayToHexString(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                sb.Append(byteToHexString(bytes[i]));
            }
            return sb.ToString();
        }
        private static String byteToHexString(byte b)
        {
            int ret = b;
            // System.out.println("ret = " + ret);
            if (ret < 0)
            {
                ret += 256;
            }
            int m = ret / 16;
            int n = ret % 16;
            return hexDigits[m] + hexDigits[n];
        }


        public string Ty(string k,string sin)
        {
            string a = "Credential=";
            a = a + k;
            a = a + ",SignedHeaders=x-jsb-sdkreq-timestamp;x-jsb-sdk-requuid,Signature=";
            a = a + sin;
            return a;
        }




        #region ----------------------获取头域2--------------------------------------------
        public string GetAuthorizationHeader(string method, string uri, string timestamp, string requestId,string ak,string sk)
        {
            //Build a string containing essential information used to identify a request. It will not be sent to the server but instead, used to generate a digest. The generated digest will be keyed-hashed and sent to the server.
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(method.ToUpper());
            stringBuilder.Append("\n");
            stringBuilder.Append(WebUtility.UrlEncode(uri).ToLower());
            stringBuilder.Append("\n");
            stringBuilder.Append("x-jsb-sdk-req-timestamp:");
            stringBuilder.Append(timestamp);
            stringBuilder.Append("\n");
            stringBuilder.Append("x-jsb-sdk-req-uuid:");
            stringBuilder.Append(requestId.ToLower());
            stringBuilder.Append("\n");

            var stringToSign = stringBuilder.ToString();
            var stringToSignUtf8 = Encoding.UTF8.GetBytes(stringToSign);
            //Get SHA-1 Algroism
            var sha1Algorism = new Sha1Digest();
            //Create a Digest of stringToSign using SHA-1
            sha1Algorism.BlockUpdate(stringToSignUtf8, 0, stringToSignUtf8.Length);
            var Sha1DigestOfStringToSign = new byte[sha1Algorism.GetDigestSize()];
            sha1Algorism.DoFinal(Sha1DigestOfStringToSign, 0);
            var Sha1DigestHexStringOfStringToSign = BitConverter.ToString(Sha1DigestOfStringToSign).Replace("-", "").ToLower();
            //Generate a hash key from JSB's secret key and request id. The hash key is the result of HMac SHA-256 algorism where request id is the string to hash and secret key is the hash key.
            var hashKey = ComputeHMacSHA256(requestId, Encoding.UTF8.GetBytes("JSB4" + sk));
            //Using the generated hash key above to keyed-hash Sha1DigestHexStringOfStringToSign. This will be used as the signature in the authorization header.
            var signature = ComputeHMacSHA256(Sha1DigestHexStringOfStringToSign, hashKey);
            var signatureHex = BitConverter.ToString(signature).Replace("-", "").ToLower();

            //Build the authorization header string.
            stringBuilder = new StringBuilder();
            stringBuilder.Append("Credential=" + ak);
            stringBuilder.Append(",SignedHeaders=x-jsb-sdk-req-timestamp;x-jsb-sdk-req-uuid,Signature=");
            stringBuilder.Append(signatureHex);
            return stringBuilder.ToString();
        }


        private byte[] ComputeHMacSHA256(string data, byte[] key)
        {
            var hmacSha256Algorism = new HMac(new Sha256Digest());

            hmacSha256Algorism.Init(new KeyParameter(key));
            var dataUtf8 = Encoding.UTF8.GetBytes(data);
            hmacSha256Algorism.BlockUpdate(dataUtf8, 0, dataUtf8.Length);
            var result = new byte[hmacSha256Algorism.GetUnderlyingDigest().GetDigestSize()];
            hmacSha256Algorism.DoFinal(result, 0);
            return result;
        }
        #endregion
    }
}
