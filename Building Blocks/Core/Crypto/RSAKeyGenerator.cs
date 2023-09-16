using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace Core.Crypto
{
    public class RSAKeyGenerator
    {
        public static RsaSecurityKey BuildRSAKey(string publicKey)
        {
            RSA rsa = RSA.Create();


            rsa.ImportSubjectPublicKeyInfo(
                source: Convert.FromBase64String(publicKey),
                out _
            );

            var IssuerSigningKey = new RsaSecurityKey(rsa);

            return IssuerSigningKey;
        }
    }
}
