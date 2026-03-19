Set WshShell = CreateObject("WScript.Shell") 
strPath = CreateObject("Scripting.FileSystemObject").GetParentFolderName(WScript.ScriptFullName)

' Puszczenie skryptu START_MACRO.bat w tle (tryb ukryty)
WshShell.Run chr(34) & strPath & "\START_MACRO.bat" & Chr(34), 0
Set WshShell = Nothing