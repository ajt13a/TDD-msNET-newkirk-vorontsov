Notes:  -- the files related to this document are now in a GitHub repository
        -- no longer valid:  http://workspaces.gotdotnet.com/tdd
        
        -- James Newkirk was the author of NUnit 2
        -- James Newkirk and Brad Wilson are responsible for xUnit.net
           which is a substantially improved alternative to NUnit
           https://xunit.github.io/
        -- "ASP.NET 5 uses xUnit.net as its unit test framework.
            This framework uses the [Fact] attribute instead of
            the [TestMethod] attribute (and no [TestClass] attribute])"
            http://stephenwalther.com/archive/2015/02/24/top-10-changes-in-asp-net-5-and-mvc-6
        
LinkS:  http://jamesnewkirk.typepad.com/posts/tdd.html
        http://jamesnewkirk.typepad.com/tdd/toc.pdf Book table of content
        http://jamesnewkirk.typepad.com/tdd/ch2.pdf Book sample, chapter 2.
        https://www.microsoftpressstore.com/store/test-driven-development-in-microsoft-.net-9780735691292 
        http://www.amazon.com/Test-Driven-Development-Microsoft-Developer-Reference-ebook/dp/B00JDMPOSO/ e-book
        http://www.nunit.org/ 
        @xunit        https://twitter.com/xunit
        @jamesnewkirk https://twitter.com/jamesnewkirk
        @bradwwilson  https://twitter.com/bradwilson
        
        
Kudos:  Joe K. Priestly for his free .msi extraction tool jsMSIx.exe - Simple MSI/MSM Unpacker Program
        http://www.jsware.net/jsware/msicode.php5
        http://www.jsware.net/jsware/msicode.php5#unpackx 
        http://www.jsware.net/jsware/zips/jsmsix418a.zip  contains jsMSIx.exe 
        
Gotchas:
(a) permissions for catalog_Data.mdf and catalog_Data.log
(b) version of SQL server is incorrect:                     [ MS SQL Server 2014 Management Studio ]
    "Msg 1813, Level 16, State 2, Line 1
     Could not open new database 'catalog'. CREATE DATABASE is aborted.
     Msg 950, Level 20, State 1, Line 1
     Database 'catalog' cannot be upgraded because its non-release version (539) is not supported
     by this version of SQL Server. You cannot open a database that is incompatible with this version of sqlservr.exe.
     You must re-create the database."
        

                         Readme file
                             for
              Test Driven Development in Microsoft .NET

               by James Newkirk and Alexei Vorontsov
                
             Copyright (c) 2004 by Microsoft Corporation
                    Portions copyright (c) 2004 
		by James Newkirk and Alexei Vorontsov
                       All rights reserved.


README CONTENTS
 - WHAT'S IN THIS COMPANION CONTENT?
 - Instructions for each chapter
 - SUPPORT INFORMATION
 - Microsoft Press support information
 - Microsoft Visual Studio .NET support
 - Supported Platforms - Windows XP (SP1), Visual Studio.NET 2003 (.NET Framework 1.1), 
	SQL Server 2000 (SP3), NUnit V2.1.4


WHAT'S IN THIS COMPANION CONTENT?
=================================

The companion content includes the sample files to use with
the book and this Readme.txt file.

Instructions for each Chapter
=============================
	                     

Chapter 2
=========

1. Open stack.sln in Visual Studio .NET and build.

2. Run NUnit and open bin\Debug\stack.dll and run the tests.

Chapter 3
=========

1. Open Before\primes.sln in Visual Studio .NET and build to see the initial version of the primes 
   program.

2. Run NUnit and open bin\debug\primes.dll and run the tests.

3. Open After\primes.sln in Visual Studio .NET and build to see the final version of the primes 
   program.

4. Run NUnit and open bin\debug\primes.dll and run the tests.

Chapter 5
=========

1. Run InstallDatabase.vbs to install the "catalog" database. 

2. Open DataAccessLayer\DataAccessLayer.sln in Visual Studio .NET and build. This will open two 
   project files - DataAccessLayer and DataAccessLayerTests. 

3. Run NUnit and open DataAccessLayerTests\bin\debug\DataAccessLayerTests.dll and run the tests.

4. Run UninstallDatabase.vbs to remove the "catalog" database before moving to another chapter. 

Chapter 6
=========

1. Run InstallDatabase.vbs to install the "catalog" database. 

2. Run Installvroot.vbs to install the virtual directory for the web service. 

3. Open service.interface\service.interface.sln in Visual Studio .NET and build. This will open three
   project files - ServiceInterface, DataAccessLayer and DataAccessLayerTests. 

4. Run NUnit and open DataAccessLayerTests\bin\debug\DataAccessLayerTests.dll and run the tests.

5. Run the following script in the SQL Query Analyzer - Replace "JAMESNEW01" with the the name of 
   your current machine (we assume that your web service and SQL database are on the current machine).  

    if not exists (select * from master.dbo.syslogins where loginname = N'JAMESNEW01\ASPNET')
        EXEC sp_grantlogin N'JAMESNEW01\ASPNET'
	EXEC sp_defaultdb N'JAMESNEW01\ASPNET', N'catalog'
	
    GO
    
    USE catalog
    GO

    if not exists (select * from dbo.sysusers where name = N'ASPNET' and uid < 16382)
	EXEC sp_grantdbaccess N'JAMESNEW01\ASPNET', N'ASPNET'
    GO

    EXEC sp_addrolemember N'db_owner', N'ASPNET'
    GO


Note: This is not a safe/secure thing to do but we need to do this to run the tests. 

6. Run NUnit and open service.interface\bin\ServiceInterface.dll and run the tests.
 
7. Run UninstallDatabase.vbs to remove the "catalog" database before moving to another chapter. 

Chapter 7, 8, 9, and 10
=======================

1. Run InstallDatabase.vbs to install the "catalog" database. 

2. Run Installvroot.vbs to install the virtual directory for the web service. 

3. Open book.sln in Visual Studio .NET and build. This will open six
   project files. 

4. Run NUnit and open DataAccessLayerTests\bin\debug\DataAccessLayerTests.dll and run the tests.

5. Run the following script in the SQL Query Analyzer - Replace "JAMESNEW01" with the the name of 
   your current machine (we assume that your web service and SQL database are on the current machine).  

    if not exists (select * from master.dbo.syslogins where loginname = N'JAMESNEW01\ASPNET')
        EXEC sp_grantlogin N'JAMESNEW01\ASPNET'
	EXEC sp_defaultdb N'JAMESNEW01\ASPNET', N'catalog'
	
    GO
    
    USE catalog
    GO

    if not exists (select * from dbo.sysusers where name = N'ASPNET' and uid < 16382)
	EXEC sp_grantdbaccess N'JAMESNEW01\ASPNET', N'ASPNET'
    GO

    EXEC sp_addrolemember N'db_owner', N'ASPNET'
    GO


Note: This is not a safe/secure thing to do but we need to do this to run the tests. 

6. Run NUnit and open service.interface.tests\bin\service.interface.tests.dll and run the tests.

7. Go to customer.tests\bin\Debug and run fit.cmd. Open ..\..\CatalogResult.html to see the results of the FIT tests.
 
8. Run UninstallDatabase.vbs to remove the "catalog" database before moving to another chapter. 



Chapter 11
==========

1. Run InstallDatabase.vbs to install the "catalog" database. 

2. Run Installvroot.vbs to install the virtual directory for the web service. 

3. Open book.sln in Visual Studio .NET and build. This will open nine
   project files. 

4. Run NUnit and open DataAccessLayerTests\bin\debug\DataAccessLayerTests.dll and run the tests.

5. Run the following script in the SQL Query Analyzer - Replace "JAMESNEW01" with the the name of 
   your current machine (we assume that your web service and SQL database are on the current machine).  

    if not exists (select * from master.dbo.syslogins where loginname = N'JAMESNEW01\ASPNET')
        EXEC sp_grantlogin N'JAMESNEW01\ASPNET'
	EXEC sp_defaultdb N'JAMESNEW01\ASPNET', N'catalog'
	
    GO
    
    USE catalog
    GO

    if not exists (select * from dbo.sysusers where name = N'ASPNET' and uid < 16382)
	EXEC sp_grantdbaccess N'JAMESNEW01\ASPNET', N'ASPNET'
    GO

    EXEC sp_addrolemember N'db_owner', N'ASPNET'
    GO


Note: This is not a safe/secure thing to do but we need to do this to run the tests. 

6. Run NUnit and open service.interface.tests\bin\service.interface.tests.dll and run the tests.

7. Run NUnit and open service.layer.tests\bin\service.layer.tests.dll and run the tests.

7. Go to customer.tests\bin\Debug and run fit.cmd. Open ..\..\CatalogResult.html to see the results of the FIT tests.
 
8. Run UninstallDatabase.vbs to remove the "catalog" database before moving to another chapter. 

Chapter 12
==========

1. Run InstallDatabase.vbs to install the "catalog" database. 

2. Run Installvroot.vbs to install the virtual directory for the web service. 

3. Open book.sln in Visual Studio .NET and build. This will open eleven
   project files. 

4. Run NUnit and open DataAccessLayerTests\bin\debug\DataAccessLayerTests.dll and run the tests.

5. Run the following script in the SQL Query Analyzer - Replace "JAMESNEW01" with the the name of 
   your current machine (we assume that your web service and SQL database are on the current machine).  

    if not exists (select * from master.dbo.syslogins where loginname = N'JAMESNEW01\ASPNET')
        EXEC sp_grantlogin N'JAMESNEW01\ASPNET'
	EXEC sp_defaultdb N'JAMESNEW01\ASPNET', N'catalog'
	
    GO
    
    USE catalog
    GO

    if not exists (select * from dbo.sysusers where name = N'ASPNET' and uid < 16382)
	EXEC sp_grantdbaccess N'JAMESNEW01\ASPNET', N'ASPNET'
    GO

    EXEC sp_addrolemember N'db_owner', N'ASPNET'
    GO


Note: This is not a safe/secure thing to do but we need to do this to run the tests. 

6. Run NUnit and open service.interface.tests\bin\service.interface.tests.dll and run the tests.

7. Run NUnit and open service.layer.tests\bin\service.layer.tests.dll and run the tests.

8. Run NUnit and open web.client.tests\bin\web.client.tests.dll and run the tests

9. Go to customer.tests\bin\Debug and run fit.cmd. Open ..\..\CatalogResult.html to see the results of the FIT tests.
 
10. Open your web browser to http://localhost/WebClient/SearchPage.aspx. At this point (25 April 	
    2004) the search page does not return results from the datbase that will be present in the next 
    release. 

11. Run UninstallDatabase.vbs to remove the "catalog" database before moving to another chapter. 


Uninstalling the Source Code
----------------------------

To uninstall the source code, make the appropriate selection from
Add/Remove Programs in Control Panel.



SUPPORT INFORMATION
===================

Microsoft Press support information
----------------------------------- 

Every effort has been made to ensure the accuracy of the book and 
this companion content. Microsoft Press provides corrections for books and
companion content through the World Wide Web at:

	http://workspaces.gotdotnet.com/tdd  <== no longer valid


If you have comments, questions, or ideas regarding the book or this 
companion content, please send them to Microsoft Press via e-mail to:

	MSPInput@microsoft.com
    
or via postal mail to:

    	Microsoft Press
    	Attn:  Test Driven Development in Microsoft .NET Editor
    	One Microsoft Way
    	Redmond, WA  98052-6399

Please note that product support is not offered through the above addresses. 


Microsoft Visual Studio .NET Support
------------------------------------

For support information regarding Microsoft Visual Studio .NET or
Microsoft Visual Studio .NET 2003, connect to Microsoft Technical Support 
on the Web at:

    	http://support.microsoft.com/support/

More information about Visual Studio is available at:

	http://msdn.microsoft.com/vstudio/



