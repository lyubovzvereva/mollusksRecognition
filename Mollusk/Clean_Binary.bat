@echo off
FOR /d /r %%p IN (*bin *obj) DO ( rmdir "%%p" /s /q )