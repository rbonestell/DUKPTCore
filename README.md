# DUKPTCore [![Code Quality](https://img.shields.io/codefactor/grade/github/rbonestell/dukptcore)](https://www.codefactor.io/repository/github/rbonestell/dukptcore) ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/rbonestell/dukptcore/build-and-test) [![NuGet](https://img.shields.io/nuget/v/DUKPTCore.svg)](https://www.nuget.org/packages/DUKPTCore/) [![NuGet Downloads](https://img.shields.io/nuget/dt/DUKPTCore.svg)](https://www.nuget.org/packages/DUKPTCore/)

A .NET Standard implementation of TDES DUKPT, both PIN and Data variants.

 ### About DUKPTCore
 You'll find this library useful if you're working on financial services applications with the need to decrypt data using TDES (3DES, TDEA, triple-DES, etc) DUKPT (derived unique key per transaction), such as PIN or credit card account data.
 
 DUKPTCore was adapted from [sgbj's Dukpt.NET project](https://github.com/sgbj/Dukpt.NET) and ported to .NET Standard.
 
 ### Examples
 Encrypting data:
 ```c#
 string bdk = "0123456789ABCDEFFEDCBA9876543210";
 string ksn = "FFFF9876543210E00008";
 byte[] superSecretMessage = Encoding.UTF8.GetBytes("Hello World!");
 byte[] encryptedData = DUKPT.Encrypt(bdk, ksn, superSecretMessage, DUKPTVariant.Data);
 ```
 
  Decrypting data:
 ```c#
 byte[] decryptedData = DUKPT.Decrypt(bdk, ksn, encryptedData, DUKPTVariant.Data);
 string superSecretMessage = Encoding.UTF8.GetString(decryptedData); // Hello World!
 ```
 
### Resources
* [ANSI X9.24 - Retail Financial Services Set](https://webstore.ansi.org/Standards/ASCX9/ANSIX924RetailFinancial)
* [How To Decrypt Credit Card Data, Part I - IDTECH](https://idtechproducts.com/how-to-decrypt-credit-card-data-part-i/)
* [How To Decrypt Credit Card Data, Part II - IDTECH](https://idtechproducts.com/how-to-decrypt-credit-card-data-part-ii/)
* [How To Decrypt Magnetic Card Data With DUKPT - Parthenon Software Group, Travis Hoffman](https://www.parthenonsoftware.com/blog/how-to-decrypt-magnetic-stripe-scanner-data-with-dukpt/)
* [Dukpt.NET - Scott Batary (sgbj)](http://sgbj.github.io/Dukpt.NET/)
* [Derived unique key per transaction - Wikipedia](https://en.wikipedia.org/wiki/Derived_unique_key_per_transaction)

### Contributing
1. Fork it!
2. Create your feature branch: `git checkout -b my-new-feature`
3. Commit your changes: `git commit -am 'Added some new feature'`
4. Push to the branch: `git push origin my-new-feature`
5. [Submit a pull request!](https://github.com/rbonestell/DUKPTCore/pull/new/development)
