@ECHO OFF

:choice
set /P c=Are you sure you want to continue[Y/N]?
if /I "%c%" EQU "Y" goto :somewhere
if /I "%c%" EQU "N" goto :somewhere_else
goto :choice

:somewhere

echo "Resetting repo"
git reset --hard origin/master
pause 
exit

:somewhere_else
exit