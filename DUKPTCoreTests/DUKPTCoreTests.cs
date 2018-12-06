using System;
using System.Text;
using DUKPTCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DUKPTCoreTests
{
    [TestClass]
    public class DUKPTCoreTests
    {
        public static string _ksn = "FFFF9876543210E00008";
        public static string _bdk = "0123456789ABCDEFFEDCBA9876543210";
        public static string _clearData = "%B5452300551227189^HOGAN/PAUL      ^08043210000000725000000?\0\0\0\0";
        public static string _expectedEncryptedHexPEK = "C25C1D1197D31CAA87285D59A892047426D9182EC11353C051ADD6D0F072A6CB3436560B3071FC1FD11D9F7E74886742D9BEE0CFD1EA1064C213BB55278B2F12";
        public static string _expectedEncryptedHexDEK = "411D405D7DEDB9D84797F045559721E8C06A5565FFB3B4050509277E5F80072E2410E0E6ADCBB614419700A9173807BA27C4E9D80BE67A2C32498032B200A7E3";

        #region PEK Mask Tests

        [TestMethod]
        public void Test_PEK_Encryption()
        {
            byte[] encryptedBytes = DUKPT.Encrypt(_bdk, _ksn, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.PIN);
            string encryptedHexResult = BitConverter.ToString(encryptedBytes).Replace("-", "");
            Assert.AreEqual(encryptedHexResult, _expectedEncryptedHexPEK);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_PEK_Encryption_Invalid_Length_BDK()
        {
            byte[] encryptedBytes = DUKPT.Encrypt(_bdk.Substring(0, _bdk.Length / 2), _ksn, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_PEK_Encryption_Invalid_Length_KSN()
        {
            byte[] encryptedBytes = DUKPT.Encrypt(_bdk, _ksn.Substring(0, 2), Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Encryption_Null_Data()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(_bdk, _ksn, null, DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Encryption_Null_BDK()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(null, _ksn, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Encryption_Null_KSN()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(_bdk, null, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Encryption_Empty_BDK()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(string.Empty, _ksn, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Encryption_Empty_KSN()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(_bdk, string.Empty, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.PIN);
        }

        [TestMethod]
        public void Test_PEK_Decryption()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(_bdk, _ksn, _expectedEncryptedHexPEK.HexStringToByteArray(), DUKPTVariant.PIN);
            string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
            Assert.AreEqual(decryptedData, _clearData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_PEK_Decryption_Invalid_Length_BDK()
        {
            byte[] encryptedBytes = DUKPT.Decrypt(_bdk.Substring(0, _bdk.Length / 2), _ksn, _expectedEncryptedHexPEK.HexStringToByteArray(), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_PEK_Decryption_Invalid_Length_KSN()
        {
            byte[] encryptedBytes = DUKPT.Decrypt(_bdk, _ksn.Substring(0, 2), _expectedEncryptedHexPEK.HexStringToByteArray(), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Decryption_Null_Data()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(_bdk, _ksn, null, DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Decryption_Null_BDK()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(null, _ksn, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Decryption_Null_KSN()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(_bdk, null, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Decryption_Empty_BDK()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(string.Empty, _ksn, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.PIN);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_PEK_Decryption_Empty_KSN()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(_bdk, string.Empty, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.PIN);
        }

        #endregion

        #region DEK Mask Tests

        [TestMethod]
        public void Test_DEK_Encryption()
        {
            byte[] encryptedBytes = DUKPT.Encrypt(_bdk, _ksn, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.Data);
            string encryptedHexResult = BitConverter.ToString(encryptedBytes).Replace("-", "");
            Assert.AreEqual(encryptedHexResult, _expectedEncryptedHexDEK);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_DEK_Encryption_Invalid_Length_BDK()
        {
            byte[] encryptedBytes = DUKPT.Encrypt(_bdk.Substring(0, _bdk.Length / 2), _ksn, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_DEK_Encryption_Invalid_Length_KSN()
        {
            byte[] encryptedBytes = DUKPT.Encrypt(_bdk, _ksn.Substring(0, 2), Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Encryption_Null_Data()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(_bdk, _ksn, null, DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Encryption_Null_BDK()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(null, _ksn, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Encryption_Null_KSN()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(_bdk, null, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Encryption_Empty_BDK()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(string.Empty, _ksn, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Encryption_Empty_KSN()
        {
            byte[] decryptedBytes = DUKPT.Encrypt(_bdk, string.Empty, Encoding.UTF8.GetBytes(_clearData), DUKPTVariant.Data);
        }

        [TestMethod]
        public void Test_DEK_Decryption()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(_bdk, _ksn, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.Data);
            string decryptedData = Encoding.UTF8.GetString(decryptedBytes);
            Assert.AreEqual(decryptedData, _clearData);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_DEK_Decryption_Invalid_Length_BDK()
        {
            byte[] encryptedBytes = DUKPT.Decrypt(_bdk.Substring(0, _bdk.Length / 2), _ksn, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Test_DEK_Decryption_Invalid_Length_KSN()
        {
            byte[] encryptedBytes = DUKPT.Decrypt(_bdk, _ksn.Substring(0, 2), _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Decryption_Null_Data()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(_bdk, _ksn, null, DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Decryption_Null_BDK()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(null, _ksn, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Decryption_Null_KSN()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(_bdk, null, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Decryption_Empty_BDK()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(string.Empty, _ksn, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.Data);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Test_DEK_Decryption_Empty_KSN()
        {
            byte[] decryptedBytes = DUKPT.Decrypt(_bdk, string.Empty, _expectedEncryptedHexDEK.HexStringToByteArray(), DUKPTVariant.Data);
        }

        #endregion
    }
}
