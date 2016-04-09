const PROGID_SHELL = "WScript.Shell"
const ENV_PROCESS = "Process"

const BAT_SET_PERMS = "SET_PERMS.BAT"

'  warn user that to run this script, MUST be admin...no way around, sorry
Dim isAdmin

isAdmin = MsgBox( "PLEASE NOTE:" & vbcrlf & vbcrlf & "To run this Quickstart install script, " & vbcrlf & "you MUST have ADMINISTRATOR-level access.  " & vbcrlf & vbcrlf & "If you are not an Administrator, click 'No' to stop running the setup.", vbYesNo, "MUST BE ADMIN TO RUN")

If isAdmin = vbYes Then

  Dim fso
  Dim path 

  Set fso = CreateObject("Scripting.FileSystemObject")
  path = fso.GetAbsolutePathName( "." )
  Set fso = Nothing
 
  call DeleteWebFolder("ServiceInterface")
  call DeleteWebFolder("WebClient")

  '  create iis vdir for cs
  call CreateWebFolder( path + "\service.interface", "ServiceInterface" )

  '  create iis vdir for web.client
  call CreateWebFolder( path + "\web.client", "WebClient" )


  '  set "read" permission for "everyone" on web-read folders
  call SetWebDirPerms( path + "\service.interface" )

  call SetWebDirPerms( path + "\web.client" )



  '  ****  call out completion
  msgbox( "Completed running the install scripts." )
End If


'Create a Web folder 
Sub CreateWebFolder(folderPath, folderName)
	Dim vRoot, vDir

	Set vRoot = GetObject("IIS://localhost/W3svc/1/Root")
	
	For Each vDir In vRoot
	  If vDir.Name = folderName Then
	    	vRoot.Delete "IIsWebVirtualDir", folderName
	  End If
	Next

	Set vDir = vRoot.Create("IIsWebVirtualDir", folderName)
	vDir.AccessRead = true
	vDir.Path = folderPath
	
	vDir.DirBrowseFlags = &H4000003E     
	'  BITS likes dir browse        
	vDir.EnableDirBrowsing = True
	vDir.AppCreate( true )
	vDir.AccessScript = true
	
  vDir.SetInfo
End Sub

'  set permissions on the web-server-accessed folders to "read" by "everyone"
sub SetWebDirPerms( folderPath )
	dim oShell
	dim oEnv 
	
	'  create shell object
	set oShell = CreateObject(PROGID_SHELL)	
	'  create environment object for that shell
	Set oEnv = oShell.Environment(ENV_PROCESS) 
	
	oShell.Run "cacls " + folderPath + " /g everyone:R administrators:F /e /t /c", 0, true
end sub

Sub DeleteWebFolder(folderName)
	Dim vRoot, vDir

	Set vRoot = GetObject("IIS://localhost/W3svc/1/Root")
	
	For Each vDir In vRoot
	  If vDir.Name = folderName Then
	    	vRoot.Delete "IIsWebVirtualDir", folderName
	  End If
	Next
End Sub

