const PROGID_SHELL = "WScript.Shell"
const ENV_PROCESS = "Process"

const BAT_SET_PERMS = "SET_PERMS.BAT"

'  warn user that to run this script, MUST be admin...no way around, sorry
Dim isAdmin

isAdmin = MsgBox( "PLEASE NOTE:" & vbcrlf & vbcrlf & "To run this uninstall script, " & vbcrlf & "you MUST have ADMINISTRATOR-level access.  " & vbcrlf & vbcrlf & "If you are not an Administrator, click 'No' to stop running the setup.", vbYesNo, "MUST BE ADMIN TO RUN")

If isAdmin = vbYes Then
  Dim wshSell

  '  Install database scripts
  Set wshShell = WScript.CreateObject("WScript.Shell")
  WshShell.Run "osql -E -i..\Database\uninstalldb.sql", 0, true
  Set wshSell = Nothing
  
  '  ****  call out completion
  msgbox( "Completed running the install scripts." )
End If