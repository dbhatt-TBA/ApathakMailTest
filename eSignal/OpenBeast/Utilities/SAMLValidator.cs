using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Xml;
using System.Security.Cryptography.Xml;

namespace OpenF2.AppCode
{
    class SAMLValidator
    {
        private static string SAML2_PROTOCOL_NS = "urn:oasis:names:tc:SAML:2.0:protocol";

        private static bool SignatureIsTrustedAndValid(XmlDocument doc)
        {
            SignedXml sxml = new SignedXml(doc);
            XmlNodeList nodeList = doc.GetElementsByTagName("Signature", "http://www.w3.org/2000/09/xmldsig#");
            if (nodeList.Count <= 0)
            {
                throw new CryptographicException("Verification failed: No Signature was found in the document.");
            }
            if (nodeList.Count >= 2)
            {
                throw new CryptographicException("Verification failed: More that one signature was found for the document.");
            }

            XmlElement certificateElem = (XmlElement)doc.GetElementsByTagName("X509Certificate")[0];
            string certificate = certificateElem.InnerText;

            byte[] certData = Convert.FromBase64String(certificate);
            X509Certificate2 cert = new X509Certificate2(certData);
            PublicKey pk = cert.PublicKey;


            sxml.LoadXml((XmlElement)nodeList[0]);

            // create certificate
            return sxml.CheckSignature(cert, true);
        }
        public static bool IsValid(XmlDocument doc)
        {
            XmlElement docElement = doc.DocumentElement;
            if (docElement.NamespaceURI != SAML2_PROTOCOL_NS || docElement.LocalName != "Response")
            {
                throw new CryptographicException("Invalid response structure");
            }
            return SignatureIsTrustedAndValid(doc);
        }

    }
}